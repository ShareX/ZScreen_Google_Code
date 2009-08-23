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
            FlickrUploader flickr = new FlickrUploader("72157621991839563-23481544f632f6b60");

            FlickrUploader.AuthInfo auth = flickr.CheckToken(flickr.Token);
            flickr.UserID = auth.UserID;

            flickr.Settings.Title = "Title test";
            flickr.Settings.Description = "Description test";
            flickr.Settings.Tags = "Tag1,Tag2,Tag3";
            flickr.Settings.IsPublic = "0";
            flickr.Settings.IsFriend = "1";
            flickr.Settings.IsFamily = "1";
            flickr.Settings.SafetyLevel = "1";
            flickr.Settings.ContentType = "2";
            flickr.Settings.Hidden = "2";

            string url = flickr.UploadImage(@"C:\Users\PC\Desktop\main.png").GetFullImageUrl();
            Console.WriteLine(url);

            Console.ReadLine();
        }
    }
}