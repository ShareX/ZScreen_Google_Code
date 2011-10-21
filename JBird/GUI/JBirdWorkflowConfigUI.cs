﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkey;
using ZScreenLib;

namespace JBirdGUI
{
    public partial class JBirdWorkflowWizard : ZScreenLib.WorkflowWizard
    {
        public JBirdWorkflowWizard(TaskInfo info = null, Workflow workflow = null, WorkflowWizardGUIOptions gui = null)
        {
            InitializeComponent();
            base.InitializeComponent();
            base.Initialize(info, workflow, gui);

            HotkeyManager tempHotkeyMgr;
            Program.HotkeyMgrs.TryGetValue(this.Config.ID, out tempHotkeyMgr);
            if (tempHotkeyMgr != null)
            {
                hmcHotkeys.PrepareHotkeys(tempHotkeyMgr);
            }
            else
            {
                HotkeyManager hm = new HotkeyManager(Program.CoreUI, ZAppType.JBird);
                hm.AddHotkey(JBirdHotkey.Workflow, Config.Hotkey, Config.Start);
                hmcHotkeys.PrepareHotkeys(hm);
            }
        }

        private void JBirdWorkflowWizard_Load(object sender, EventArgs e)
        {
            this.Controls.Add(tcMain);
        }
    }
}