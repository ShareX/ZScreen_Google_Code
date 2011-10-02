using System;
using System.Diagnostics;
using HelpersLib;

namespace ZScreenLib.Helpers
{
    public class Freeze : IDisposable
    {
        private uint processId;

        public Freeze(Workflow p, IntPtr windowHandle)
        {
            if (p.ActiveWindowGDIFreezeWindow)
            {
                NativeMethods.GetWindowThreadProcessId(windowHandle, out processId);
                FreezeThreads((int)processId);
            }
        }

        public void Dispose()
        {
            UnfreezeThreads((int)processId);
        }

        public static void FreezeThreads(int intPID)
        {
            if (intPID != 0 && Process.GetCurrentProcess().Id != intPID)
            {
                Process pProc = Process.GetProcessById(intPID);
                if (!string.IsNullOrEmpty(pProc.ProcessName) && pProc.ProcessName != "explorer")
                {
                    foreach (ProcessThread pT in pProc.Threads)
                    {
                        IntPtr ptrOpenThread = NativeMethods.OpenThread(NativeMethods.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);
                        if (ptrOpenThread != null)
                        {
                            NativeMethods.SuspendThread(ptrOpenThread);
                        }
                    }
                }
            }
        }

        public static void UnfreezeThreads(int intPID)
        {
            if (intPID != 0 && Process.GetCurrentProcess().Id != intPID)
            {
                Process pProc = Process.GetProcessById(intPID);
                if (!string.IsNullOrEmpty(pProc.ProcessName))
                {
                    foreach (ProcessThread pT in pProc.Threads)
                    {
                        IntPtr ptrOpenThread = NativeMethods.OpenThread(NativeMethods.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);
                        if (ptrOpenThread != null)
                        {
                            NativeMethods.ResumeThread(ptrOpenThread);
                        }
                    }
                }
            }
        }
    }
}