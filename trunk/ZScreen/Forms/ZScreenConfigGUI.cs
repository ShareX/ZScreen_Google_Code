using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using GraphicsMgrLib;
using HelpersLib;
using MS.WindowsAPICodePack.Internal;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using ZScreenLib;
using ZSS.UpdateCheckerLib;

namespace ZScreenGUI
{
    public partial class ZScreen : Form
    {
        private void ZScreen_ConfigGUI()
        {
            FileSystem.AppendDebug("Configuring ZScreen GUI via " + new StackFrame(1).GetMethod().Name);
            pgApp.SelectedObject = Engine.conf;
            pgIndexer.SelectedObject = Engine.conf.IndexerConfig;
            ZScreen_ConfigGUI_Main();
            ZScreen_ConfigGUI_TrayMenu();
            ZScreen_ConfigGUI_Options();
            ZScreen_ConfigGUI_Destinations();
            ZScreen_ConfigGUI_Hotkeys();
            ZScreen_ConfigGUI_Screenshots();
            ZScreen_ConfigGUI_Editors();
            ZScreen_ConfigGUI_ImageHosting();
            ZScreen_ConfigGUI_TextServices();
            ZScreen_ConfigGUI_Translator();
            ZScreen_ConfigGUI_History();

            CheckFormSettings();
        }

        private void ZScreen_ConfigGUI_Main()
        {
            if (ucDestOptions.cboImageUploaders.Items.Count == 0)
            {
                ucDestOptions.cboImageUploaders.Items.AddRange(typeof(ImageUploaderType).GetDescriptions());
                ucDestOptions.cboImageUploaders.SelectedIndex = Engine.conf.MyImageUploader.BetweenOrDefault(0, ucDestOptions.cboImageUploaders.Items.Count - 1);
            }

            if (ucDestOptions.cboTextUploaders.Items.Count == 0)
            {
                ucDestOptions.cboTextUploaders.Items.AddRange(typeof(TextUploaderType).GetDescriptions());
                ucDestOptions.cboTextUploaders.SelectedIndex = Engine.conf.MyTextUploader.BetweenOrDefault(0, ucDestOptions.cboTextUploaders.Items.Count - 1);
            }

            if (ucDestOptions.cboFileUploaders.Items.Count == 0)
            {
                ucDestOptions.cboFileUploaders.Items.AddRange(typeof(FileUploaderType).GetDescriptions());
                ucDestOptions.cboFileUploaders.SelectedIndex = Engine.conf.MyFileUploader.BetweenOrDefault(0, ucDestOptions.cboFileUploaders.Items.Count - 1);
            }

            if (ucDestOptions.cboURLShorteners.Items.Count == 0)
            {
                ucDestOptions.cboURLShorteners.Items.AddRange(typeof(UrlShortenerType).GetDescriptions());
                ucDestOptions.cboURLShorteners.SelectedIndex = Engine.conf.MyURLShortener.BetweenOrDefault(0, ucDestOptions.cboURLShorteners.Items.Count - 1);
            }

            if (cboURLFormat.Items.Count == 0)
            {
                cboURLFormat.Items.AddRange(typeof(ClipboardUriType).GetDescriptions());
                cboURLFormat.SelectedIndex = Engine.conf.MyClipboardUriMode.BetweenOrDefault(0, cboURLFormat.Items.Count - 1);
            }

            chkManualNaming.Checked = Engine.conf.ManualNaming;
            chkShowCursor.Checked = Engine.conf.ShowCursor;
        }

