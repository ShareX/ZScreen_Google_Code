//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// Stores the result of displaying a dialog.
    /// </summary>
    public class CommonFileDialogResult
    {
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="canceled">The starting value for the <see cref="Microsoft.WindowsAPICodePack.Shell.CommonFileDialogResult.Canceled"/> property.</param>
        public CommonFileDialogResult(bool canceled) 
        {
            this.canceled = canceled;
        }

        private bool canceled;
        /// <summary>
        /// Gets a value that indicates if the end user canceled the dialog.
        /// </summary>
        public bool Canceled
        {
            get { return canceled; }
        }

    }
}
