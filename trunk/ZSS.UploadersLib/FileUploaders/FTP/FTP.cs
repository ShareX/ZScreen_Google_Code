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
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using Starksoft.Net.Ftp;
using Starksoft.Net.Proxy;

namespace UploadersLib
{
    public sealed class FTP : IDisposable
    {
        public delegate void FTPProgressEventHandler(float percentage);

        public delegate void FTPDebugEventHandler(string text);

        public event FTPProgressEventHandler ProgressChanged;

        public event FTPDebugEventHandler DebugMessage;

        public FTPAccount Account { get; set; }

        public FtpClient Client { get; set; }

        public FTP(FTPAccount account)
        {
            this.Account = account;
            this.Client = new FtpClient();

            Client.Host = account.Server;
            Client.Port = account.Port;
            Client.DataTransferMode = account.IsActive ? TransferMode.Active : TransferMode.Passive;

            IWebProxy proxy = Uploader.ProxySettings;
            if (proxy != null)
            {
                try
                {
                    string testUrl = "http://google.com";
                    Client.Proxy = new HttpProxyClient(proxy.GetProxy(new Uri(testUrl)).Host, proxy.GetProxy(new Uri(testUrl)).Port);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            Client.TransferProgress += new EventHandler<TransferProgressEventArgs>(OnTransferProgressChanged);
            Client.ClientRequest += new EventHandler<FtpRequestEventArgs>(Client_ClientRequest);
            Client.ServerResponse += new EventHandler<FtpResponseEventArgs>(Client_ServerResponse);
        }

        private void Client_ServerResponse(object sender, FtpResponseEventArgs e)
        {
            OnDebugMessage("Server: " + e.Response.RawText);
        }

        private void Client_ClientRequest(object sender, FtpRequestEventArgs e)
        {
            OnDebugMessage("Client: " + e.Request.Text);
        }

        private void OnDebugMessage(string text)
        {
            if (DebugMessage != null)
            {
                DebugMessage(text);
            }
        }

        private void OnTransferProgressChanged(object sender, TransferProgressEventArgs e)
        {
            if (ProgressChanged != null)
            {
                /*Console.WriteLine("{0}/{1} - {2}% - {3} - {4}", e.TotalBytesTransferred / 1024, e.TotalBytes / 1024, e.Percentage,
                   e.EstimatedCompleteTime.TotalMilliseconds, e.ElapsedTime.TotalMilliseconds);*/
                ProgressChanged(e.Percentage);
            }
        }

        public void Connect(string username, string password)
        {
            if (!Client.IsConnected)
            {
                Client.Open(username, password);
            }
        }

        public void Connect()
        {
            Connect(Account.Username, Account.Password);
        }

        public void Disconnect()
        {
            Client.Close();
        }

        public void UploadData(Stream stream, string remotePath)
        {
            Connect();
            Client.PutFile(stream, remotePath, FileAction.Create);
        }

        public void UploadData(byte[] data, string remotePath)
        {
            using (MemoryStream stream = new MemoryStream(data, false))
            {
                UploadData(stream, remotePath);
            }
        }

        public void UploadFile(string localPath, string remotePath)
        {
            using (FileStream stream = new FileStream(localPath, FileMode.Open))
            {
                UploadData(stream, remotePath);
            }
        }

        public void UploadImage(Image image, string remotePath)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, image.RawFormat);
                UploadData(stream, remotePath);
            }
        }

        public void UploadText(string text, string remotePath)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(text), false))
            {
                UploadData(stream, remotePath);
            }
        }

        public FtpItemCollection GetDirList(string remotePath)
        {
            Connect();
            return Client.GetDirList(remotePath);
        }

        public void DownloadFile(string remotePath, string localPath)
        {
            Connect();
            Client.GetFile(remotePath, localPath);
        }

        public void MakeDirectory(string remotePath)
        {
            Connect();
            Client.MakeDirectory(remotePath);
        }

        public void Rename(string fromRemotePath, string toRemotePath)
        {
            Connect();
            Client.Rename(fromRemotePath, toRemotePath);
        }

        public void DeleteFile(string remotePath)
        {
            Connect();
            Client.DeleteFile(remotePath);
        }

        public void DeleteDirectory(string remotePath)
        {
            Connect();

            string filename = FTPHelpers.GetFileName(remotePath);
            if (filename == "." || filename == "..")
            {
                return;
            }

            FtpItemCollection files = GetDirList(remotePath);

            foreach (FtpItem file in files)
            {
                if (file.ItemType == FtpItemType.Directory)
                {
                    DeleteDirectory(file.FullPath);
                }
                else
                {
                    DeleteFile(file.FullPath);
                }
            }

            Client.DeleteDirectory(remotePath);
        }

        public void Dispose()
        {
            Disconnect();
            Client.Dispose();
        }
    }
}