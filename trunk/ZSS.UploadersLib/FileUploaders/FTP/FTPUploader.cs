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
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Starksoft.Net.Ftp;
using Starksoft.Net.Proxy;
using ZSS;

namespace UploadersLib.FileUploaders.FTP
{
    public sealed class FTPUploader : IDisposable
    {
        public delegate void FTPProgressEventHandler(float percentage);
        public event FTPProgressEventHandler ProgressChanged;

        private FTPAccount account;
        private FtpClient client;

        public FTPUploader(FTPAccount account)
        {
            this.account = account;
            this.client = new FtpClient();

            client.Host = account.Server;
            client.Port = account.Port;
            client.DataTransferMode = account.IsActive ? TransferMode.Active : TransferMode.Passive;
            client.TransferProgress += new EventHandler<TransferProgressEventArgs>(OnTransferProgressChanged);
        }

        private void OnTransferProgressChanged(object sender, TransferProgressEventArgs e)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(e.Percentage);
            }
        }

        public void Connect(string username, string password)
        {
            if (!client.IsConnected)
            {
                client.Open(username, password);
            }
        }

        public void Connect()
        {
            Connect(account.Username, account.Password);
        }

        public void Disconnect()
        {
            client.Close();
        }

        public bool UploadData(Stream stream, string remotePath)
        {
            Connect();

            client.PutFile(stream, remotePath, FileAction.Create);

            return true;
        }

        public bool UploadData(byte[] data, string remotePath)
        {
            using (MemoryStream stream = new MemoryStream(data, false))
            {
                return UploadData(stream, remotePath);
            }
        }

        public bool UploadFile(string localPath, string remotePath)
        {
            using (FileStream stream = new FileStream(localPath, FileMode.Open))
            {
                return UploadData(stream, remotePath);
            }
        }

        public bool UploadText(string text, string remotePath)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(text), false))
            {
                return UploadData(stream, remotePath);
            }
        }

        public void Dispose()
        {
            Disconnect();
            client.Dispose();
        }
    }
}