        private void ZScreen_ConfigGUI_Destinations()
        {
            #region FTP Settings

            if (Engine.conf.FTPAccountList == null || Engine.conf.FTPAccountList.Count == 0)
            {
                FTPSetup(new List<FTPAccount>());
            }
            else
            {
                FTPSetup(Engine.conf.FTPAccountList);
                if (ucFTPAccounts.AccountsList.Items.Count > 0)
                {
                    ucFTPAccounts.AccountsList.SelectedIndex = 0;
                }
            }

            txtFTPThumbWidth.Text = Engine.conf.FTPThumbnailWidth.ToString();
            cbFTPThumbnailCheckSize.Checked = Engine.conf.FTPThumbnailCheckSize;

            #endregion FTP Settings

            #region Localhost Settings

            if (Engine.conf.LocalhostAccountList == null || Engine.conf.LocalhostAccountList.Count == 0)
            {
                LocalhostAccountsSetup(new List<LocalhostAccount>());
            }
            else
            {
                LocalhostAccountsSetup(Engine.conf.LocalhostAccountList);
                if (ucLocalhostAccounts.AccountsList.Items.Count > 0)
                {
                    ucLocalhostAccounts.AccountsList.SelectedIndex = Engine.conf.LocalhostSelected;
                }
            }

            #endregion Localhost Settings

            #region MindTouch Settings

            ///////////////////////////////////
            // MindTouch Deki Wiki Settings
            ///////////////////////////////////

            if (Engine.conf.DekiWikiAccountList == null || Engine.conf.DekiWikiAccountList.Count == 0)
            {
                DekiWikiSetup(new List<DekiWikiAccount>());
            }
            else
            {
                DekiWikiSetup(Engine.conf.DekiWikiAccountList);
                if (ucMindTouchAccounts.AccountsList.Items.Count > 0)
                {
                    ucMindTouchAccounts.AccountsList.SelectedIndex = Engine.conf.DekiWikiSelected;
                }
            }

            chkDekiWikiForcePath.Checked = Engine.conf.DekiWikiForcePath;

            #endregion MindTouch Settings

            #region MediaWiki Settings

            ///////////////////////////////////
            // MediaWiki Settings
            ///////////////////////////////////

            if (Engine.conf.MediaWikiAccountList == null || Engine.conf.MediaWikiAccountList.Count == 0)
            {
                MediaWikiSetup(new List<MediaWikiAccount>());
            }
            else
            {
                MediaWikiSetup(Engine.conf.MediaWikiAccountList);
                if (ucMediaWikiAccounts.AccountsList.Items.Count > 0)
                {
                    ucMediaWikiAccounts.AccountsList.SelectedIndex = Engine.conf.MediaWikiAccountSelected;
                }
            }

            #endregion MediaWiki Settings

            #region Image Uploaders

            ///////////////////////////////////
            // Image Uploader Settings
            ///////////////////////////////////

            // ImageShack

            txtImageShackRegistrationCode.Text = Engine.conf.ImageShackRegistrationCode;
            txtUserNameImageShack.Text = Engine.conf.ImageShackUserName;
            chkPublicImageShack.Checked = Engine.conf.ImageShackShowImagesInPublic;

            // TinyPic

            txtTinyPicShuk.Text = Engine.conf.TinyPicShuk;
            chkRememberTinyPicUserPass.Checked = Engine.conf.RememberTinyPicUserPass;

            // cboTwitPicUploadMode.SelectedIndex = (int)Engine.conf.TwitPicUploadMode;
            cbTwitPicShowFull.Checked = Engine.conf.TwitPicShowFull;
            if (cboTwitPicThumbnailMode.Items.Count == 0)
            {
                cboTwitPicThumbnailMode.Items.AddRange(typeof(TwitPicThumbnailType).GetDescriptions());
            }

            cboTwitPicThumbnailMode.SelectedIndex = (int)Engine.conf.TwitPicThumbnailMode;

            // Imgur

            chkImgurUserAccount.Checked = Engine.conf.ImgurAccountType == AccountType.User;

            if (OAuthInfo.CheckOAuth(Engine.conf.ImgurOAuthInfo))
            {
                lblImgurStatus.Text = "User token: " + Engine.conf.ImgurOAuthInfo.UserToken;
            }

            // Twitter

            ucTwitterAccounts.AccountsList.Items.Clear();
            foreach (OAuthInfo acc in Engine.conf.TwitterOAuthInfoList)
            {
                ucTwitterAccounts.AccountsList.Items.Add(acc);
            }
            if (ucTwitterAccounts.AccountsList.Items.Count > 0)
            {
                ucTwitterAccounts.AccountsList.SelectedIndex = Engine.conf.TwitterAcctSelected;
            }

            // Flickr

            pgFlickrAuthInfo.SelectedObject = Engine.conf.FlickrAuthInfo;
            pgFlickrSettings.SelectedObject = Engine.conf.FlickrSettings;
            // btnFlickrOpenImages.Text = string.Format("{0}'s photostream", Engine.conf.FlickrAuthInfo.Username);

            #endregion Image Uploaders

            #region File Uploaders

            // RapidShare

            if (cboRapidShareAcctType.Items.Count == 0)
            {
                cboRapidShareAcctType.Items.AddRange(typeof(RapidShareAcctType).GetDescriptions());
            }

            cboRapidShareAcctType.SelectedIndex = (int)Engine.conf.RapidShareAccountType;
            txtRapidShareCollectorID.Text = Engine.conf.RapidShareCollectorsID;
            txtRapidSharePassword.Text = Engine.conf.RapidSharePassword;
            txtRapidSharePremiumUserName.Text = Engine.conf.RapidSharePremiumUserName;

            // SendSpace

            if (cboSendSpaceAcctType.Items.Count == 0)
            {
                cboSendSpaceAcctType.Items.AddRange(typeof(AccountType).GetDescriptions());
            }

            cboSendSpaceAcctType.SelectedIndex = (int)Engine.conf.SendSpaceAccountType;
            txtSendSpacePassword.Text = Engine.conf.SendSpacePassword;
            txtSendSpaceUserName.Text = Engine.conf.SendSpaceUserName;

            // Dropbox

            txtDropboxPath.Text = Engine.conf.DropboxUploadPath;
            UpdateDropboxStatus();

            #endregion File Uploaders

            #region Text Uploaders

            pgPastebinSettings.SelectedObject = Engine.conf.PastebinSettings;

            #endregion Text Uploaders
        }

