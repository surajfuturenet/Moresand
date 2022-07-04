using System;
using System.Collections.Generic;
using System.Text;

namespace PluginInterfaces
{
    public interface IPlugin
    {
        public static IImageAttributes attributes;

        public string ProcessImage(IImageAttributes _attributes);
    }
}
