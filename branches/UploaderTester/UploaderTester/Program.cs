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
            SendSpace sendSpace = new SendSpace();
            sendSpace.AuthRegister("Jaex", "Berk", "blabla@gmail.com", "pass");
            string token = sendSpace.AuthCreateToken();
            SendSpace.LoginInfo loginInfo = sendSpace.AuthLogin(token, "Jaex", "pass");
            string sessionKey = loginInfo.SessionKey;
            if (sendSpace.AuthCheckSession(sessionKey))
            {
                sendSpace.CurrentUploadInfo = sendSpace.UploadGetInfo(sessionKey, "0");
                string url = sendSpace.Upload(@"C:\Users\PC\Desktop\test.rar");
                Console.WriteLine("Success: {0}", url);
            }

            Console.ReadLine();
        }
    }
}