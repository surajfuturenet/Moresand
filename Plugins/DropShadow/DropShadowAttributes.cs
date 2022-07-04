using Newtonsoft.Json;
using PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DropShadow
{
    public class DropShadowAttributes : IImageAttributes
    {

        public string keyword { get; set; }

        [JsonProperty("offset-x")]
        public string OffsetX { get; set; }

        [JsonProperty("offset-y")]
        
        public string OffsetY { get; set; }

        public string ImageSavePath { get; set; }
    }
}
