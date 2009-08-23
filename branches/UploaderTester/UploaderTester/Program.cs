using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.ImageUploaders;

namespace UploaderTester
{
    class Program
    {
        static void Main(string[] args)
        {
            FlickrUploader flickr = new FlickrUploader("72157622106795898-65be13742be74e79");
            
            /*
            flickr.GetAuthFrob();
            Process.Start(flickr.GetAuthLink());
            Console.ReadLine();
            flickr.GetAuthToken();
            */

            string url = flickr.UploadImage(@"C:\Users\PC\Desktop\main.png").URL;
            Console.WriteLine(url);

            Console.ReadLine();
        }
    }
}