using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UploadersLib.FileUploaders;

namespace UploaderTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string url;
            SendSpace sendSpace = new SendSpace();

            try
            {
                string token = sendSpace.AuthCreateToken();
                SendSpace.LoginInfo loginInfo = sendSpace.AuthLogin(token, "Jaex", "pass");
                sendSpace.CurrentUploadInfo = sendSpace.UploadGetInfo(loginInfo.SessionKey);
                url = sendSpace.Upload(@"C:\Users\PC\Desktop\test.rar");
                Console.WriteLine("Success: {0}", url);

                sendSpace.CurrentUploadInfo = sendSpace.AnonymousUploadGetInfo();
                url = sendSpace.Upload(@"C:\Users\PC\Desktop\test.rar");
                Console.WriteLine("Success: {0}", url);
            }
            catch
            {
                Console.WriteLine(sendSpace.ToErrorString());
            }

            Console.ReadLine();
        }
    }
}