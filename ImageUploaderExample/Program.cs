using System;
using System.Collections.Generic;
using System.Text;
using ZSS.ImageUploader;

namespace ZSS
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageShackUploader imageShack = new ImageShackUploader();

            List<ImageFile> uris = imageShack.UploadImage(@"C:\test.png");

            foreach(ImageFile imageF in uris)
            {
                switch(imageF.Type)
                {
                    case ImageFile.ImageType.FULLIMAGE:
                        Console.WriteLine("Image link: {0}", imageF.URI);
                        break;
                    case ImageFile.ImageType.THUMBNAIL:
                        Console.WriteLine("Thumbnail link: {0}", imageF.URI);
                        break;
                }
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey(true);
        }
    }
}
