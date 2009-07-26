//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// An ennumerable collection of ShellObjects
    /// </summary>
    public class ShellObjectCollection : IEnumerable, IDisposable
    {
        private IShellItemArray iContent = null;
        
        #region construction/disposal/finialization
        /// <summary>
        /// Creates a ShellObject collection from an IShellItemArray
        /// </summary>
        /// <param name="iArray"></param>
        internal ShellObjectCollection( IShellItemArray iArray )
        {
            iContent = iArray;
        }

        bool isDisposed = false;
        /// <summary>
        /// Standard Dispose pattern
        /// </summary>
        public void Dispose( )
        {
            Dispose( true );
        }

        /// <summary>
        /// Standard Dispose patterns 
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose( bool disposing )
        {
            if( isDisposed == false )
            {
                if( disposing )
                {
                    // managed cleanup
                }

                // unmanaged cleanup
                if( iContent != null )
                {
                    Marshal.ReleaseComObject( iContent );
                    iContent = null;
                }

                isDisposed = true;
            }
        }

        /// <summary>
        /// Ensures the native object cleanup occurs
        /// </summary>
        ~ShellObjectCollection( )
        {
            Dispose( false );
        }
        #endregion

        #region implementation
        /// <summary>
        /// Item count
        /// </summary>
        public uint Count
        {
            get
            {
                uint items = 0;
                if( iContent != null )
                    iContent.GetCount( out items );
                return items;
            }
        }
        
        /// <summary>
        /// Collection indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1043:UseIntegralOrStringArgumentForIndexers", Justification="UInt is what we pass to the native API, so can't use int")]
        public ShellObject this[ uint index ]
        {
            get
            {
                IShellItem item = null;
                if( iContent != null )
                    iContent.GetItemAt( index, out item  );
                return ShellObjectFactory.Create( item );
            }
        }

        /// <summary>
        /// Collection enumeration
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            uint items = 0;
            if( iContent != null )
                iContent.GetCount( out items );
            for( uint index = 0; index < items; index++ )
            {
                yield return this[index];
            }
        }
        #endregion
    }
}
