using System;
using System.Windows.Forms;
using HelpersLib;

namespace ZScreenCoreLib
{
    public partial class FileNamingUI : Form
    {
        #region 0 Properties

        private TextBox mHadFocus;
        private int mHadFocusAt;
        public FileNamingConfig Config = new FileNamingConfig();

        #endregion 0 Properties

        #region 1 Constructor

        public FileNamingUI(FileNamingConfig config)
        {
            InitializeComponent();
            this.Config = config;
        }

        #endregion 1 Constructor

        private void btnCodes_Click(object sender, EventArgs e)
        {
            var b = (Button)sender;

            const string beginning = "btnCodes";
            string name = b.Name, code;

            if (name.Contains(beginning))
            {
                name = name.Replace(beginning, string.Empty);
                code = "%" + name.ToLower();

                if (mHadFocus != null)
                {
                    mHadFocus.Text = mHadFocus.Text.Insert(mHadFocusAt, code);
                    mHadFocus.Focus();
                    mHadFocus.Select(mHadFocusAt + code.Length, 0);
                }
            }
        }

        private void chkOverwriteFiles_CheckedChanged(object sender, EventArgs e)
        {
            Config.OverwriteFiles = chkOverwriteFiles.Checked;
        }

        private void nudMaxNameLength_ValueChanged(object sender, EventArgs e)
        {
            Config.MaxNameLength = (int)nudMaxNameLength.Value;
        }

        private void txtEntireScreen_TextChanged(object sender, EventArgs e)
        {
            Config.EntireScreenPattern = txtEntireScreen.Text;
            var parser = new NameParser(NameParserType.EntireScreen)
                             {
                                 CustomProductName = Application.ProductName,
                                 IsPreview = true,
                                 MaxNameLength = Config.MaxNameLength
                             };
            lblEntireScreenPreview.Text = parser.Convert(Config.EntireScreenPattern);
        }

        private void txtEntireScreen_Leave(object sender, EventArgs e)
        {
            mHadFocus = (TextBox)sender;
            mHadFocusAt = ((TextBox)sender).SelectionStart;
        }

        private void btnResetIncrement_Click(object sender, EventArgs e)
        {
            Config.AutoIncrement = 0;
        }

        private void txtActiveWindow_TextChanged(object sender, EventArgs e)
        {
            Config.ActiveWindowPattern = txtActiveWindow.Text;
            var parser = new NameParser(NameParserType.ActiveWindow)
                             {
                                 CustomProductName = Application.ProductName,
                                 IsPreview = true,
                                 MaxNameLength = Config.MaxNameLength
                             };
            lblActiveWindowPreview.Text = parser.Convert(Config.ActiveWindowPattern);
        }

        private void txtActiveWindow_Leave(object sender, EventArgs e)
        {
            mHadFocus = (TextBox)sender;
            mHadFocusAt = ((TextBox)sender).SelectionStart;
        }

        private void FileNamingUI_Load(object sender, EventArgs e)
        {
            txtActiveWindow.Text = Config.ActiveWindowPattern;
            txtEntireScreen.Text = Config.EntireScreenPattern;
            nudMaxNameLength.Value = Config.MaxNameLength;
            chkOverwriteFiles.Checked = Config.OverwriteFiles;

            this.Text = "File Naming Configurator - " + Application.ProductName;
        }
    }
}