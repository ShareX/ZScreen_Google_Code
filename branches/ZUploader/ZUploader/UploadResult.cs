using System.Collections.Generic;

namespace ZUploader
{
    public class UploadResult
    {
        public string URL { get; set; }
        public string ThumbnailURL { get; set; }
        public string DeletionURL { get; set; }
        public List<string> Errors { get; set; }
    }
}