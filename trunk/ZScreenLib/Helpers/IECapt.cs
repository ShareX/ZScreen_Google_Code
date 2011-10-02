#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v2)

////////////////////////////////////////////////////////////////////
//
// IECapt# - A Internet Explorer Web Page Rendering Capture Utility
//
// Copyright (C) 2007 Bjoern Hoehrmann <bjoern@hoehrmann.de>
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// $Id: IECapt.cs,v 1.4 2008/06/23 10:22:50 hoehrmann Exp $
//
////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AxSHDocVw; // Use `aximp %SystemRoot%\system32\shdocvw.dll`
using IECaptComImports;
using ZScreenLib;
using HelpersLib;

[ComImport, Guid("0000010D-0000-0000-C000-000000000046"), InterfaceType((short)1), ComConversionLoss]
public interface IViewObject
{
    void Draw([MarshalAs(UnmanagedType.U4)] UInt32 dwDrawAspect,
              int lindex,
              IntPtr pvAspect,
              [In] IntPtr ptd,
              IntPtr hdcTargetDev,
              IntPtr hdcDraw,
              [MarshalAs(UnmanagedType.Struct)] ref _RECTL lprcBounds,
              [In] IntPtr lprcWBounds,
              IntPtr pfnContinue,
              [MarshalAs(UnmanagedType.U4)] UInt32 dwContinue);

    void RemoteGetColorSet([In] uint dwDrawAspect, [In] int lindex, [In] uint pvAspect, [In] ref tagDVTARGETDEVICE ptd, [In] uint hicTargetDev, [Out] IntPtr ppColorSet);

    void RemoteFreeze([In] uint dwDrawAspect, [In] int lindex, [In] uint pvAspect, out uint pdwFreeze);

    void Unfreeze([In] uint dwFreeze);

    void SetAdvise([In] uint aspects, [In] uint advf, [In, MarshalAs(UnmanagedType.Interface)] IAdviseSink pAdvSink);

    void RemoteGetAdvise(out uint pAspects, out uint pAdvf, [MarshalAs(UnmanagedType.Interface)] out IAdviseSink ppAdvSink);
}

public class IECapt
{
    public delegate void ImageEventHandler(Image img);
    public event ImageEventHandler ImageCaptured;

    private string url;
    private int width, height, delay;
    private AxWebBrowser mWb;
    private Timer mTimer = new Timer();

    public IECapt(int width, int height, int delay)
    {
        this.width = width;
        this.height = height;
        this.delay = delay;

        mTimer.Interval = delay;
        mTimer.Tick += new EventHandler(mTimer_Tick);
    }

    public void CapturePage(string url)
    {
        this.url = url;

        mWb = new AxWebBrowser();

        mWb.BeginInit();
        mWb.Parent = new Form();
        mWb.EndInit();

        // Set the initial dimensions of the browser's client area.
        mWb.SetBounds(0, 0, width, height);

        object oBlank = "about:blank";
        object oURL = url;
        object oNull = String.Empty;

        // Internet Explorer should show no dialog boxes; this does not dis-
        // able script debugging however, I am not aware of a method to dis-
        // able that, other than manual configuration in he Internet Settings
        // or perhaps the registry.
        mWb.Silent = true;

        // The custom UI handler can only be registered on a document, so we
        // navigate to about:blank as a first step, then register the handler.
        mWb.Navigate2(ref oBlank, ref oNull, ref oNull, ref oNull, ref oNull);

        ICustomDoc cdoc = mWb.Document as ICustomDoc;
        cdoc.SetUIHandler(new IECaptUIHandler());

        // Register a document complete handler. It will be called whenever a
        // document completes loading, including embedded documents and the
        // initial about:blank document.
        mWb.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(IE_DocumentComplete);

        // Register an error handler. If the main document cannot be loaded,
        // the document complete event will not fire, so we have to listen to
        // this and shut the application down in case of a fatal error.
        mWb.NavigateError += new DWebBrowserEvents2_NavigateErrorEventHandler(IE_NavigateError);

        // Now navigate to the final destination.
        mWb.Navigate2(ref oURL, ref oNull, ref oNull, ref oNull, ref oNull);
    }

    private void IE_DocumentComplete(object sender, DWebBrowserEvents2_DocumentCompleteEvent e)
    {
        AxWebBrowser wb = (AxWebBrowser)sender;

        // Skip document complete event for embedded frames.
        if (wb.Application != e.pDisp) return;

        // Skip the initial about:blank document; this is not necessarily
        // the best thing to do, e.g. if the requested page is about:blank
        // or redirects to it, we might never exit. This could be avoided
        // by remembering whether we saw the first document complete event.
        if (e.uRL.Equals("about:blank")) return;

        mTimer.Start();
    }

