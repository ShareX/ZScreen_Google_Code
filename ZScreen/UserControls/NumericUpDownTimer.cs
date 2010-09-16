#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v2)

using System;
using System.ComponentModel;
using System.Windows.Forms;
using ZScreenLib;

namespace ZScreenGUI
{
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
                    case Times.Minutes:
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
                    case Times.Minutes:
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
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
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
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, e);
            }
        }
    }
}