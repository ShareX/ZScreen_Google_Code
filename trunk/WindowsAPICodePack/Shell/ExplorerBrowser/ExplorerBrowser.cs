//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.WindowsAPICodePack.Shell
{
    
    /// <summary>
    /// This class is a wrapper around the Windows Explorer Browser control.
    /// </summary>
    public class ExplorerBrowser : System.Windows.Forms.UserControl, Microsoft.WindowsAPICodePack.Shell.IServiceProvider, IExplorerPaneVisibility, IExplorerBrowserEvents, ICommDlgBrowser
    {
        #region properties
        /// <summary>
        /// Options that control how the ExplorerBrowser navigates
        /// </summary>
        public ExplorerBrowserNavigationOptions NavigationOptions
        {
            get;
            private set;
        }

        /// <summary>
        /// Options that control how the content of the ExplorerBorwser looks
        /// </summary>
        public ExplorerBrowserContentOptions ContentOptions
        {
            get;
            private set;
        }

        /// <summary>
        /// The set of ShellObjects in the Explorer Browser
        /// </summary>
        public ShellObjectCollection Items
        {
            get
            {
                return new ShellObjectCollection( GetItemsArray( ) );
            }
        }

        /// <summary>
        /// The set of selected ShellObjects in the Explorer Browser
        /// </summary>
        public ShellObjectCollection SelectedItems
        {
            get
            {
                return new ShellObjectCollection( GetSelectedItemsArray( ) );
            }
        }

        /// <summary>
        /// Contains the navigation history of the ExplorerBrowser
        /// </summary>
        public ExplorerBrowserNavigationLog NavigationLog
        {
            get;
            private set;
        }

        /// <summary>
        /// The name of the property bag used to persist changes to the ExplorerBrowser's view state.
        /// </summary>
        public string PropertyBagName
        {
            get
            {
                return propertyBagName;
            }
            set
            {
                propertyBagName = value;
                explorerBrowserControl.SetPropertyBag( propertyBagName );
            }
        }

        #endregion
       
        #region operations
        /// <summary>
        /// Clears the Explorer Browser of existing content, fills it with
        /// the specified content, and adds a new point to the Travel Log.
        /// </summary>
        /// <param name="shellObject"></param>
        public void Navigate( ShellObject shellObject )
        {
            if( explorerBrowserControl == null )
            {
                antecreationNavigationTarget = shellObject;
            }
            else
            {
                HRESULT hr = explorerBrowserControl.BrowseToObject( shellObject.NativeShellItem, 0 );
                if( hr != HRESULT.S_OK )
                {
                    throw new COMException( "BrowseToObject failed", (int)hr );
                }
            }
        }

        /// <summary>
        /// Navigates within the navigation log. This does not change the set of 
        /// locations in the navigation log.
        /// </summary>
        /// <param name="direction">Forward of Backward</param>
        /// <returns></returns>
        public bool NavigateLogLocation( NavigationLogDirection direction )
        {
            return NavigationLog.NavigateLog( direction );
        }

        /// <summary>
        /// Navigate within the navigation log. This does not change the set of 
        /// locations in the navigation log.
        /// </summary>
        /// <param name="navigationLogIndex">An index into the navigation logs Locations collection.</param>
        /// <returns></returns>
        public bool NavigateLogLocation( int navigationLogIndex )
        {
            return NavigationLog.NavigateLog( navigationLogIndex );
        }
        #endregion

        #region events

        /// <summary>
        /// Fires when the SelectedItems collection changes. 
        /// </summary>
        /// <remarks>Arrives on a non-UI thread</remarks>
        public event ExplorerBrowserSelectionChangedEventHandler SelectionChanged;

        /// <summary>
        /// Fires when the Items colection changes. 
        /// </summary>
        /// <remarks>Arrives on a non-UI thread</remarks>
        public event ExplorerBrowserItemsChangedEventHandler ItemsChanged;
        
        /// <summary>
        /// Fires when a navigation has been initiated, but is not yet complete.
        /// </summary>
        /// <remarks>Arrives on a non-UI thread</remarks>
        public event ExplorerBrowserNavigationPendingEventHandler NavigationPending;

        /// <summary>
        /// Fires when a navigation has been 'completed': no NavigationPending listener 
        /// has cancelled, and the ExplorerBorwser has created a new view. The view 
        /// will be populated with new items asynchronously, and ItemsChanged will be 
        /// fired to reflect this some time later.
        /// </summary>
        /// <remarks>Arrives on a non-UI thread</remarks>
        public event ExplorerBrowserNavigationCompleteEventHandler NavigationComplete;
        
        /// <summary>
        /// Fires when either a NavigationPending listener cancels the navigation, or
        /// if the operating system determines that navigation is not possible.
        /// </summary>
        /// <remarks>Arrives on a non-UI thread</remarks>
        public event ExplorerBrowserNavigationFailedEventHandler NavigationFailed;

        #endregion

        #region implementation

        #region construction
        internal ExplorerBrowserClass explorerBrowserControl = null;
        
        // for the IExplorerBrowserEvents Advise call
        internal uint eventsCookie = 0;

        // for doing background state update of the collections
        System.Windows.Forms.Timer collectionStateUpdateTimer;
        System.Threading.AutoResetEvent updateCollectionState = new AutoResetEvent( false );

        // name of the property bag that contains the view state options of the browser
        string propertyBagName = typeof( ExplorerBrowser ).FullName;

        /// <summary>
        /// Initializes the ExplorerBorwser WinForms wrapper.
        /// </summary>
        public ExplorerBrowser( )
            : base( )
        {
            NavigationOptions = new ExplorerBrowserNavigationOptions( this );
            ContentOptions = new ExplorerBrowserContentOptions( this );
            NavigationLog = new ExplorerBrowserNavigationLog( this );

            collectionStateUpdateTimer = new System.Windows.Forms.Timer();
            collectionStateUpdateTimer.Tick += new EventHandler( TestForCollectionUpdate );
            collectionStateUpdateTimer.Interval = 100;
            collectionStateUpdateTimer.Start( );
        }
        #endregion

        #region message handlers

        /// <summary>
        /// Displays a placeholder for the explorer browser in design mode
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint( PaintEventArgs e )
        {
            if( DesignMode )
            {
                LinearGradientBrush linGrBrush = new LinearGradientBrush(
                    ClientRectangle,
                    Color.Aqua,
                    Color.CadetBlue,
                    LinearGradientMode.ForwardDiagonal );


                e.Graphics.FillRectangle( linGrBrush, ClientRectangle );

                StringFormat sf = new StringFormat( );
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(
                    "ExplorerBrowserControl",
                    new Font( "Garamond", 30 ),
                    Brushes.White,
                    ClientRectangle,
                    sf );
            }

            base.OnPaint( e );
        }

        ShellObject antecreationNavigationTarget = null;

        /// <summary>
        /// Creates and initializes the native ExplorerBrowser control
        /// </summary>
        protected override void OnCreateControl( )
        {
            base.OnCreateControl( );

            HRESULT hr = HRESULT.S_OK;

            if( this.DesignMode == false )
            {
                explorerBrowserControl = new ExplorerBrowserClass( );

                // hooks up IExplorerPaneVisibility and ICommDlgBrowser event notifications
                hr = ExplorerBrowserNativeMethods.IUnknown_SetSite( explorerBrowserControl, this );

                // hooks up IExplorerBrowserEvents event notification
                hr = explorerBrowserControl.Advise( 
                    Marshal.GetComInterfaceForObject( this, typeof( IExplorerBrowserEvents ) ),
                    out eventsCookie );

                CoreNativeMethods.RECT rect = new CoreNativeMethods.RECT( );
                rect.top = ClientRectangle.Top;
                rect.left = ClientRectangle.Left;
                rect.right = ClientRectangle.Right;
                rect.bottom = ClientRectangle.Bottom;

                explorerBrowserControl.Initialize( this.Handle, ref rect, null );

                // Force an initial show frames so that IExplorerPaneVisibility works the first time it is set.
                // This also enables the control panel to be browsed to. If it is not set, then navigating to 
                // the control panel succeeds, but no items are visible in the view.
                explorerBrowserControl.SetOptions( EXPLORER_BROWSER_OPTIONS.EBO_SHOWFRAMES );
                
                explorerBrowserControl.SetPropertyBag( propertyBagName );

                if( antecreationNavigationTarget != null )
                {
                    BeginInvoke( new MethodInvoker(
                    delegate
                    {
                        Navigate( antecreationNavigationTarget );
                        antecreationNavigationTarget = null;
                    } ) );
                }
            }
        }

        /// <summary>
        /// Sizes the native control to match the WinForms control wrapper.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged( EventArgs e )
        {
            if( explorerBrowserControl != null )
            {
                CoreNativeMethods.RECT rect = new CoreNativeMethods.RECT( );
                rect.top = ClientRectangle.Top;
                rect.left = ClientRectangle.Left;
                rect.right = ClientRectangle.Right;
                rect.bottom = ClientRectangle.Bottom;
                IntPtr ptr = IntPtr.Zero;
                explorerBrowserControl.SetRect( ref ptr, rect );
            }

            base.OnSizeChanged( e );
        }

        /// <summary>
        /// cleans up the explorer browser events+object when our window is being taken down
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleDestroyed( EventArgs e )
        {
            if( explorerBrowserControl != null )
            {
                // unhook events
                HRESULT hr = explorerBrowserControl.Unadvise( eventsCookie );
                ExplorerBrowserNativeMethods.IUnknown_SetSite( explorerBrowserControl, null );

                // destroy the explorer browser control
                explorerBrowserControl.Destroy( );

                // release com reference to it
                Marshal.ReleaseComObject( explorerBrowserControl );
                explorerBrowserControl = null;
            }

            base.OnHandleDestroyed( e );
        }

        private List<ShellObject> previousItems = new List<ShellObject>( );
        private List<ShellObject> previousSelection = new List<ShellObject>( );

        /// <summary>
        /// Tests for collection update and fires the events if neccessary
        /// </summary>
        void TestForCollectionUpdate( object sender, EventArgs e )
        {
            ShellObjectCollection newItems = Items;
            if( previousItems.Count != newItems.Count )
            {
                // item count is different, notify clients
                previousItems.Clear( );
                foreach( ShellObject shobj in newItems )
                    previousItems.Add( shobj );

                if( ItemsChanged != null )
                {
                    ItemsChanged( this, EventArgs.Empty );
                }
            }

            ShellObjectCollection newSelection = SelectedItems;
            if( previousSelection.Count != newSelection.Count )
            {
                // the selection count has changed, notify clients
                previousSelection.Clear( );
                foreach( ShellObject shobj in newSelection )
                    previousSelection.Add( shobj );

                if( SelectionChanged != null )
                {
                    SelectionChanged( this, EventArgs.Empty );
                }
            }

            // The selection can change without the number of items changing.
            // Because collection comparison is more expensive than counting,
            // limit camparison to when the ICommDlgBrowser tells up that the
            // selection has changed.
            if( updateCollectionState.WaitOne( 0 ) )
            {
                for( uint index = 0; index < previousSelection.Count; index++ )
                {
                    if( false == ShellObjectsAreEqual( previousSelection[ (int)index ], newSelection[ index ] )  )
                    {
                        // the selection items are different, notify client 
                        previousSelection.Clear( );
                        foreach( ShellObject shobj in newSelection )
                            previousSelection.Add( shobj );

                        if( SelectionChanged != null )
                        {
                            SelectionChanged( this, EventArgs.Empty );
                        }

                        break;
                    }
                }
            }
        }

        #endregion

        #region object interfaces

        #region IServiceProvider
        HRESULT Microsoft.WindowsAPICodePack.Shell.IServiceProvider.QueryService(
            ref Guid guidService, ref Guid riid, out IntPtr ppvObject )
        {
            HRESULT hr = HRESULT.S_OK;

            if( guidService.CompareTo( new Guid( ExplorerBrowserIIDGuid.IExplorerPaneVisibility ) ) == 0 )
            {
                // Responding to this SID allows us to control the visibility of the 
                // explorer browser panes
                ppvObject =
                    Marshal.GetComInterfaceForObject( this, typeof( IExplorerPaneVisibility ) );
                hr = HRESULT.S_OK;
            }
            else if( guidService.CompareTo( new Guid( ExplorerBrowserIIDGuid.ICommDlgBrowser ) ) == 0) 
            {
                // Responding to this SID allows us to hook up our ICommDlgBrowser
                // implementation so we get selection change events from the view.
                if( riid.CompareTo( new Guid( ExplorerBrowserIIDGuid.ICommDlgBrowser ) ) == 0 )
                {
                    ppvObject = Marshal.GetComInterfaceForObject( this, typeof( ICommDlgBrowser ) );
                    hr = HRESULT.S_OK;
                }
                else
                {
                    ppvObject = IntPtr.Zero;
                    hr = HRESULT.E_NOINTERFACE;
                }
            }
            else
            {
                IntPtr nullObj = IntPtr.Zero;
                ppvObject = nullObj;
                hr = HRESULT.E_NOINTERFACE;
            }

            return hr;
        }
        #endregion

        #region IExplorerPaneVisibility
        /// <summary>
        /// Controls the visibility of the explorer borwser panes
        /// </summary>
        /// <param name="explorerPane">a guid identifying the pane</param>
        /// <param name="peps">the pane state desired</param>
        /// <returns></returns>
        HRESULT IExplorerPaneVisibility.GetPaneState( ref Guid explorerPane, out EXPLORERPANESTATE peps )
        {
            switch( explorerPane.ToString( ) )
            {
                case ExplorerBrowserViewPanes.AdvancedQuery:
                    peps = VisibilityToPaneState( NavigationOptions.PaneVisibility.AdvancedQuery );
                    break;
                case ExplorerBrowserViewPanes.Commands:
                    peps = VisibilityToPaneState( NavigationOptions.PaneVisibility.Commands );
                    break;
                case ExplorerBrowserViewPanes.CommandsOrganize:
                    peps = VisibilityToPaneState( NavigationOptions.PaneVisibility.CommandsOrganize );
                    break;
                case ExplorerBrowserViewPanes.CommandsView:
                    peps = VisibilityToPaneState( NavigationOptions.PaneVisibility.CommandsView );
                    break;
                case ExplorerBrowserViewPanes.Details:
                    peps = VisibilityToPaneState( NavigationOptions.PaneVisibility.Details );
                    break;
                case ExplorerBrowserViewPanes.Navigation:
                    peps = VisibilityToPaneState( NavigationOptions.PaneVisibility.Navigation );
                    break;
                case ExplorerBrowserViewPanes.Preview:
                    peps = VisibilityToPaneState( NavigationOptions.PaneVisibility.Preview );
                    break;
                case ExplorerBrowserViewPanes.Query:
                    peps = VisibilityToPaneState( NavigationOptions.PaneVisibility.Query );
                    break;
                default:
#if LOG_UNKNOWN_PANES
                    System.Diagnostics.Debugger.Log( 4, "ExplorerBrowser", "unknown pane view state. id=" + explorerPane.ToString( ) );
#endif
                    peps = EXPLORERPANESTATE.EPS_DONTCARE;
                    break;
            }

            return HRESULT.S_OK;
        }

        private EXPLORERPANESTATE VisibilityToPaneState( PaneVisibilityState visibility )
        {
            switch( visibility )
            {
                case PaneVisibilityState.DontCare:
                    return EXPLORERPANESTATE.EPS_DONTCARE;

                case PaneVisibilityState.Hide:
                    return EXPLORERPANESTATE.EPS_DEFAULT_OFF | EXPLORERPANESTATE.EPS_FORCE;

                case PaneVisibilityState.Show:
                    return EXPLORERPANESTATE.EPS_DEFAULT_ON | EXPLORERPANESTATE.EPS_FORCE;

                default:
                    throw new ArgumentException( "unexpected PaneVisibilityState" );
            }
        }

        #endregion

        #region IExplorerBrowserEvents
        HRESULT IExplorerBrowserEvents.OnNavigationPending( IntPtr pidlFolder )
        {
            bool canceled = false;

            if( NavigationPending != null )
            {
                NavigationPendingEventArgs args = new NavigationPendingEventArgs( );

                // For some special items (like network machines), ShellObject.FromIDList
                // might return null
                args.PendingLocation = ShellObjectFactory.Create( pidlFolder );

                if( args.PendingLocation != null )
                {
                    foreach( Delegate del in NavigationPending.GetInvocationList( ) )
                    {
                        del.DynamicInvoke( new object[ ] { this, args } );
                        if( args.Cancel )
                        {
                            canceled = true;
                        }
                    }
                }
            }
        
            return canceled ? HRESULT.E_FAIL : HRESULT.S_OK;
        }

        HRESULT IExplorerBrowserEvents.OnViewCreated( IntPtr psv )
        {
            // The new view is empty, so both items and selection have changed
            previousItems.Clear( );
            if( ItemsChanged != null )
            {
                ItemsChanged( this, EventArgs.Empty );
            }

            previousSelection.Clear( );
            if( SelectionChanged != null )
            {
                SelectionChanged( this, EventArgs.Empty );
            }

            return HRESULT.S_OK;
        }

        HRESULT IExplorerBrowserEvents.OnNavigationComplete( IntPtr pidlFolder )
        {
            // view mode may change 
            ContentOptions.folderSettings.ViewMode = GetCurrentViewMode( );

            if( NavigationComplete != null )
            {
                NavigationCompleteEventArgs args = new NavigationCompleteEventArgs( );
                args.NewLocation = ShellObjectFactory.Create(pidlFolder);
                NavigationComplete( this, args );
            }
            return HRESULT.S_OK;
        }

        HRESULT IExplorerBrowserEvents.OnNavigationFailed( IntPtr pidlFolder )
        {
            if( NavigationFailed != null )
            {
                NavigationFailedEventArgs args = new NavigationFailedEventArgs( );
                args.FailedLocation = ShellObjectFactory.Create(pidlFolder);
                NavigationFailed( this, args );
            }
            return HRESULT.S_OK;
        }
        #endregion

        #region ICommDlgBrowser
        HRESULT ICommDlgBrowser.OnDefaultCommand( IntPtr ppshv )
        {
            return HRESULT.S_FALSE;
        }

        HRESULT ICommDlgBrowser.OnStateChange( IntPtr ppshv, CommDlgBrowserStateChange uChange )
        {
            if( uChange == CommDlgBrowserStateChange.CDBOSC_SELCHANGE )
                updateCollectionState.Set( );

            return HRESULT.S_OK;
        }

        HRESULT ICommDlgBrowser.IncludeObject( IntPtr ppshv, IntPtr pidl )
        {
            return HRESULT.S_OK;
        }

        #endregion

        #endregion

        #region utilities
        
        /// <summary>
        /// Returns the current view mode of the browser
        /// </summary>
        /// <returns></returns>
        internal FOLDERVIEWMODE GetCurrentViewMode()
        {
            IFolderView2 ifv2 = GetFolderView2( );
            uint viewMode = 0;
            if( ifv2 != null )
            {
                try
                {
                    HRESULT hr = ifv2.GetCurrentViewMode( out viewMode );
                    if( hr != HRESULT.S_OK )
                        throw Marshal.GetExceptionForHR( (int)hr );
                }
                finally
                {
                    Marshal.ReleaseComObject( ifv2 );
                    ifv2 = null;
                }
            }
            return (FOLDERVIEWMODE)viewMode;
        }
        
        /// <summary>
        /// Gets the IFolderView2 interface from the explorer browser.
        /// </summary>
        /// <returns></returns>
        internal IFolderView2 GetFolderView2( )
        {
            Guid iid = new Guid( ExplorerBrowserIIDGuid.IFolderView2 );
            IntPtr view = IntPtr.Zero;
            if( this.explorerBrowserControl != null )
            {
                HRESULT hr = this.explorerBrowserControl.GetCurrentView( ref iid, out view );
                switch( hr )
                {
                    case HRESULT.S_OK:
                        break;

                    case HRESULT.E_NOINTERFACE:
                    case HRESULT.E_FAIL:
#if LOG_KNOWN_COM_ERRORS
                        Debugger.Log( 2, "ExplorerBrowser", "Unable to obtain view. Error=" + e.ToString( ) );
#endif
                        return null;

                    default:
                        throw new COMException( "ExplorerBrowser failed to get current view.", (int)hr );
                }

                return (IFolderView2)Marshal.GetObjectForIUnknown( view );
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the selected items in the explorer browser as an IShellItemArray
        /// </summary>
        /// <returns></returns>
        internal IShellItemArray GetSelectedItemsArray( )
        {
            IShellItemArray iArray = null;
            IFolderView2 iFV2 = GetFolderView2( );
            if( iFV2 != null )
            {
                try
                {
                    Guid iidShellItemArray = new Guid( ShellIIDGuid.IShellItemArray );
                    object oArray = null;
                    HRESULT hr = iFV2.Items( (uint)SVGIO.SVGIO_SELECTION, ref iidShellItemArray, out oArray );
                    iArray = oArray as IShellItemArray;
                    if( hr != HRESULT.S_OK &&
                        hr != HRESULT.E_ELEMENTNOTFOUND &&
                        hr != HRESULT.E_FAIL )
                    {
                        throw new COMException( "unexpected error retrieving selection", (int)hr );
                    }
                }
                finally
                {
                    Marshal.ReleaseComObject( iFV2 );
                    iFV2 = null;
                }
            }

            return iArray;
        }

        /// <summary>
        /// Gets the items in the ExplorerBrowser as an IShellItemArray
        /// </summary>
        /// <returns></returns>
        internal IShellItemArray GetItemsArray( )
        {
            IShellItemArray iArray = null;
            IFolderView2 iFV2 = GetFolderView2( );
            if( iFV2 != null )
            {
                try
                {
                    Guid iidShellItemArray = new Guid( ShellIIDGuid.IShellItemArray );
                    object oArray = null;
                    HRESULT hr = iFV2.Items( (uint)SVGIO.SVGIO_ALLVIEW, ref iidShellItemArray, out oArray );
                    if( hr != HRESULT.S_OK &&
                        hr != HRESULT.E_FAIL &&
                        hr != HRESULT.E_ELEMENTNOTFOUND &&
                        hr != HRESULT.E_INVALIDARG )
                    {
                        throw new COMException( "unexpected error retrieving view items", (int)hr );
                    }

                    iArray = oArray as IShellItemArray;
                }
                finally
                {
                    Marshal.ReleaseComObject( iFV2 );
                    iFV2 = null;
                }
            }
            return iArray;
        }

        /// <summary>
        /// This does a rigorous test for shell object equality
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        bool ShellObjectsAreEqual( ShellObject first, ShellObject second )
        {
            bool areEqual = false;

            if( first != null && second != null )
            {
                IShellItem ifirst = first.NativeShellItem;
                IShellItem isecond = second.NativeShellItem;
                if( ( ifirst != null ) && ( isecond != null ) )
                {
                    int result = 0;
                    HRESULT hr = ifirst.Compare(
                        isecond, SICHINTF.SICHINT_ALLFIELDS, out result );

                    if( ( hr == HRESULT.S_OK ) && ( result == 0 ) )
                        areEqual = true;
                }
            }

            return areEqual;
        }
        
        
        #endregion

        #endregion
    }

}
