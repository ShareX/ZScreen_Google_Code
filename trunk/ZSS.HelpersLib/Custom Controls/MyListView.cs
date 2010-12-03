#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2010 ZScreen Developers

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

using System.Diagnostics;
using System.Windows.Forms;

namespace HelpersLib.Custom_Controls
{
    public class MyListView : ListView
    {
        private const int WM_ERASEBKGND = 0x14;

        public MyListView()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.EnableNotifyMessage, true);

            this.FullRowSelect = true;
            this.HideSelection = false;
            this.View = View.Details;
        }

        public void AutoResizeLastColumn()
        {
            if (this.View == View.Details && this.Columns.Count > 0)
            {
                if (this.Columns.Count == 1)
                {
                    if (this.Columns[0].Width != this.ClientSize.Width)
                    {
                        this.Columns[0].Width = this.ClientSize.Width;
                    }
                }
                else
                {
                    int columns = this.Columns.Count - 1;
                    int width = 0;

                    for (int i = 0; i < columns; i++)
                    {
                        width += this.Columns[i].Width;
                    }

                    this.Columns[this.Columns.Count - 1].Width = this.ClientSize.Width - width;
                }
            }
        }

        [DebuggerStepThrough]
        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg != WM_ERASEBKGND)
            {
                base.OnNotifyMessage(m);
            }
        }
    }
}