using System;
using HelpersLib;
using HelpersLib.Hotkey;
using ZScreenLib;

namespace JBirdGUI
{
    public partial class JBirdWorkflowWizard : ZScreenLib.WorkflowWizard
    {
        public JBirdWorkflowWizard(WorkerTask task = null, Workflow workflow = null, WorkflowWizardGUIOptions gui = null)
        {
            base.InitializeComponent();
            InitializeComponent();
            base.Initialize(task, gui);

            HotkeyManager tempHotkeyMgr;
            this.Task = new WorkerTask(workflow);
            this.Config = workflow;
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
            // this.Controls.Add(tcMain);
        }
    }
}