    private void IE_NavigateError(object sender, DWebBrowserEvents2_NavigateErrorEvent e)
    {
        AxWebBrowser wb = (AxWebBrowser)sender;

        // Ignore errors for embedded documents
        if (wb.Application != e.pDisp) return;

        // If we get here, the main document cannot be navigated
        // to meaning there is nothing to draw, so we just croak.
        StaticHelper.WriteLine("Failed to navigate to {0} (0x{1:X08})", e.uRL, e.statusCode);
        ReportCapture(null);

        wb.Dispose();
    }

    private void mTimer_Tick(object sender, EventArgs e)
    {
        mTimer.Stop();

        try
        {
            DoCapture();
        }
        catch (Exception ex)
        {
            StaticHelper.WriteException(ex);
            ReportCapture(null);
        }

        mWb.Dispose();
    }

    private void DoCapture()
    {
        IHTMLDocument2 doc2 = (IHTMLDocument2)mWb.Document;
        IHTMLDocument3 doc3 = (IHTMLDocument3)mWb.Document;
        IHTMLElement2 body2 = (IHTMLElement2)doc2.body;
        IHTMLElement2 root2 = (IHTMLElement2)doc3.documentElement;

        // Determine dimensions for the image; we could add minWidth here
        // to ensure that we get closer to the minimal width (the width
        // computed might be a few pixels less than what we want).
        int width = Math.Max(body2.scrollWidth, root2.scrollWidth);
        int height = Math.Max(root2.scrollHeight, body2.scrollHeight);

        // Resize the web browser control
        mWb.SetBounds(0, 0, width, height);

        // Do it a second time; in some cases the initial values are
        // off by quite a lot, for as yet unknown reasons. We could
        // also do this in a loop until the values stop changing with
        // some additional terminating condition like n attempts.
        width = Math.Max(body2.scrollWidth, root2.scrollWidth);
        height = Math.Max(root2.scrollHeight, body2.scrollHeight);

        mWb.SetBounds(0, 0, width, height);

        Bitmap image = new Bitmap(width, height);
        Graphics g = Graphics.FromImage(image);

        _RECTL bounds;
        bounds.left = 0;
        bounds.top = 0;
        bounds.right = width;
        bounds.bottom = height;

        IntPtr hdc = g.GetHdc();
        IViewObject iv = doc2 as IViewObject;

        iv.Draw(1, -1, (IntPtr)0, (IntPtr)0, (IntPtr)0, (IntPtr)hdc, ref bounds, (IntPtr)0, (IntPtr)0, 0);

        g.ReleaseHdc(hdc);

        ReportCapture((Image)image.Clone());

        image.Dispose();
    }

    private void ReportCapture(Image img)
    {
        if (ImageCaptured != null)
        {
            ImageCaptured(img);
        }
    }
}

public class IECaptUIHandler : IDocHostUIHandler
{
    public void ShowContextMenu(uint dwID, ref tagPOINT ppt, object pcmdtReserved, object pdispReserved) { }

    public void GetHostInfo(ref _DOCHOSTUIINFO pInfo)
    {
        pInfo.cbSize = (uint)Marshal.SizeOf(pInfo);
        pInfo.dwDoubleClick = 0;
        pInfo.pchHostCss = (IntPtr)0;
        pInfo.pchHostNS = (IntPtr)0;
        pInfo.dwFlags = (uint)(0 | tagDOCHOSTUIFLAG.DOCHOSTUIFLAG_SCROLL_NO | tagDOCHOSTUIFLAG.DOCHOSTUIFLAG_NO3DBORDER
            | tagDOCHOSTUIFLAG.DOCHOSTUIFLAG_NO3DOUTERBORDER);
    }

    public void ShowUI(uint dwID, IOleInPlaceActiveObject pActiveObject, IOleCommandTarget pCommandTarget, IOleInPlaceFrame pFrame, IOleInPlaceUIWindow pDoc) { }

    public void HideUI() { }

    public void UpdateUI() { }

    public void EnableModeless(int fEnable) { }

    public void OnDocWindowActivate(int fActivate) { }

    public void OnFrameWindowActivate(int fActivate) { }

    public void ResizeBorder(ref tagRECT prcBorder, IOleInPlaceUIWindow pUIWindow, int fRameWindow) { }

    public void TranslateAccelerator(ref tagMSG lpmsg, ref Guid pguidCmdGroup, uint nCmdID) { }

    public void GetOptionKeyPath(out string pchKey, uint dw)
    {
        pchKey = null;
    }

    public void GetDropTarget(IECaptComImports.IDropTarget pDropTarget, out IECaptComImports.IDropTarget ppDropTarget)
    {
        ppDropTarget = null;
    }

    public void GetExternal(out object ppDispatch)
    {
        ppDispatch = null;
    }

    public void TranslateUrl(uint dwTranslate, ref ushort pchURLIn, IntPtr ppchURLOut) { }

    public void FilterDataObject(IECaptComImports.IDataObject pDO, out IECaptComImports.IDataObject ppDORet)
    {
        ppDORet = null;
    }
}