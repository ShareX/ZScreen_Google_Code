//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace Microsoft.WindowsAPICodePack
{
    /// <summary>
    /// HRESULT Wrapper
    /// This is intended for Library Internal use only.
    /// </summary>
    public enum HRESULT : uint
    {
        /// <summary>
        /// S_FALSE
        /// </summary>
        S_FALSE = 0x0001,
        
        /// <summary>
        /// S_OK
        /// </summary>
        S_OK = 0x0000,
        
        /// <summary>
        /// E_INVALIDARG
        /// </summary>
        E_INVALIDARG = 0x80070057,
        
        /// <summary>
        /// E_OUTOFMEMORY
        /// </summary>
        E_OUTOFMEMORY = 0x8007000E,

        /// <summary>
        /// E_NOINTERFACE
        /// </summary>
        E_NOINTERFACE = 0x80004002,

        /// <summary>
        /// E_FAIL
        /// </summary>
        E_FAIL = 0x80004005,

        /// <summary>
        /// E_ELEMENTNOTFOUND
        /// </summary>
        E_ELEMENTNOTFOUND = 0x80070490,

        /// <summary>
        /// TYPE_E_ELEMENTNOTFOUND
        /// </summary>
        TYPE_E_ELEMENTNOTFOUND = 0x8002802B,

        /// <summary>
        /// Win32 Error code: ERROR_CANCELLED
        /// </summary>
        ERROR_CANCELLED = 1223,
    }

    /// <summary>
    /// Provide Error Message Helper Methods.
    /// This is intended for Library Internal use only.
    /// </summary>
    public static class CoreErrorHelper
    {
        /// <summary>
        /// This is intended for Library Internal use only.
        /// </summary>
        private const int FACILITY_WIN32 = 7;

        /// <summary>
        /// This is intended for Library Internal use only.
        /// </summary>
        public const int IGNORED = (int)HRESULT.S_OK;

        /// <summary>
        /// This is intended for Library Internal use only.
        /// </summary>
        /// <param name="win32ErrorCode"></param>
        /// <returns></returns>
        public static int HResultFromWin32(int win32ErrorCode)
        {
            if (win32ErrorCode > 0)
            {
                win32ErrorCode =
                    (int)(((uint)win32ErrorCode & 0x0000FFFF) | (FACILITY_WIN32 << 16) | 0x80000000);
            }
            return win32ErrorCode;

        }

        /// <summary>
        /// This is intended for Library Internal use only.
        /// </summary>
        /// <param name="hresult"></param>
        /// <returns></returns>
        public static bool Succeeded(int hresult)
        {
            return (hresult >= 0);
        }

        /// <summary>
        /// This is intended for Library Internal use only.
        /// </summary>
        /// <param name="hresult"></param>
        /// <returns></returns>
        public static bool Failed(HRESULT hresult)
        {
            return ((int)hresult < 0);
        }

        /// <summary>
        /// This is intended for Library Internal use only.
        /// </summary>
        /// <param name="hresult"></param>
        /// <param name="win32ErrorCode"></param>
        /// <returns></returns>
        public static bool Matches(int hresult, int win32ErrorCode)
        {
            return (hresult == HResultFromWin32(win32ErrorCode));
        }


    }
}
