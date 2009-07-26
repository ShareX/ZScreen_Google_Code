//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// Defines the class of commonly used file filters.
    /// </summary>
    public static class CommonFileDialogStandardFilters
    {
        private static CommonFileDialogFilter textFilesFilter;
        /// <summary>
        /// Gets a value that specifies the filter for *.txt files.
        /// </summary>
        public static CommonFileDialogFilter TextFiles
        {
            get
            {
                if (textFilesFilter == null)
                    textFilesFilter = new CommonFileDialogFilter("Text Files", "*.txt");
                return textFilesFilter;
            }
        }

        private static CommonFileDialogFilter officeFilesFilter;
        /// <summary>
        /// Gets a value that specifies the filter for Microsoft Word and Excel files.
        /// </summary>
        public static CommonFileDialogFilter OfficeFiles
        {
            get
            {
                if (officeFilesFilter == null)
                    officeFilesFilter = new CommonFileDialogFilter("Office Files", "*.doc, *.xls, *.docx");
                return officeFilesFilter;
            }
        }
    }
}