        private void ZScreen_ConfigGUI_Editors()
        {
            chkPerformActions.Checked = Engine.conf.PerformActions;
            tsmEditinImageSoftware.Checked = Engine.conf.PerformActions;

            Software editor = new Software(Engine.ZSCREEN_IMAGE_EDITOR, string.Empty, false) { TriggerForFiles = false, TriggerForText = false };
            if (Software.Exist(Engine.ZSCREEN_IMAGE_EDITOR))
            {
                editor = Software.GetByName(Engine.ZSCREEN_IMAGE_EDITOR);
            }

            string mspaint = "Paint";
            Software paint = new Software(mspaint, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "mspaint.exe"), false) { TriggerForFiles = false, TriggerForText = false };
            if (Software.Exist(mspaint))
            {
                paint = Software.GetByName(mspaint);
            }

            Engine.conf.ActionsList.RemoveAll(x => x.Path == string.Empty || x.Name == Engine.ZSCREEN_IMAGE_EDITOR || x.Name == mspaint || !File.Exists(x.Path));

            Engine.conf.ActionsList.Insert(0, editor);
            if (File.Exists(paint.Path))
            {
                Engine.conf.ActionsList.Insert(1, paint);
            }

            RegistryMgr.FindImageEditors();

            lbSoftware.Items.Clear();

            foreach (Software app in Engine.conf.ActionsList)
            {
                if (!String.IsNullOrEmpty(app.Name))
                {
                    lbSoftware.Items.Add(app.Name, app.Enabled);
                }
            }
            RewriteImageEditorsRightClickMenu();

            int i;
            if (Engine.conf.ImageEditor != null && (i = lbSoftware.Items.IndexOf(Engine.conf.ImageEditor.Name)) != -1)
            {
                lbSoftware.SelectedIndex = i;
            }
            else if (lbSoftware.Items.Count > 0)
            {
                lbSoftware.SelectedIndex = 0;
            }

            chkImageEditorAutoSave.Checked = Engine.conf.ImageEditorAutoSave;

            // Text Editors

