using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ZScreenLib.Helpers
{
    public class Freeze : IDisposable
    {
        private uint processId;

        public Freeze(IntPtr windowHandle)
        {
            if (Engine.conf.ActiveWindowGDIFreezeWindow)
            {
                User32.GetWindowThreadProcessId(windowHandle, out processId);
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
                if (pProc.ProcessName != "explorer")
                {
                    if (!string.IsNullOrEmpty(pProc.ProcessName))
                    {
                        foreach (ProcessThread pT in pProc.Threads)
                        {
                            IntPtr ptrOpenThread = User32.OpenThread(User32.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);
                            if (ptrOpenThread != null)
                            {
                                User32.SuspendThread(ptrOpenThread);
                            }
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
                        IntPtr ptrOpenThread = User32.OpenThread(User32.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);
                        if (ptrOpenThread != null)
                        {
                            User32.ResumeThread(ptrOpenThread);
                        }
                    }
                }
            }
        }
    }
}