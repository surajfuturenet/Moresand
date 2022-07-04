using PluginInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DropShadow
{
    public class DropShadow : IPlugin
    {
        private string messege = "I am adding the drop shadow to the image to mentioned specifications";
        private static DropShadowAttributes attributes;

        /// <summary>
        /// This method will not do anything for the image.
        /// Need to do the manupulations needed for the image as required.
        /// </summary>
        /// <param name="_attributes"></param>
        /// <returns></returns>
        public string ProcessImage(IImageAttributes _attributes)
        {
            attributes = (DropShadowAttributes)_attributes;
            
            string fullpath = string.Concat(_attributes.ImageSavePath, "Dropshadow.txt");

            File.WriteAllText(fullpath, String.Concat(messege, " ", DateTime.Now.ToString()));
            string readText = File.ReadAllText(fullpath);

            return attributes.ImageSavePath;
        }
    }
}
