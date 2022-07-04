using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ImageProcessService.Model
{

    public class DropShadowAttributes
    {
        public string keyword { get; set; }

        [JsonProperty("offset-x")]
        public string OffsetX { get; set; }

        [JsonProperty("offset-y")]
        public string OffsetY { get; set; }

        public string ImageSavePath { get; set; }
    }

    public class ResizeImageAttributes
    {
        public string height { get; set; }
        public string width { get; set; }
        public string ImageSavePath { get; set; }
    }

    public class Root
    {
        public ResizeImageAttributes ResizeImageAttributes { get; set; }
        public DropShadowAttributes DropShadowAttributes { get; set; }
    }

}
