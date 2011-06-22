namespace HelpersLib
{
    public class PrintSettings
    {
        public int Margin { get; set; }
        public bool AutoRotateImage { get; set; }
        public bool AutoScaleImage { get; set; }
        public bool AllowEnlargeImage { get; set; }
        public bool CenterImage { get; set; }

        public PrintSettings()
        {
            Margin = 10;
            AutoRotateImage = true;
            AutoScaleImage = true;
            AllowEnlargeImage = false;
            CenterImage = false;
        }
    }
}