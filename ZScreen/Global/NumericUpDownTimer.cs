using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ZSS
{
    public enum Times
    {
        Milliseconds, Seconds, Minute, Hours
    }

    [DefaultEvent("ValueChanged")]
    public partial class NumericUpDownTimer : UserControl
    {
        public event EventHandler ValueChanged;
        public event EventHandler SelectedIndexChanged;
        public Times Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                cbDelay.SelectedIndex = (int)time;
            }
        }

        private Times time;

        public NumericUpDownTimer()
        {
            InitializeComponent();
            cbDelay.Items.AddRange(Enum.GetNames(typeof(Times)));
        }

        public NumericUpDownTimer(Times time)
            : this()
        {
            this.Time = time;
        }

        public long RealValue
        {
            get
            {
                return (long)nudDelay.Value;
            }
            set
            {
                nudDelay.Value = value;
            }
        }

        public long Value
        {
            get // return MS
            {
                switch (Time)
                {
                    default:
                    case Times.Milliseconds:
                        return (long)nudDelay.Value;
                    case Times.Seconds:
                        return (long)(nudDelay.Value * 1000);
                    case Times.Minute:
                        return (long)(nudDelay.Value * 60000);
                    case Times.Hours:
                        return (long)(nudDelay.Value * 3600000);
                }
            }
            set // value = MS
            {
                switch (Time)
                {
                    default:
                    case Times.Milliseconds:
                        nudDelay.Value = value;
                        break;
                    case Times.Seconds:
                        nudDelay.Value = value / 1000;
                        break;
                    case Times.Minute:
                        nudDelay.Value = value / 60000;
                        break;
                    case Times.Hours:
                        nudDelay.Value = value / 3600000;
                        break;
                }
            }
        }

        private void nudDelay_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null) ValueChanged(this, e);
        }

        private void cbDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Time = (Times)cbDelay.SelectedIndex;
            if (Time == Times.Milliseconds)
            {
                nudDelay.DecimalPlaces = 0;
            }
            else
            {
                nudDelay.DecimalPlaces = 1;
            }
            this.Value = this.Value;
            if (SelectedIndexChanged != null) SelectedIndexChanged(this, e);
        }
    }
}