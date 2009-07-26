//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.WindowsAPICodePack
{
    internal static class Power
    {
        internal static PowerManagementNativeMethods.SystemPowerCapabilities 
            GetSystemPowerCapabilities()
        {
            IntPtr status = Marshal.AllocCoTaskMem(
                Marshal.SizeOf(typeof(PowerManagementNativeMethods.SystemPowerCapabilities)));

            uint retval = PowerManagementNativeMethods.CallNtPowerInformation(
              4,  // SystemPowerCapabilities
              (IntPtr)null,
              0,
              status,
              (UInt32)Marshal.SizeOf(typeof(PowerManagementNativeMethods.SystemPowerCapabilities))
              );

            if (retval == CoreNativeMethods.STATUS_ACCESS_DENIED)
            {
                throw new UnauthorizedAccessException("The caller had insufficient access rights to get the system power capabilities.");
            }

            PowerManagementNativeMethods.SystemPowerCapabilities powerCap = (PowerManagementNativeMethods.SystemPowerCapabilities)Marshal.PtrToStructure(status, typeof(PowerManagementNativeMethods.SystemPowerCapabilities));
            Marshal.FreeCoTaskMem(status);

            return powerCap;
        }

        internal static PowerManagementNativeMethods.SystemBatteryState GetSystemBatteryState()
        {
            IntPtr status = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(PowerManagementNativeMethods.SystemBatteryState)));
            uint retval = PowerManagementNativeMethods.CallNtPowerInformation(
              5,  // SystemBatteryState
              (IntPtr)null,
              0,
              status,
              (UInt32)Marshal.SizeOf(typeof(PowerManagementNativeMethods.SystemBatteryState))
              );

            if (retval == CoreNativeMethods.STATUS_ACCESS_DENIED) 
            {
                throw new UnauthorizedAccessException("The caller had insufficient access rights to get the system battery state.");
            }

            PowerManagementNativeMethods.SystemBatteryState batt_status = (PowerManagementNativeMethods.SystemBatteryState)Marshal.PtrToStructure(status, typeof(PowerManagementNativeMethods.SystemBatteryState));
            Marshal.FreeCoTaskMem(status);

            return batt_status;
        }

        /// <summary>
        /// Registers the application to receive power setting notifications 
        /// for the specific power setting event.
        /// </summary>
        /// <param name="handle">Handle indicating where the power setting 
        /// notifications are to be sent.</param>
        /// <param name="powerSetting">The GUID of the power setting for 
        /// which notifications are to be sent.</param>
        /// <returns>Returns a notification handle for unregistering 
        /// power notifications.</returns>
        internal static int RegisterPowerSettingNotification(
            IntPtr handle, Guid powerSetting)
        {
            int outHandle = PowerManagementNativeMethods.RegisterPowerSettingNotification(
                handle, 
                ref powerSetting, 
                0);

            return outHandle;
        }

        /// <summary>
        /// Allows an application to inform the system that it 
        /// is in use, thereby preventing the system from entering 
        /// the sleeping power state or turning off the display 
        /// while the application is running.
        /// </summary>
        /// <param name="flags">The thread's execution requirements.</param>
        /// <exception cref="Win32Exception">Thrown if the SetThreadExecutionState call fails.</exception>
        internal static void SetThreadExecutionState(ExecutionState flags)
        {
            ExecutionState? ret = PowerManagementNativeMethods.SetThreadExecutionState(flags);
            if (ret == null)
                throw new Win32Exception("SetThreadExecutionState call failed.");
        }

        internal static void SetScreenSaverActive(bool active)
        {
            PowerManagementNativeMethods.SystemParametersInfoSet(
                PowerManagementNativeMethods.SPI_SETSCREENSAVEACTIVE,
                (UInt32) (active ? 1 : 0), 
                IntPtr.Zero,
                PowerManagementNativeMethods.SPIF_SENDCHANGE |
                    PowerManagementNativeMethods.SPIF_UPDATEINIFILE);
        }
    }
}