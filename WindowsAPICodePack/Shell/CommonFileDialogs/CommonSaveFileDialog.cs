//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// Creates a Vista or Windows 7 Common File Dialog, allowing the user to select the filename and location for a saved file.
    /// </summary>
    /// <permission cref="System.Security.Permissions.FileDialogPermission">
    /// to save a file. Associated enumeration: <see cref="System.Security.Permissions.SecurityAction.LinkDemand"/>.
    /// </permission>
    [FileDialogPermissionAttribute(SecurityAction.LinkDemand, Save = true)]
    public sealed class CommonSaveFileDialog : CommonFileDialog
    {
        private NativeFileSaveDialog saveDialogCoClass;

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        public CommonSaveFileDialog() : base() { }
        /// <summary>
        /// Creates a new instance of this class with the specified name.
        /// </summary>
        /// <param name="name">The name of this dialog.</param>
        public CommonSaveFileDialog(string name) : base(name) { }

        #region Public API specific to Save

        private bool overwritePrompt;

        /// <summary>
        /// Gets or sets a value that controls whether to prompt before 
        /// overwriting an existing file of the same name. 
        /// </summary>
        /// <permission cref="System.InvalidOperationException">
        /// This property cannot be changed when the dialog is showing.
        /// </permission>
        public bool OverwritePrompt
        {
            get { return overwritePrompt; }
            set 
            {
                ThrowIfDialogShowing("OverwritePrompt" + IllegalPropertyChangeString);
                overwritePrompt = value; 
            }
        }

        private bool createPrompt;
        /// <summary>
        /// Gets or sets a value that controls whether to prompt for creation if the item returned in the save dialog does not exist. 
        /// </summary>
        /// <remarks>Note that this does not actually create the item.</remarks>
        /// <permission cref="System.InvalidOperationException">
        /// This property cannot be changed when the dialog is showing.
        /// </permission>
        public bool CreatePrompt
        {
            get { return createPrompt; }
            set 
            {
                ThrowIfDialogShowing("CreatePrompt" + IllegalPropertyChangeString);
                createPrompt = value; 
            }
        }

        private bool enableMiniMode;
        /// <summary>
        /// Gets or sets a value that controls whether to the save dialog 
        /// displays in expanded mode. 
        /// </summary>
        /// <remarks>Expanded mode controls whether the dialog
        /// shows folders for browsing or hides them.</remarks>
        /// <permission cref="System.InvalidOperationException">
        /// This property cannot be changed when the dialog is showing.
        /// </permission>
        public bool EnableMiniMode
        {
            get { return enableMiniMode; }
            set 
            {
                ThrowIfDialogShowing("EnableMiniMode" + IllegalPropertyChangeString);
                enableMiniMode = value; 
            }
        }

        private bool strictExtensions;
        /// <summary>
        /// Gets or sets a value that controls whether the 
        /// returned file name has a file extension that matches the 
        /// currently selected file type.  If necessary, the dialog appends the correct 
        /// file extension.
        /// </summary>
        /// <permission cref="System.InvalidOperationException">
        /// This property cannot be changed when the dialog is showing.
        /// </permission>
        public bool StrictExtensions
        {
            get { return strictExtensions; }
            set
            {
                ThrowIfDialogShowing("StrictExtensions" + IllegalPropertyChangeString);
                strictExtensions = value;
            }
        }

        /// <summary>
        /// Sets an item to appear as the initial entry in a <b>Save As</b> dialog.
        /// </summary>
        /// <param name="item">The initial entry to be set in the dialog.</param>
        public void SetSaveAsItem(ShellObject item)
        {
            IFileSaveDialog nativeDialog = null;

            if (nativeDialog == null)
            {
                InitializeNativeFileDialog();
                nativeDialog = GetNativeFileDialog() as IFileSaveDialog;
            }

            // Get the native IShellItem from ShellObject
            if (nativeDialog != null)
                nativeDialog.SetSaveAsItem(item.NativeShellItem);
        }

        #endregion

        internal override void InitializeNativeFileDialog()
        {
            if (saveDialogCoClass == null)
                saveDialogCoClass = new NativeFileSaveDialog();
        }
 
        internal override IFileDialog GetNativeFileDialog()
        {
            Debug.Assert(saveDialogCoClass != null,
                "Must call Initialize() before fetching dialog interface");
            return (IFileDialog)saveDialogCoClass;
        }

        internal override void PopulateWithFileNames(
            System.Collections.ObjectModel.Collection<string> names)
        {
            IShellItem item;
            saveDialogCoClass.GetResult(out item);

            if (item == null)
                throw new InvalidOperationException(
                    "Retrieved a null shell item from dialog");
            names.Add(GetFileNameFromShellItem(item));
        }

        internal override void PopulateWithIShellItems(
            System.Collections.ObjectModel.Collection<IShellItem> items)
        {
            IShellItem item;
            saveDialogCoClass.GetResult(out item);

            if (item == null)
                throw new InvalidOperationException(
                    "Retrieved a null shell item from dialog");
            items.Add(item);
        }

        internal override void CleanUpNativeFileDialog()
        {
            if (saveDialogCoClass != null)
                Marshal.ReleaseComObject(saveDialogCoClass);
        }

        internal override ShellNativeMethods.FOS GetDerivedOptionFlags(ShellNativeMethods.FOS flags)
        {
            if (overwritePrompt)
                flags |= ShellNativeMethods.FOS.FOS_OVERWRITEPROMPT;
            if (createPrompt)
                flags |= ShellNativeMethods.FOS.FOS_CREATEPROMPT;
            if (!enableMiniMode)
                flags |= ShellNativeMethods.FOS.FOS_DEFAULTNOMINIMODE;
            if (strictExtensions)
                flags |= ShellNativeMethods.FOS.FOS_STRICTFILETYPES;
            return flags;
        }
    }
}
