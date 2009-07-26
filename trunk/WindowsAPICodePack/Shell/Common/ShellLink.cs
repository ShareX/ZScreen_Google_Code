//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// Represents a link to existing FileSystem or Virtual item.
    /// </summary>
    public class ShellLink : ShellObject
    {

        //
        private static int MAX_PATH = 260;
        private static uint SLGP_RAWPATH = 0x0004;
        private static PropertyKey PKEY_Title = new PropertyKey(new Guid("F29F85E0-4FF9-1068-AB91-08002B27B3D9"), 2);

        #region Create methods

        /// <summary>
        /// Creates a new ShellLink using the given title, path and icon reference.
        /// </summary>
        /// <param name="title">Title for the shell link</param>
        /// <param name="path">Path or parsing name to which this link points to</param>
        /// <param name="iconReference">Icon reference (module name and resource id) for the link's icon</param>
        /// <returns>ShellLink object that points to the given path/parsing name</returns>
        public static ShellLink Create(string title, string path, IconReference iconReference)
        {
            ShellLink shellLink = new ShellLink(path);
            shellLink.Title = title;
            shellLink.IconReference = iconReference;

            return shellLink;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Path for this file e.g. c:\Windows\file.txt,
        /// </summary>
        private string internalPath = null;

        /// <summary>
        /// Represents the native IShellLinkW object
        /// </summary>
        private IShellLinkW internalShellLink = null;

        #endregion

        #region Internal Constructors

        internal ShellLink(IShellItem2 shellItem)
        {
            // If constructed using this ctor, Path/IShellItem/IShellLink
            // will all be set in this method. Therefore all the IShellItem specific features
            // (thumbnail, properties, etc) will work, as well as all the IShellLink specific
            // features will also work.

            nativeShellItem = shellItem;

            // Create IShellLink using the target parsing name of this IShellItem (shortcut/lnk file)
            Path = this.Properties.System.ParsingPath.Value;
            TargetLocation = this.Properties.System.Link.TargetParsingPath.Value;

            if (!string.IsNullOrEmpty(TargetLocation))
            {
                internalShellLink = (IShellLinkW)(new CShellLink());
                internalShellLink.SetPath(TargetLocation);
            }
        }

        internal ShellLink(string targetParsingName)
        {
            // If constructed using this ctor, Path/NativeShellItem
            // will be null. Shell Properties and other things related to IShellItem
            // will not be accessible.

            TargetLocation = targetParsingName;

            internalShellLink = (IShellLinkW)(new CShellLink());
            internalShellLink.SetPath(TargetLocation);

        }

        #endregion

        #region Internal Properties

        internal IShellLinkW NativeShellLink
        {
            get
            {
                if (internalShellLink == null)
                {
                    internalShellLink = (IShellLinkW)(new CShellLink());
                }

                return internalShellLink;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The path for this link
        /// </summary>
        virtual public string Path
        {
            get
            {
                if (internalPath == null && NativeShellItem != null)
                {
                    internalPath = ShellHelper.GetParsingName(NativeShellItem);
                }
                return internalPath;
            }
            protected set
            {
                this.internalPath = value;
            }
        }

        private string internalTargetLocation;
        /// <summary>
        /// Gets the location to which this link points to.
        /// </summary>
        public string TargetLocation
        {
            get
            {
                if (string.IsNullOrEmpty(internalTargetLocation))
                {
                    if (NativeShellLink != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.EnsureCapacity(MAX_PATH);

                        NativeShellLink.GetPath(sb, MAX_PATH, IntPtr.Zero, SLGP_RAWPATH);
                    }
                    else if (NativeShellItem2 != null)
                        internalTargetLocation = this.Properties.System.Link.TargetParsingPath.Value;
                }

                return internalTargetLocation;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                internalTargetLocation = value;

                if (NativeShellLink != null)
                    NativeShellLink.SetPath(value);
                else if (NativeShellItem2 != null)
                    this.Properties.System.Link.TargetParsingPath.Value = internalTargetLocation;
            }
        }

        /// <summary>
        /// Gets the ShellObject to which this link points to.
        /// </summary>
        public ShellObject TargetObject
        {
            get
            {
                return ShellObjectFactory.Create(TargetLocation);
            }
        }

        /// <summary>
        /// Gets or sets the link's title
        /// </summary>
        public string Title
        {
            get
            {
                if (NativeShellItem2 != null)
                    return this.Properties.System.Title.Value;
                else
                    return null;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                if (NativeShellItem2 != null)
                    this.Properties.System.Title.Value = value;
                else if (NativeShellLink != null)
                {
                    // We can't use the property system as IShellItem is null.
                    // Use the native interface directly (IPropertyStore and PropVar)
                    IPropertyStore propertyStore = (IPropertyStore)NativeShellLink;
                    PropVariant propVariant = new PropVariant();

                    propVariant.SetString(value);
                    propertyStore.SetValue(ref PKEY_Title, ref propVariant);
                    propVariant.Clear();

                    propertyStore.Commit();
                }
            }
        }

        /// <summary>
        /// Gets or sets the icon reference (location and index) of the link's icon.
        /// </summary>
        public IconReference IconReference
        {
            get
            {
                if (NativeShellLink != null)
                {
                    StringBuilder iconPath;
                    int iconIndex;

                    NativeShellLink.GetIconLocation(out iconPath, MAX_PATH, out iconIndex);

                    return new IconReference(iconPath.ToString(), iconIndex);
                }
                else
                    return new IconReference();
            }
            set
            {
                if (NativeShellLink != null)
                    NativeShellLink.SetIconLocation(value.ModuleName, value.ResourceId);
            }
        }

        /// <summary>
        /// Gets or sets the object's arguments (passed to the command line).
        /// </summary>
        public string Arguments
        {
            get
            {
                if (NativeShellLink != null)
                {
                    StringBuilder args = new StringBuilder();
                    args.EnsureCapacity(MAX_PATH);

                    NativeShellLink.GetArguments(args, MAX_PATH);
                    return args.ToString();
                }
                else
                    return null;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                if (NativeShellLink != null)
                    NativeShellLink.SetArguments(value);
            }
        }

        /// <summary>
        /// Gets or sets the object's working directory.
        /// </summary>
        public string WorkingDirectory
        {
            get
            {
                if (NativeShellLink != null)
                {
                    StringBuilder workingDir = new StringBuilder();
                    workingDir.EnsureCapacity(MAX_PATH);
                    NativeShellLink.GetWorkingDirectory(workingDir, MAX_PATH);
                    return workingDir.ToString();
                }
                else
                    return null;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                if (NativeShellLink != null)
                    NativeShellLink.SetWorkingDirectory(value);
            }
        }

        /// <summary>
        /// Gets or sets the show command of the lauched application.
        /// </summary>
        public WindowShowCommand ShowCommand
        {
            get
            {
                if (NativeShellLink != null)
                {
                    uint showCommand;
                    NativeShellLink.GetShowCmd(out showCommand);
                    return (WindowShowCommand)showCommand;
                }
                else
                    return WindowShowCommand.Default;
            }
            set
            {
                if (NativeShellLink != null)
                    NativeShellLink.SetShowCmd((uint)value);
            }
        }


        #endregion

    }
}
