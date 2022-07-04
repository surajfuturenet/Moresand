using PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResizeImage
{
    public class ResizeImageAttributes : IImageAttributes
    {
        public string height { get; set; }
        public string width { get; set; }
        public string ImageSavePath { get; set; }
    }
}
