namespace UploadersLib
{
    partial class UploadersConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcUploaders = new System.Windows.Forms.TabControl();
            this.tpImageUploaders = new System.Windows.Forms.TabPage();
            this.tcImageUploaders = new System.Windows.Forms.TabControl();
            this.tpImageShack = new System.Windows.Forms.TabPage();
            this.btnImageShackOpenPublicProfile = new System.Windows.Forms.Button();
            this.cbImageShackIsPublic = new System.Windows.Forms.CheckBox();
            this.btnImageShackOpenMyImages = new System.Windows.Forms.Button();
            this.lblImageShackUsername = new System.Windows.Forms.Label();
            this.btnImageShackOpenRegistrationCode = new System.Windows.Forms.Button();
            this.txtImageShackUsername = new System.Windows.Forms.TextBox();
            this.txtImageShackRegistrationCode = new System.Windows.Forms.TextBox();
            this.lblImageShackRegistrationCode = new System.Windows.Forms.Label();
            this.tpTinyPic = new System.Windows.Forms.TabPage();
            this.btnTinyPicLogin = new System.Windows.Forms.Button();
            this.txtTinyPicPassword = new System.Windows.Forms.TextBox();
            this.lblTinyPicPassword = new System.Windows.Forms.Label();
            this.txtTinyPicUsername = new System.Windows.Forms.TextBox();
            this.lblTinyPicUsername = new System.Windows.Forms.Label();
            this.btnTinyPicOpenMyImages = new System.Windows.Forms.Button();
            this.cbTinyPicRememberUsernamePassword = new System.Windows.Forms.CheckBox();
            this.lblTinyPicRegistrationCode = new System.Windows.Forms.Label();
            this.txtTinyPicRegistrationCode = new System.Windows.Forms.TextBox();
            this.tpImgur = new System.Windows.Forms.TabPage();
            this.gbImgurUserAccount = new System.Windows.Forms.GroupBox();
            this.btnImgurOpenAuthorizePage = new System.Windows.Forms.Button();
            this.lblImgurVerificationCode = new System.Windows.Forms.Label();
            this.btnImgurEnterVerificationCode = new System.Windows.Forms.Button();
            this.txtImgurVerificationCode = new System.Windows.Forms.TextBox();
            this.lblImgurAccountStatus = new System.Windows.Forms.Label();
            this.cbImgurUseUserAccount = new System.Windows.Forms.CheckBox();
            this.tpFlickr = new System.Windows.Forms.TabPage();
            this.tpTwitPic = new System.Windows.Forms.TabPage();
            this.tpTwitSnaps = new System.Windows.Forms.TabPage();
            this.lblTwitSnapsTip = new System.Windows.Forms.Label();
            this.tpYFrog = new System.Windows.Forms.TabPage();
            this.tpMediaWiki = new System.Windows.Forms.TabPage();
            this.tpDekiWiki = new System.Windows.Forms.TabPage();
            this.tpFileUploaders = new System.Windows.Forms.TabPage();
            this.tcFileUploaders = new System.Windows.Forms.TabControl();
            this.tpFTP = new System.Windows.Forms.TabPage();
            this.tpRapidShare = new System.Windows.Forms.TabPage();
            this.tpDropbox = new System.Windows.Forms.TabPage();
            this.tpSendSpace = new System.Windows.Forms.TabPage();
            this.tpCustomUploaders = new System.Windows.Forms.TabPage();
            this.tpTextUploaders = new System.Windows.Forms.TabPage();
            this.tcTextUploaders = new System.Windows.Forms.TabControl();
            this.tpPastebin = new System.Windows.Forms.TabPage();
            this.tpURLShorteners = new System.Windows.Forms.TabPage();
            this.tcURLShorteners = new System.Windows.Forms.TabControl();
            this.tpURLShortenerPage1 = new System.Windows.Forms.TabPage();
            this.tpOtherServices = new System.Windows.Forms.TabPage();
            this.tcOtherServices = new System.Windows.Forms.TabControl();
            this.tpTwitter = new System.Windows.Forms.TabPage();
            this.tcUploaders.SuspendLayout();
            this.tpImageUploaders.SuspendLayout();
            this.tcImageUploaders.SuspendLayout();
            this.tpImageShack.SuspendLayout();
            this.tpTinyPic.SuspendLayout();
            this.tpImgur.SuspendLayout();
            this.gbImgurUserAccount.SuspendLayout();
            this.tpTwitSnaps.SuspendLayout();
            this.tpFileUploaders.SuspendLayout();
            this.tcFileUploaders.SuspendLayout();
            this.tpTextUploaders.SuspendLayout();
            this.tcTextUploaders.SuspendLayout();
            this.tpURLShorteners.SuspendLayout();
            this.tcURLShorteners.SuspendLayout();
            this.tpOtherServices.SuspendLayout();
            this.tcOtherServices.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcUploaders
            // 
            this.tcUploaders.Controls.Add(this.tpImageUploaders);
            this.tcUploaders.Controls.Add(this.tpFileUploaders);
            this.tcUploaders.Controls.Add(this.tpTextUploaders);
            this.tcUploaders.Controls.Add(this.tpURLShorteners);
            this.tcUploaders.Controls.Add(this.tpOtherServices);
            this.tcUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcUploaders.Location = new System.Drawing.Point(3, 3);
            this.tcUploaders.Name = "tcUploaders";
            this.tcUploaders.SelectedIndex = 0;
            this.tcUploaders.Size = new System.Drawing.Size(856, 522);
            this.tcUploaders.TabIndex = 0;
            // 
            // tpImageUploaders
            // 
            this.tpImageUploaders.Controls.Add(this.tcImageUploaders);
            this.tpImageUploaders.Location = new System.Drawing.Point(4, 22);
            this.tpImageUploaders.Name = "tpImageUploaders";
            this.tpImageUploaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageUploaders.Size = new System.Drawing.Size(848, 496);
            this.tpImageUploaders.TabIndex = 0;
            this.tpImageUploaders.Text = "Image uploaders";
            this.tpImageUploaders.UseVisualStyleBackColor = true;
            // 
            // tcImageUploaders
            // 
            this.tcImageUploaders.Controls.Add(this.tpImageShack);
            this.tcImageUploaders.Controls.Add(this.tpTinyPic);
            this.tcImageUploaders.Controls.Add(this.tpImgur);
            this.tcImageUploaders.Controls.Add(this.tpFlickr);
            this.tcImageUploaders.Controls.Add(this.tpTwitPic);
            this.tcImageUploaders.Controls.Add(this.tpTwitSnaps);
            this.tcImageUploaders.Controls.Add(this.tpYFrog);
            this.tcImageUploaders.Controls.Add(this.tpMediaWiki);
            this.tcImageUploaders.Controls.Add(this.tpDekiWiki);
            this.tcImageUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcImageUploaders.Location = new System.Drawing.Point(3, 3);
            this.tcImageUploaders.Name = "tcImageUploaders";
            this.tcImageUploaders.SelectedIndex = 0;
            this.tcImageUploaders.Size = new System.Drawing.Size(842, 490);
            this.tcImageUploaders.TabIndex = 0;
            // 
            // tpImageShack
            // 
            this.tpImageShack.Controls.Add(this.btnImageShackOpenPublicProfile);
            this.tpImageShack.Controls.Add(this.cbImageShackIsPublic);
            this.tpImageShack.Controls.Add(this.btnImageShackOpenMyImages);
            this.tpImageShack.Controls.Add(this.lblImageShackUsername);
            this.tpImageShack.Controls.Add(this.btnImageShackOpenRegistrationCode);
            this.tpImageShack.Controls.Add(this.txtImageShackUsername);
            this.tpImageShack.Controls.Add(this.txtImageShackRegistrationCode);
            this.tpImageShack.Controls.Add(this.lblImageShackRegistrationCode);
            this.tpImageShack.Location = new System.Drawing.Point(4, 22);
            this.tpImageShack.Name = "tpImageShack";
            this.tpImageShack.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageShack.Size = new System.Drawing.Size(834, 464);
            this.tpImageShack.TabIndex = 0;
            this.tpImageShack.Text = "ImageShack";
            this.tpImageShack.UseVisualStyleBackColor = true;
            // 
            // btnImageShackOpenPublicProfile
            // 
            this.btnImageShackOpenPublicProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImageShackOpenPublicProfile.Location = new System.Drawing.Point(16, 208);
            this.btnImageShackOpenPublicProfile.Name = "btnImageShackOpenPublicProfile";
            this.btnImageShackOpenPublicProfile.Size = new System.Drawing.Size(200, 23);
            this.btnImageShackOpenPublicProfile.TabIndex = 6;
            this.btnImageShackOpenPublicProfile.Text = "Open public profile page...";
            this.btnImageShackOpenPublicProfile.UseVisualStyleBackColor = true;
            this.btnImageShackOpenPublicProfile.Click += new System.EventHandler(this.btnImageShackOpenPublicProfile_Click);
            // 
            // cbImageShackIsPublic
            // 
            this.cbImageShackIsPublic.AutoSize = true;
            this.cbImageShackIsPublic.Location = new System.Drawing.Point(16, 136);
            this.cbImageShackIsPublic.Name = "cbImageShackIsPublic";
            this.cbImageShackIsPublic.Size = new System.Drawing.Size(307, 17);
            this.cbImageShackIsPublic.TabIndex = 3;
            this.cbImageShackIsPublic.Text = "Show images uploaded to ImageShack in your public profile";
            this.cbImageShackIsPublic.UseVisualStyleBackColor = true;
            // 
            // btnImageShackOpenMyImages
            // 
            this.btnImageShackOpenMyImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImageShackOpenMyImages.Location = new System.Drawing.Point(16, 240);
            this.btnImageShackOpenMyImages.Name = "btnImageShackOpenMyImages";
            this.btnImageShackOpenMyImages.Size = new System.Drawing.Size(200, 23);
            this.btnImageShackOpenMyImages.TabIndex = 3;
            this.btnImageShackOpenMyImages.Text = "Open my images page...";
            this.btnImageShackOpenMyImages.UseVisualStyleBackColor = true;
            this.btnImageShackOpenMyImages.Click += new System.EventHandler(this.btnImageShackOpenMyImages_Click);
            // 
            // lblImageShackUsername
            // 
            this.lblImageShackUsername.AutoSize = true;
            this.lblImageShackUsername.Location = new System.Drawing.Point(16, 72);
            this.lblImageShackUsername.Name = "lblImageShackUsername";
            this.lblImageShackUsername.Size = new System.Drawing.Size(242, 13);
            this.lblImageShackUsername.TabIndex = 5;
            this.lblImageShackUsername.Text = "Username (to be able to open public profile page):";
            // 
            // btnImageShackOpenRegistrationCode
            // 
            this.btnImageShackOpenRegistrationCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImageShackOpenRegistrationCode.Location = new System.Drawing.Point(16, 176);
            this.btnImageShackOpenRegistrationCode.Name = "btnImageShackOpenRegistrationCode";
            this.btnImageShackOpenRegistrationCode.Size = new System.Drawing.Size(200, 23);
            this.btnImageShackOpenRegistrationCode.TabIndex = 2;
            this.btnImageShackOpenRegistrationCode.Text = "Open registration code page...";
            this.btnImageShackOpenRegistrationCode.UseVisualStyleBackColor = true;
            this.btnImageShackOpenRegistrationCode.Click += new System.EventHandler(this.btnImageShackOpenRegistrationCode_Click);
            // 
            // txtImageShackUsername
            // 
            this.txtImageShackUsername.Location = new System.Drawing.Point(16, 96);
            this.txtImageShackUsername.Name = "txtImageShackUsername";
            this.txtImageShackUsername.Size = new System.Drawing.Size(360, 20);
            this.txtImageShackUsername.TabIndex = 4;
            this.txtImageShackUsername.TextChanged += new System.EventHandler(this.txtImageShackUsername_TextChanged);
            // 
            // txtImageShackRegistrationCode
            // 
            this.txtImageShackRegistrationCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImageShackRegistrationCode.Location = new System.Drawing.Point(16, 40);
            this.txtImageShackRegistrationCode.Name = "txtImageShackRegistrationCode";
            this.txtImageShackRegistrationCode.Size = new System.Drawing.Size(360, 20);
            this.txtImageShackRegistrationCode.TabIndex = 0;
            this.txtImageShackRegistrationCode.TextChanged += new System.EventHandler(this.txtImageShackRegistrationCode_TextChanged);
            // 
            // lblImageShackRegistrationCode
            // 
            this.lblImageShackRegistrationCode.AutoSize = true;
            this.lblImageShackRegistrationCode.Location = new System.Drawing.Point(16, 16);
            this.lblImageShackRegistrationCode.Name = "lblImageShackRegistrationCode";
            this.lblImageShackRegistrationCode.Size = new System.Drawing.Size(93, 13);
            this.lblImageShackRegistrationCode.TabIndex = 1;
            this.lblImageShackRegistrationCode.Text = "Registration code:";
            // 
            // tpTinyPic
            // 
            this.tpTinyPic.Controls.Add(this.btnTinyPicLogin);
            this.tpTinyPic.Controls.Add(this.txtTinyPicPassword);
            this.tpTinyPic.Controls.Add(this.lblTinyPicPassword);
            this.tpTinyPic.Controls.Add(this.txtTinyPicUsername);
            this.tpTinyPic.Controls.Add(this.lblTinyPicUsername);
            this.tpTinyPic.Controls.Add(this.btnTinyPicOpenMyImages);
            this.tpTinyPic.Controls.Add(this.cbTinyPicRememberUsernamePassword);
            this.tpTinyPic.Controls.Add(this.lblTinyPicRegistrationCode);
            this.tpTinyPic.Controls.Add(this.txtTinyPicRegistrationCode);
            this.tpTinyPic.Location = new System.Drawing.Point(4, 22);
            this.tpTinyPic.Name = "tpTinyPic";
            this.tpTinyPic.Padding = new System.Windows.Forms.Padding(3);
            this.tpTinyPic.Size = new System.Drawing.Size(834, 464);
            this.tpTinyPic.TabIndex = 1;
            this.tpTinyPic.Text = "TinyPic";
            this.tpTinyPic.UseVisualStyleBackColor = true;
            // 
            // btnTinyPicLogin
            // 
            this.btnTinyPicLogin.Location = new System.Drawing.Point(16, 128);
            this.btnTinyPicLogin.Name = "btnTinyPicLogin";
            this.btnTinyPicLogin.Size = new System.Drawing.Size(80, 23);
            this.btnTinyPicLogin.TabIndex = 13;
            this.btnTinyPicLogin.Text = "Login";
            this.btnTinyPicLogin.UseVisualStyleBackColor = true;
            this.btnTinyPicLogin.Click += new System.EventHandler(this.btnTinyPicLogin_Click);
            // 
            // txtTinyPicPassword
            // 
            this.txtTinyPicPassword.Location = new System.Drawing.Point(16, 96);
            this.txtTinyPicPassword.Name = "txtTinyPicPassword";
            this.txtTinyPicPassword.PasswordChar = '*';
            this.txtTinyPicPassword.Size = new System.Drawing.Size(360, 20);
            this.txtTinyPicPassword.TabIndex = 12;
            this.txtTinyPicPassword.TextChanged += new System.EventHandler(this.txtTinyPicPassword_TextChanged);
            // 
            // lblTinyPicPassword
            // 
            this.lblTinyPicPassword.AutoSize = true;
            this.lblTinyPicPassword.Location = new System.Drawing.Point(16, 72);
            this.lblTinyPicPassword.Name = "lblTinyPicPassword";
            this.lblTinyPicPassword.Size = new System.Drawing.Size(56, 13);
            this.lblTinyPicPassword.TabIndex = 11;
            this.lblTinyPicPassword.Text = "Password:";
            // 
            // txtTinyPicUsername
            // 
            this.txtTinyPicUsername.Location = new System.Drawing.Point(16, 40);
            this.txtTinyPicUsername.Name = "txtTinyPicUsername";
            this.txtTinyPicUsername.Size = new System.Drawing.Size(360, 20);
            this.txtTinyPicUsername.TabIndex = 10;
            this.txtTinyPicUsername.TextChanged += new System.EventHandler(this.txtTinyPicUsername_TextChanged);
            // 
            // lblTinyPicUsername
            // 
            this.lblTinyPicUsername.AutoSize = true;
            this.lblTinyPicUsername.Location = new System.Drawing.Point(16, 16);
            this.lblTinyPicUsername.Name = "lblTinyPicUsername";
            this.lblTinyPicUsername.Size = new System.Drawing.Size(58, 13);
            this.lblTinyPicUsername.TabIndex = 9;
            this.lblTinyPicUsername.Text = "Username:";
            // 
            // btnTinyPicOpenMyImages
            // 
            this.btnTinyPicOpenMyImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTinyPicOpenMyImages.Location = new System.Drawing.Point(16, 224);
            this.btnTinyPicOpenMyImages.Name = "btnTinyPicOpenMyImages";
            this.btnTinyPicOpenMyImages.Size = new System.Drawing.Size(200, 23);
            this.btnTinyPicOpenMyImages.TabIndex = 8;
            this.btnTinyPicOpenMyImages.Text = "Open my images page...";
            this.btnTinyPicOpenMyImages.UseVisualStyleBackColor = true;
            this.btnTinyPicOpenMyImages.Click += new System.EventHandler(this.btnTinyPicOpenMyImages_Click);
            // 
            // cbTinyPicRememberUsernamePassword
            // 
            this.cbTinyPicRememberUsernamePassword.AutoSize = true;
            this.cbTinyPicRememberUsernamePassword.Location = new System.Drawing.Point(112, 131);
            this.cbTinyPicRememberUsernamePassword.Name = "cbTinyPicRememberUsernamePassword";
            this.cbTinyPicRememberUsernamePassword.Size = new System.Drawing.Size(233, 17);
            this.cbTinyPicRememberUsernamePassword.TabIndex = 8;
            this.cbTinyPicRememberUsernamePassword.Text = "Remember TinyPic username and password";
            this.cbTinyPicRememberUsernamePassword.UseVisualStyleBackColor = true;
            this.cbTinyPicRememberUsernamePassword.CheckedChanged += new System.EventHandler(this.cbTinyPicRememberUsernamePassword_CheckedChanged);
            // 
            // lblTinyPicRegistrationCode
            // 
            this.lblTinyPicRegistrationCode.AutoSize = true;
            this.lblTinyPicRegistrationCode.Location = new System.Drawing.Point(16, 168);
            this.lblTinyPicRegistrationCode.Name = "lblTinyPicRegistrationCode";
            this.lblTinyPicRegistrationCode.Size = new System.Drawing.Size(335, 13);
            this.lblTinyPicRegistrationCode.TabIndex = 4;
            this.lblTinyPicRegistrationCode.Text = "Registration code (You must login for be able to get registration code):";
            // 
            // txtTinyPicRegistrationCode
            // 
            this.txtTinyPicRegistrationCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTinyPicRegistrationCode.BackColor = System.Drawing.Color.White;
            this.txtTinyPicRegistrationCode.Location = new System.Drawing.Point(16, 192);
            this.txtTinyPicRegistrationCode.Name = "txtTinyPicRegistrationCode";
            this.txtTinyPicRegistrationCode.ReadOnly = true;
            this.txtTinyPicRegistrationCode.Size = new System.Drawing.Size(360, 20);
            this.txtTinyPicRegistrationCode.TabIndex = 3;
            // 
            // tpImgur
            // 
            this.tpImgur.Controls.Add(this.gbImgurUserAccount);
            this.tpImgur.Controls.Add(this.cbImgurUseUserAccount);
            this.tpImgur.Location = new System.Drawing.Point(4, 22);
            this.tpImgur.Name = "tpImgur";
            this.tpImgur.Padding = new System.Windows.Forms.Padding(3);
            this.tpImgur.Size = new System.Drawing.Size(834, 464);
            this.tpImgur.TabIndex = 2;
            this.tpImgur.Text = "Imgur";
            this.tpImgur.UseVisualStyleBackColor = true;
            // 
            // gbImgurUserAccount
            // 
            this.gbImgurUserAccount.Controls.Add(this.btnImgurOpenAuthorizePage);
            this.gbImgurUserAccount.Controls.Add(this.lblImgurVerificationCode);
            this.gbImgurUserAccount.Controls.Add(this.btnImgurEnterVerificationCode);
            this.gbImgurUserAccount.Controls.Add(this.txtImgurVerificationCode);
            this.gbImgurUserAccount.Controls.Add(this.lblImgurAccountStatus);
            this.gbImgurUserAccount.Location = new System.Drawing.Point(16, 48);
            this.gbImgurUserAccount.Name = "gbImgurUserAccount";
            this.gbImgurUserAccount.Size = new System.Drawing.Size(392, 192);
            this.gbImgurUserAccount.TabIndex = 6;
            this.gbImgurUserAccount.TabStop = false;
            this.gbImgurUserAccount.Text = "User account";
            // 
            // btnImgurOpenAuthorizePage
            // 
            this.btnImgurOpenAuthorizePage.Location = new System.Drawing.Point(16, 24);
            this.btnImgurOpenAuthorizePage.Name = "btnImgurOpenAuthorizePage";
            this.btnImgurOpenAuthorizePage.Size = new System.Drawing.Size(200, 23);
            this.btnImgurOpenAuthorizePage.TabIndex = 1;
            this.btnImgurOpenAuthorizePage.Text = "Open authorize page...";
            this.btnImgurOpenAuthorizePage.UseVisualStyleBackColor = true;
            this.btnImgurOpenAuthorizePage.Click += new System.EventHandler(this.btnImgurOpenAuthorizePage_Click);
            // 
            // lblImgurVerificationCode
            // 
            this.lblImgurVerificationCode.AutoSize = true;
            this.lblImgurVerificationCode.Location = new System.Drawing.Point(16, 64);
            this.lblImgurVerificationCode.Name = "lblImgurVerificationCode";
            this.lblImgurVerificationCode.Size = new System.Drawing.Size(292, 13);
            this.lblImgurVerificationCode.TabIndex = 5;
            this.lblImgurVerificationCode.Text = "Verification code (Get verification code from authorize page):";
            // 
            // btnImgurEnterVerificationCode
            // 
            this.btnImgurEnterVerificationCode.Location = new System.Drawing.Point(16, 120);
            this.btnImgurEnterVerificationCode.Name = "btnImgurEnterVerificationCode";
            this.btnImgurEnterVerificationCode.Size = new System.Drawing.Size(200, 23);
            this.btnImgurEnterVerificationCode.TabIndex = 2;
            this.btnImgurEnterVerificationCode.Text = "Complete authorization";
            this.btnImgurEnterVerificationCode.UseVisualStyleBackColor = true;
            this.btnImgurEnterVerificationCode.Click += new System.EventHandler(this.btnImgurEnterVerificationCode_Click);
            // 
            // txtImgurVerificationCode
            // 
            this.txtImgurVerificationCode.Location = new System.Drawing.Point(16, 88);
            this.txtImgurVerificationCode.Name = "txtImgurVerificationCode";
            this.txtImgurVerificationCode.Size = new System.Drawing.Size(360, 20);
            this.txtImgurVerificationCode.TabIndex = 4;
            // 
            // lblImgurAccountStatus
            // 
            this.lblImgurAccountStatus.AutoSize = true;
            this.lblImgurAccountStatus.Location = new System.Drawing.Point(16, 160);
            this.lblImgurAccountStatus.Name = "lblImgurAccountStatus";
            this.lblImgurAccountStatus.Size = new System.Drawing.Size(77, 13);
            this.lblImgurAccountStatus.TabIndex = 3;
            this.lblImgurAccountStatus.Text = "Login required.";
            // 
            // cbImgurUseUserAccount
            // 
            this.cbImgurUseUserAccount.AutoSize = true;
            this.cbImgurUseUserAccount.Location = new System.Drawing.Point(16, 16);
            this.cbImgurUseUserAccount.Name = "cbImgurUseUserAccount";
            this.cbImgurUseUserAccount.Size = new System.Drawing.Size(142, 17);
            this.cbImgurUseUserAccount.TabIndex = 0;
            this.cbImgurUseUserAccount.Text = "Upload via user account";
            this.cbImgurUseUserAccount.UseVisualStyleBackColor = true;
            this.cbImgurUseUserAccount.CheckedChanged += new System.EventHandler(this.cbImgurUseUserAccount_CheckedChanged);
            // 
            // tpFlickr
            // 
            this.tpFlickr.Location = new System.Drawing.Point(4, 22);
            this.tpFlickr.Name = "tpFlickr";
            this.tpFlickr.Padding = new System.Windows.Forms.Padding(3);
            this.tpFlickr.Size = new System.Drawing.Size(834, 464);
            this.tpFlickr.TabIndex = 3;
            this.tpFlickr.Text = "Flickr";
            this.tpFlickr.UseVisualStyleBackColor = true;
            // 
            // tpTwitPic
            // 
            this.tpTwitPic.Location = new System.Drawing.Point(4, 22);
            this.tpTwitPic.Name = "tpTwitPic";
            this.tpTwitPic.Padding = new System.Windows.Forms.Padding(3);
            this.tpTwitPic.Size = new System.Drawing.Size(834, 464);
            this.tpTwitPic.TabIndex = 4;
            this.tpTwitPic.Text = "TwitPic";
            this.tpTwitPic.UseVisualStyleBackColor = true;
            // 
            // tpTwitSnaps
            // 
            this.tpTwitSnaps.Controls.Add(this.lblTwitSnapsTip);
            this.tpTwitSnaps.Location = new System.Drawing.Point(4, 22);
            this.tpTwitSnaps.Name = "tpTwitSnaps";
            this.tpTwitSnaps.Padding = new System.Windows.Forms.Padding(3);
            this.tpTwitSnaps.Size = new System.Drawing.Size(834, 464);
            this.tpTwitSnaps.TabIndex = 7;
            this.tpTwitSnaps.Text = "TwitSnaps";
            this.tpTwitSnaps.UseVisualStyleBackColor = true;
            // 
            // lblTwitSnapsTip
            // 
            this.lblTwitSnapsTip.AutoSize = true;
            this.lblTwitSnapsTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTwitSnapsTip.Location = new System.Drawing.Point(16, 16);
            this.lblTwitSnapsTip.Name = "lblTwitSnapsTip";
            this.lblTwitSnapsTip.Size = new System.Drawing.Size(182, 20);
            this.lblTwitSnapsTip.TabIndex = 0;
            this.lblTwitSnapsTip.Text = "Other Services -> Twitter";
            // 
            // tpYFrog
            // 
            this.tpYFrog.Location = new System.Drawing.Point(4, 22);
            this.tpYFrog.Name = "tpYFrog";
            this.tpYFrog.Padding = new System.Windows.Forms.Padding(3);
            this.tpYFrog.Size = new System.Drawing.Size(834, 464);
            this.tpYFrog.TabIndex = 8;
            this.tpYFrog.Text = "YFrog";
            this.tpYFrog.UseVisualStyleBackColor = true;
            // 
            // tpMediaWiki
            // 
            this.tpMediaWiki.Location = new System.Drawing.Point(4, 22);
            this.tpMediaWiki.Name = "tpMediaWiki";
            this.tpMediaWiki.Padding = new System.Windows.Forms.Padding(3);
            this.tpMediaWiki.Size = new System.Drawing.Size(834, 464);
            this.tpMediaWiki.TabIndex = 5;
            this.tpMediaWiki.Text = "MediaWiki";
            this.tpMediaWiki.UseVisualStyleBackColor = true;
            // 
            // tpDekiWiki
            // 
            this.tpDekiWiki.Location = new System.Drawing.Point(4, 22);
            this.tpDekiWiki.Name = "tpDekiWiki";
            this.tpDekiWiki.Padding = new System.Windows.Forms.Padding(3);
            this.tpDekiWiki.Size = new System.Drawing.Size(834, 464);
            this.tpDekiWiki.TabIndex = 6;
            this.tpDekiWiki.Text = "DekiWiki";
            this.tpDekiWiki.UseVisualStyleBackColor = true;
            // 
            // tpFileUploaders
            // 
            this.tpFileUploaders.Controls.Add(this.tcFileUploaders);
            this.tpFileUploaders.Location = new System.Drawing.Point(4, 22);
            this.tpFileUploaders.Name = "tpFileUploaders";
            this.tpFileUploaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpFileUploaders.Size = new System.Drawing.Size(848, 496);
            this.tpFileUploaders.TabIndex = 1;
            this.tpFileUploaders.Text = "File uploaders";
            this.tpFileUploaders.UseVisualStyleBackColor = true;
            // 
            // tcFileUploaders
            // 
            this.tcFileUploaders.Controls.Add(this.tpFTP);
            this.tcFileUploaders.Controls.Add(this.tpRapidShare);
            this.tcFileUploaders.Controls.Add(this.tpDropbox);
            this.tcFileUploaders.Controls.Add(this.tpSendSpace);
            this.tcFileUploaders.Controls.Add(this.tpCustomUploaders);
            this.tcFileUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcFileUploaders.Location = new System.Drawing.Point(3, 3);
            this.tcFileUploaders.Name = "tcFileUploaders";
            this.tcFileUploaders.SelectedIndex = 0;
            this.tcFileUploaders.Size = new System.Drawing.Size(842, 490);
            this.tcFileUploaders.TabIndex = 0;
            // 
            // tpFTP
            // 
            this.tpFTP.Location = new System.Drawing.Point(4, 22);
            this.tpFTP.Name = "tpFTP";
            this.tpFTP.Padding = new System.Windows.Forms.Padding(3);
            this.tpFTP.Size = new System.Drawing.Size(834, 464);
            this.tpFTP.TabIndex = 0;
            this.tpFTP.Text = "FTP";
            this.tpFTP.UseVisualStyleBackColor = true;
            // 
            // tpRapidShare
            // 
            this.tpRapidShare.Location = new System.Drawing.Point(4, 22);
            this.tpRapidShare.Name = "tpRapidShare";
            this.tpRapidShare.Padding = new System.Windows.Forms.Padding(3);
            this.tpRapidShare.Size = new System.Drawing.Size(834, 464);
            this.tpRapidShare.TabIndex = 1;
            this.tpRapidShare.Text = "RapidShare";
            this.tpRapidShare.UseVisualStyleBackColor = true;
            // 
            // tpDropbox
            // 
            this.tpDropbox.Location = new System.Drawing.Point(4, 22);
            this.tpDropbox.Name = "tpDropbox";
            this.tpDropbox.Padding = new System.Windows.Forms.Padding(3);
            this.tpDropbox.Size = new System.Drawing.Size(834, 464);
            this.tpDropbox.TabIndex = 2;
            this.tpDropbox.Text = "Dropbox";
            this.tpDropbox.UseVisualStyleBackColor = true;
            // 
            // tpSendSpace
            // 
            this.tpSendSpace.Location = new System.Drawing.Point(4, 22);
            this.tpSendSpace.Name = "tpSendSpace";
            this.tpSendSpace.Padding = new System.Windows.Forms.Padding(3);
            this.tpSendSpace.Size = new System.Drawing.Size(834, 464);
            this.tpSendSpace.TabIndex = 3;
            this.tpSendSpace.Text = "SendSpace";
            this.tpSendSpace.UseVisualStyleBackColor = true;
            // 
            // tpCustomUploaders
            // 
            this.tpCustomUploaders.Location = new System.Drawing.Point(4, 22);
            this.tpCustomUploaders.Name = "tpCustomUploaders";
            this.tpCustomUploaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpCustomUploaders.Size = new System.Drawing.Size(834, 464);
            this.tpCustomUploaders.TabIndex = 4;
            this.tpCustomUploaders.Text = "Custom uploaders";
            this.tpCustomUploaders.UseVisualStyleBackColor = true;
            // 
            // tpTextUploaders
            // 
            this.tpTextUploaders.Controls.Add(this.tcTextUploaders);
            this.tpTextUploaders.Location = new System.Drawing.Point(4, 22);
            this.tpTextUploaders.Name = "tpTextUploaders";
            this.tpTextUploaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpTextUploaders.Size = new System.Drawing.Size(848, 496);
            this.tpTextUploaders.TabIndex = 2;
            this.tpTextUploaders.Text = "Text uploaders";
            this.tpTextUploaders.UseVisualStyleBackColor = true;
            // 
            // tcTextUploaders
            // 
            this.tcTextUploaders.Controls.Add(this.tpPastebin);
            this.tcTextUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTextUploaders.Location = new System.Drawing.Point(3, 3);
            this.tcTextUploaders.Name = "tcTextUploaders";
            this.tcTextUploaders.SelectedIndex = 0;
            this.tcTextUploaders.Size = new System.Drawing.Size(842, 490);
            this.tcTextUploaders.TabIndex = 0;
            // 
            // tpPastebin
            // 
            this.tpPastebin.Location = new System.Drawing.Point(4, 22);
            this.tpPastebin.Name = "tpPastebin";
            this.tpPastebin.Padding = new System.Windows.Forms.Padding(3);
            this.tpPastebin.Size = new System.Drawing.Size(834, 464);
            this.tpPastebin.TabIndex = 0;
            this.tpPastebin.Text = "Pastebin";
            this.tpPastebin.UseVisualStyleBackColor = true;
            // 
            // tpURLShorteners
            // 
            this.tpURLShorteners.Controls.Add(this.tcURLShorteners);
            this.tpURLShorteners.Location = new System.Drawing.Point(4, 22);
            this.tpURLShorteners.Name = "tpURLShorteners";
            this.tpURLShorteners.Padding = new System.Windows.Forms.Padding(3);
            this.tpURLShorteners.Size = new System.Drawing.Size(848, 496);
            this.tpURLShorteners.TabIndex = 3;
            this.tpURLShorteners.Text = "URL Shorteners";
            this.tpURLShorteners.UseVisualStyleBackColor = true;
            // 
            // tcURLShorteners
            // 
            this.tcURLShorteners.Controls.Add(this.tpURLShortenerPage1);
            this.tcURLShorteners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcURLShorteners.Location = new System.Drawing.Point(3, 3);
            this.tcURLShorteners.Name = "tcURLShorteners";
            this.tcURLShorteners.SelectedIndex = 0;
            this.tcURLShorteners.Size = new System.Drawing.Size(842, 490);
            this.tcURLShorteners.TabIndex = 0;
            // 
            // tpURLShortenerPage1
            // 
            this.tpURLShortenerPage1.Location = new System.Drawing.Point(4, 22);
            this.tpURLShortenerPage1.Name = "tpURLShortenerPage1";
            this.tpURLShortenerPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tpURLShortenerPage1.Size = new System.Drawing.Size(834, 464);
            this.tpURLShortenerPage1.TabIndex = 0;
            this.tpURLShortenerPage1.Text = "Nothing yet";
            this.tpURLShortenerPage1.UseVisualStyleBackColor = true;
            // 
            // tpOtherServices
            // 
            this.tpOtherServices.Controls.Add(this.tcOtherServices);
            this.tpOtherServices.Location = new System.Drawing.Point(4, 22);
            this.tpOtherServices.Name = "tpOtherServices";
            this.tpOtherServices.Padding = new System.Windows.Forms.Padding(3);
            this.tpOtherServices.Size = new System.Drawing.Size(848, 496);
            this.tpOtherServices.TabIndex = 4;
            this.tpOtherServices.Text = "Other Services";
            this.tpOtherServices.UseVisualStyleBackColor = true;
            // 
            // tcOtherServices
            // 
            this.tcOtherServices.Controls.Add(this.tpTwitter);
            this.tcOtherServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcOtherServices.Location = new System.Drawing.Point(3, 3);
            this.tcOtherServices.Name = "tcOtherServices";
            this.tcOtherServices.SelectedIndex = 0;
            this.tcOtherServices.Size = new System.Drawing.Size(842, 490);
            this.tcOtherServices.TabIndex = 0;
            // 
            // tpTwitter
            // 
            this.tpTwitter.Location = new System.Drawing.Point(4, 22);
            this.tpTwitter.Name = "tpTwitter";
            this.tpTwitter.Padding = new System.Windows.Forms.Padding(3);
            this.tpTwitter.Size = new System.Drawing.Size(834, 464);
            this.tpTwitter.TabIndex = 0;
            this.tpTwitter.Text = "Twitter";
            this.tpTwitter.UseVisualStyleBackColor = true;
            // 
            // UploadersConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 528);
            this.Controls.Add(this.tcUploaders);
            this.Name = "UploadersConfigForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "Uploaders config";
            this.tcUploaders.ResumeLayout(false);
            this.tpImageUploaders.ResumeLayout(false);
            this.tcImageUploaders.ResumeLayout(false);
            this.tpImageShack.ResumeLayout(false);
            this.tpImageShack.PerformLayout();
            this.tpTinyPic.ResumeLayout(false);
            this.tpTinyPic.PerformLayout();
            this.tpImgur.ResumeLayout(false);
            this.tpImgur.PerformLayout();
            this.gbImgurUserAccount.ResumeLayout(false);
            this.gbImgurUserAccount.PerformLayout();
            this.tpTwitSnaps.ResumeLayout(false);
            this.tpTwitSnaps.PerformLayout();
            this.tpFileUploaders.ResumeLayout(false);
            this.tcFileUploaders.ResumeLayout(false);
            this.tpTextUploaders.ResumeLayout(false);
            this.tcTextUploaders.ResumeLayout(false);
            this.tpURLShorteners.ResumeLayout(false);
            this.tcURLShorteners.ResumeLayout(false);
            this.tpOtherServices.ResumeLayout(false);
            this.tcOtherServices.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcUploaders;
        private System.Windows.Forms.TabPage tpImageUploaders;
        private System.Windows.Forms.TabPage tpFileUploaders;
        private System.Windows.Forms.TabPage tpTextUploaders;
        private System.Windows.Forms.TabPage tpURLShorteners;
        private System.Windows.Forms.TabPage tpOtherServices;
        private System.Windows.Forms.TabControl tcImageUploaders;
        private System.Windows.Forms.TabPage tpImageShack;
        private System.Windows.Forms.TabPage tpTinyPic;
        private System.Windows.Forms.TabPage tpImgur;
        private System.Windows.Forms.TabPage tpFlickr;
        private System.Windows.Forms.TabPage tpTwitPic;
        private System.Windows.Forms.TabPage tpMediaWiki;
        private System.Windows.Forms.TabPage tpDekiWiki;
        private System.Windows.Forms.TabPage tpTwitSnaps;
        private System.Windows.Forms.Label lblTwitSnapsTip;
        private System.Windows.Forms.TabPage tpYFrog;
        private System.Windows.Forms.TabControl tcFileUploaders;
        private System.Windows.Forms.TabPage tpFTP;
        private System.Windows.Forms.TabPage tpRapidShare;
        private System.Windows.Forms.TabPage tpDropbox;
        private System.Windows.Forms.TabPage tpSendSpace;
        private System.Windows.Forms.TabPage tpCustomUploaders;
        private System.Windows.Forms.TabControl tcTextUploaders;
        private System.Windows.Forms.TabPage tpPastebin;
        private System.Windows.Forms.TabControl tcURLShorteners;
        private System.Windows.Forms.TabPage tpURLShortenerPage1;
        private System.Windows.Forms.TabControl tcOtherServices;
        private System.Windows.Forms.TabPage tpTwitter;
        internal System.Windows.Forms.Button btnImageShackOpenPublicProfile;
        internal System.Windows.Forms.CheckBox cbImageShackIsPublic;
        internal System.Windows.Forms.Button btnImageShackOpenMyImages;
        internal System.Windows.Forms.Label lblImageShackUsername;
        internal System.Windows.Forms.Button btnImageShackOpenRegistrationCode;
        internal System.Windows.Forms.TextBox txtImageShackUsername;
        internal System.Windows.Forms.TextBox txtImageShackRegistrationCode;
        internal System.Windows.Forms.Label lblImageShackRegistrationCode;
        internal System.Windows.Forms.Button btnTinyPicOpenMyImages;
        internal System.Windows.Forms.CheckBox cbTinyPicRememberUsernamePassword;
        internal System.Windows.Forms.Label lblTinyPicRegistrationCode;
        internal System.Windows.Forms.TextBox txtTinyPicRegistrationCode;
        private System.Windows.Forms.TextBox txtTinyPicPassword;
        private System.Windows.Forms.Label lblTinyPicPassword;
        private System.Windows.Forms.TextBox txtTinyPicUsername;
        private System.Windows.Forms.Label lblTinyPicUsername;
        private System.Windows.Forms.Button btnTinyPicLogin;
        private System.Windows.Forms.CheckBox cbImgurUseUserAccount;
        private System.Windows.Forms.Button btnImgurOpenAuthorizePage;
        private System.Windows.Forms.Label lblImgurAccountStatus;
        private System.Windows.Forms.Button btnImgurEnterVerificationCode;
        private System.Windows.Forms.TextBox txtImgurVerificationCode;
        private System.Windows.Forms.GroupBox gbImgurUserAccount;
        private System.Windows.Forms.Label lblImgurVerificationCode;
    }
}