using System;
using System.Collections.Generic;
using System.Text;

namespace PluginInterfaces
{
    interface IResizeImageAttributes : IImageAttributes
    {
        public string height { get; set; }
        public string width { get; set; }
    }
}
