using PluginInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ResizeImage
{
    public class Resize : IPlugin
    {
        private string messege = "I am resize";
        private ResizeImageAttributes attributes;

        public string ProcessImage(IImageAttributes _attributes)
        {
            attributes = (ResizeImageAttributes)_attributes;

            string fullpath = string.Concat(_attributes.ImageSavePath,"\\Resize.txt");
            File.WriteAllText(fullpath, String.Concat(messege," ", "h :- ", attributes.height, "w:- ", attributes.width, DateTime.Now.ToString()));
            string readText = File.ReadAllText(fullpath);

            return attributes.ImageSavePath;
        }
    }
}
