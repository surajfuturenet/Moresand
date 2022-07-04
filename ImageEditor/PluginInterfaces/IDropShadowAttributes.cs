using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PluginInterfaces
{
    interface IDropShadowAttributes :IImageAttributes
    {
        public string keyword { get; set; }

        [JsonProperty("offset-x")]
        public string OffsetX { get; set; }

        [JsonProperty("offset-y")]
        public string OffsetY { get; set; }
    }
}