            if (Engine.conf.TextEditors.Count == 0)
            {
                Engine.conf.TextEditors.Add(new Software("Notepad", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "notepad.exe"), true));
            }
        }

        private void ZScreen_ConfigGUI_History()
        {
            nudHistoryMaxItems.Value = Engine.conf.HistoryMaxNumber;
            cbHistorySave.Checked = Engine.conf.HistorySave;
        }

        private void ZScreen_ConfigGUI_Hotkeys()
        {
            Loader.Worker.mHotkeyMgr.UpdateHotkeysDGV();
        }

        private void ZScreen_ConfigGUI_ImageHosting()
        {
            // Image Uploaders

            cbLanguageAutoDetect.Checked = Engine.conf.GoogleAutoDetectSource;
            cbAutoTranslate.Checked = Engine.conf.AutoTranslate;
            txtAutoTranslate.Text = Engine.conf.AutoTranslateLength.ToString();

            // Custom Image Uploaders
            lbImageUploader.Items.Clear();
            if (Engine.conf.CustomUploadersList == null)
            {
                Engine.conf.CustomUploadersList = new List<CustomUploaderInfo>();
                LoadImageUploaders(new CustomUploaderInfo());
            }
            else
            {
                List<CustomUploaderInfo> iUploaders = Engine.conf.CustomUploadersList;
                foreach (CustomUploaderInfo iUploader in iUploaders)
                {
                    lbImageUploader.Items.Add(iUploader.Name);
                }

                if (lbImageUploader.Items.Count > 0)
                {
                    lbImageUploader.SelectedIndex = Engine.conf.CustomUploaderSelected;
                }

                if (lbImageUploader.SelectedIndex != -1)
                {
                    LoadImageUploaders(Engine.conf.CustomUploadersList[lbImageUploader.SelectedIndex]);
                }
            }

            // Web Page Upload

            cbWebPageUseCustomSize.Checked = Engine.conf.WebPageUseCustomSize;
            txtWebPageWidth.Text = Engine.conf.WebPageWidth.ToString();
            txtWebPageHeight.Text = Engine.conf.WebPageHeight.ToString();
            cbWebPageAutoUpload.Checked = Engine.conf.WebPageAutoUpload;
        }

        private void ZScreen_ConfigGUI_Options()
        {
            Engine.mAppSettings.PreferSystemFolders = Engine.conf.PreferSystemFolders;
            chkPreferSystemFolders.Checked = Engine.conf.PreferSystemFolders;
            txtRootFolder.Text = Engine.RootAppFolder;
            UpdateGuiControlsPaths();

            // General

            chkStartWin.Checked = RegistryMgr.CheckStartWithWindows();
            chkShellExt.Checked = RegistryMgr.CheckShellExt();
            chkOpenMainWindow.Checked = Engine.conf.ShowMainWindow;
            chkShowTaskbar.Checked = Engine.conf.ShowInTaskbar;
            cbShowHelpBalloonTips.Checked = Engine.conf.ShowHelpBalloonTips;
            cbAutoSaveSettings.Checked = Engine.conf.AutoSaveSettings;
            chkWindows7TaskbarIntegration.Checked = CoreHelpers.RunningOnWin7 && Engine.conf.Windows7TaskbarIntegration;
            chkWindows7TaskbarIntegration.Enabled = CoreHelpers.RunningOnWin7;
            // chkShowTaskbar.Enabled = !Engine.conf.Windows7TaskbarIntegration || !CoreHelpers.RunningOnWin7;
            chkTwitterEnable.Checked = Engine.conf.TwitterEnabled;

            // Interaction
            chkShortenURL.Checked = Engine.conf.ShortenUrlAfterUpload;
            nudFlashIconCount.Value = Engine.conf.FlashTrayCount;
            chkCaptureFallback.Checked = Engine.conf.CaptureEntireScreenOnError;
            cbShowPopup.Checked = Engine.conf.ShowBalloonTip;
            chkBalloonTipOpenLink.Checked = Engine.conf.BalloonTipOpenLink;
            cbShowUploadDuration.Checked = Engine.conf.ShowUploadDuration;
            cbCopyClipboardAfterTask.Checked = Engine.conf.CopyClipboardAfterTask;
            cbCompleteSound.Checked = Engine.conf.CompleteSound;
            cbCloseDropBox.Checked = Engine.conf.CloseDropBox;

            // Proxy
            if (cboProxyConfig.Items.Count == 0)
            {
                cboProxyConfig.Items.AddRange(typeof(ProxyConfigType).GetDescriptions());
            }
            cboProxyConfig.SelectedIndex = (int)Engine.conf.ProxyConfig;

            ProxySetup(Engine.conf.ProxyList);
            if (ucProxyAccounts.AccountsList.Items.Count > 0)
            {
                ucProxyAccounts.AccountsList.SelectedIndex = Engine.conf.ProxySelected;
            }

            if (cboCloseButtonAction.Items.Count == 0)
            {
                cboMinimizeButtonAction.Items.AddRange(typeof(WindowButtonAction).GetDescriptions());
            }
            if (cboCloseButtonAction.Items.Count == 0)
            {
                cboCloseButtonAction.Items.AddRange(typeof(WindowButtonAction).GetDescriptions());
            }
            cboCloseButtonAction.SelectedIndex = (int)Engine.conf.WindowButtonActionClose;
            cboMinimizeButtonAction.SelectedIndex = (int)Engine.conf.WindowButtonActionMinimize;

            ttZScreen.Active = Engine.conf.ShowHelpBalloonTips;

            chkCheckUpdates.Checked = Engine.conf.CheckUpdates;
            if (cboReleaseChannel.Items.Count == 0)
            {
                cboReleaseChannel.Items.AddRange(typeof(ReleaseChannelType).GetDescriptions());
                cboReleaseChannel.SelectedIndex = (int)Engine.conf.ReleaseChannel;
            }
            nudCacheSize.Value = Engine.conf.ScreenshotCacheSize;
            chkDeleteLocal.Checked = Engine.conf.DeleteLocal;

            FolderWatcher zWatcher = new FolderWatcher(this);
            zWatcher.FolderPath = Engine.conf.FolderMonitorPath;
            if (Engine.conf.FolderMonitoring)
            {
                zWatcher.StartWatching();
            }
            else
            {
                zWatcher.StopWatching();
            }

            // Monitor Clipboard
            chkMonImages.Checked = Engine.conf.MonitorImages;
            chkMonText.Checked = Engine.conf.MonitorText;
            chkMonFiles.Checked = Engine.conf.MonitorFiles;
            chkMonUrls.Checked = Engine.conf.MonitorUrls;
        }

        private void ZScreen_ConfigGUI_Screenshots()
        {
            ZScreen_ConfigGUI_Screenshots_CropShot();

            // Selected Window
            if (cbSelectedWindowStyle.Items.Count == 0)
            {
                cbSelectedWindowStyle.Items.AddRange(typeof(RegionStyles).GetDescriptions());
            }

            cbSelectedWindowStyle.SelectedIndex = (int)Engine.conf.SelectedWindowRegionStyles;
            cbSelectedWindowRectangleInfo.Checked = Engine.conf.SelectedWindowRectangleInfo;
            cbSelectedWindowRuler.Checked = Engine.conf.SelectedWindowRuler;
            pbSelectedWindowBorderColor.BackColor = XMLSettings.DeserializeColor(Engine.conf.SelectedWindowBorderColor);
            nudSelectedWindowBorderSize.Value = Engine.conf.SelectedWindowBorderSize;
            cbSelectedWindowDynamicBorderColor.Checked = Engine.conf.SelectedWindowDynamicBorderColor;
            nudSelectedWindowRegionInterval.Value = Engine.conf.SelectedWindowRegionInterval;
            nudSelectedWindowRegionStep.Value = Engine.conf.SelectedWindowRegionStep;
            nudSelectedWindowHueRange.Value = Engine.conf.SelectedWindowHueRange;
            chkSelectedWindowCaptureObjects.Checked = Engine.conf.SelectedWindowCaptureObjects;

            // Active Window
            chkActiveWindowPreferDWM.Checked = Engine.conf.ActiveWindowPreferDWM;
            chkSelectedWindowCleanBackground.Checked = Engine.conf.ActiveWindowClearBackground;
            chkSelectedWindowCleanTransparentCorners.Checked = Engine.conf.ActiveWindowCleanTransparentCorners;
            chkSelectedWindowIncludeShadow.Checked = Engine.conf.ActiveWindowIncludeShadows;
            chkActiveWindowTryCaptureChildren.Checked = Engine.conf.ActiveWindowTryCaptureChildren;
            chkSelectedWindowShowCheckers.Checked = Engine.conf.ActiveWindowShowCheckers;
            cbActiveWindowGDIFreezeWindow.Checked = Engine.conf.ActiveWindowGDIFreezeWindow;

            // Freehand Crop Shot
            cbFreehandCropShowHelpText.Checked = Engine.conf.FreehandCropShowHelpText;
            cbFreehandCropAutoUpload.Checked = Engine.conf.FreehandCropAutoUpload;
            cbFreehandCropAutoClose.Checked = Engine.conf.FreehandCropAutoClose;
            cbFreehandCropShowRectangleBorder.Checked = Engine.conf.FreehandCropShowRectangleBorder;

            // Naming Conventions
            txtActiveWindow.Text = Engine.conf.ActiveWindowPattern;
            txtEntireScreen.Text = Engine.conf.EntireScreenPattern;
            txtImagesFolderPattern.Text = Engine.conf.SaveFolderPattern;
            nudMaxNameLength.Value = Engine.conf.MaxNameLength;

            ZScreen_ConfigGUI_Screenshots_Watermark();
            ZScreen_ConfigGUI_Screenshots_ImageSettings();
        }

        private void ZScreen_ConfigGUI_Screenshots_CropShot()
        {
            // Crop Region Settings
            if (chkCropStyle.Items.Count == 0)
            {
                chkCropStyle.Items.AddRange(typeof(RegionStyles).GetDescriptions());
            }
            chkCropStyle.SelectedIndex = (int)Engine.conf.CropRegionStyles;
            chkRegionRectangleInfo.Checked = Engine.conf.CropRegionRectangleInfo;
            chkRegionHotkeyInfo.Checked = Engine.conf.CropRegionHotkeyInfo;

            // Crosshair Settings
            chkCropDynamicCrosshair.Checked = Engine.conf.CropDynamicCrosshair;
            nudCropCrosshairInterval.Value = Engine.conf.CropInterval;
            nudCropCrosshairStep.Value = Engine.conf.CropStep;
            nudCrosshairLineCount.Value = Engine.conf.CrosshairLineCount;
            nudCrosshairLineSize.Value = Engine.conf.CrosshairLineSize;
            pbCropCrosshairColor.BackColor = XMLSettings.DeserializeColor(Engine.conf.CropCrosshairColor);
            chkCropShowBigCross.Checked = Engine.conf.CropShowBigCross;
            chkCropShowMagnifyingGlass.Checked = Engine.conf.CropShowMagnifyingGlass;

            // Region Settings
            cbShowCropRuler.Checked = Engine.conf.CropShowRuler;
            cbCropDynamicBorderColor.Checked = Engine.conf.CropDynamicBorderColor;
            nudCropRegionInterval.Value = Engine.conf.CropRegionInterval;
            nudCropRegionStep.Value = Engine.conf.CropRegionStep;
            nudCropHueRange.Value = Engine.conf.CropHueRange;
            pbCropBorderColor.BackColor = XMLSettings.DeserializeColor(Engine.conf.CropBorderColor);
            nudCropBorderSize.Value = Engine.conf.CropBorderSize;
            cbCropShowGrids.Checked = Engine.conf.CropShowGrids;

            // Grid Mode Settings
            nudScreenshotDelay.Time = Engine.conf.ScreenshotDelayTimes;
            nudScreenshotDelay.Value = Engine.conf.ScreenshotDelayTime;
            cboCropGridMode.Checked = Engine.conf.CropGridToggle;
            nudCropGridWidth.Value = Engine.conf.CropGridSize.Width;
            nudCropGridHeight.Value = Engine.conf.CropGridSize.Height;
        }

        private void ZScreen_ConfigGUI_Screenshots_ImageSettings()
        {
            if (cboFileFormat.Items.Count == 0)
            {
                cboFileFormat.Items.AddRange(typeof(ImageFileFormatType).GetDescriptions());
            }

            cboFileFormat.SelectedIndex = (int)Engine.conf.ImageFileFormat;
            nudImageQuality.Value = Engine.conf.JpgQuality;
            cbGIFQuality.SelectedIndex = (int)Engine.conf.GIFQuality;
            nudSwitchAfter.Value = Engine.conf.SwitchAfter;
            if (cboSwitchFormat.Items.Count == 0)
            {
                cboSwitchFormat.Items.AddRange(typeof(ImageFileFormatType).GetDescriptions());
            }
            cboSwitchFormat.SelectedIndex = (int)Engine.conf.ImageFormatSwitch;

            switch (Engine.conf.ImageSizeType)
            {
                case ImageSizeType.DEFAULT:
                    rbImageSizeDefault.Checked = true;
                    break;
                case ImageSizeType.FIXED:
                    rbImageSizeFixed.Checked = true;
                    break;
                case ImageSizeType.RATIO:
                    rbImageSizeRatio.Checked = true;
                    break;
            }

            txtImageSizeFixedWidth.Text = Engine.conf.ImageSizeFixedWidth.ToString();
            txtImageSizeFixedHeight.Text = Engine.conf.ImageSizeFixedHeight.ToString();
            txtImageSizeRatio.Text = Engine.conf.ImageSizeRatioPercentage.ToString();
        }

        private void ZScreen_ConfigGUI_Screenshots_Watermark()
        {
            if (cboWatermarkType.Items.Count == 0)
            {
                cboWatermarkType.Items.AddRange(typeof(WatermarkType).GetDescriptions());
            }

            cboWatermarkType.SelectedIndex = (int)Engine.conf.WatermarkMode;
            if (chkWatermarkPosition.Items.Count == 0)
            {
                chkWatermarkPosition.Items.AddRange(typeof(WatermarkPositionType).GetDescriptions());
            }

            chkWatermarkPosition.SelectedIndex = (int)Engine.conf.WatermarkPositionMode;
            nudWatermarkOffset.Value = Engine.conf.WatermarkOffset;
            cbWatermarkAddReflection.Checked = Engine.conf.WatermarkAddReflection;
            cbWatermarkAutoHide.Checked = Engine.conf.WatermarkAutoHide;

            txtWatermarkText.Text = Engine.conf.WatermarkText;
            pbWatermarkFontColor.BackColor = XMLSettings.DeserializeColor(Engine.conf.WatermarkFontColor);
            lblWatermarkFont.Text = FontToString();
            nudWatermarkFontTrans.Value = Engine.conf.WatermarkFontTrans;
            trackWatermarkFontTrans.Value = (int)Engine.conf.WatermarkFontTrans;
            nudWatermarkCornerRadius.Value = Engine.conf.WatermarkCornerRadius;
            pbWatermarkGradient1.BackColor = XMLSettings.DeserializeColor(Engine.conf.WatermarkGradient1);
            pbWatermarkGradient2.BackColor = XMLSettings.DeserializeColor(Engine.conf.WatermarkGradient2);
            pbWatermarkBorderColor.BackColor = XMLSettings.DeserializeColor(Engine.conf.WatermarkBorderColor);
            nudWatermarkBackTrans.Value = Engine.conf.WatermarkBackTrans;
            trackWatermarkBackgroundTrans.Value = (int)Engine.conf.WatermarkBackTrans;
            if (cbWatermarkGradientType.Items.Count == 0)
            {
                cbWatermarkGradientType.Items.AddRange(Enum.GetNames(typeof(LinearGradientMode)));
            }

            cbWatermarkGradientType.SelectedIndex = (int)Engine.conf.WatermarkGradientType;
            cboUseCustomGradient.Checked = Engine.conf.WatermarkUseCustomGradient;

            txtWatermarkImageLocation.Text = Engine.conf.WatermarkImageLocation;
            cbWatermarkUseBorder.Checked = Engine.conf.WatermarkUseBorder;
            nudWatermarkImageScale.Value = Engine.conf.WatermarkImageScale;

            TestWatermark();
        }

        private void ZScreen_ConfigGUI_TextServices()
        {
        }

        private void ZScreen_ConfigGUI_Translator()
        {
            // yet empty
        }

        private void ZScreen_ConfigGUI_TrayMenu()
        {
            if (tsmiTabs.DropDownItems.Count == 0)
            {
                foreach (TabPage tp in tcMain.TabPages)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(tp.Text + "...");
                    tsmi.Click += new EventHandler(tsmiTab_Click);
                    tsmi.Image = tabImageList.Images[tp.ImageKey];
                    tsmi.Tag = tp.Name;
                    tsmiTabs.DropDownItems.Add(tsmi);
                }
            }

            if (tsmImageDest.DropDownItems.Count == 0)
            {
                foreach (ImageUploaderType idt in Enum.GetValues(typeof(ImageUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(idt.GetDescription());
                    tsmi.Click += new EventHandler(tsmiDestImages_Click);
                    tsmi.Tag = idt;
                    tsmImageDest.DropDownItems.Add(tsmi);
                }
            }
            CheckToolStripMenuItem(tsmImageDest, GetImageDestMenuItem((ImageUploaderType)Engine.conf.MyImageUploader));

            if (tsmFileDest.DropDownItems.Count == 0)
            {
                foreach (FileUploaderType fileUploader in Enum.GetValues(typeof(FileUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(fileUploader.GetDescription());
                    tsmi.Click += new EventHandler(tsmiDestFiles_Click);
                    tsmi.Tag = fileUploader;
                    tsmFileDest.DropDownItems.Add(tsmi);
                }
            }
            CheckToolStripMenuItem(tsmFileDest, GetFileDestMenuItem((FileUploaderType)Engine.conf.MyFileUploader));
        }
    }
}