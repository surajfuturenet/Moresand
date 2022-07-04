using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResizeImage
{
    [ApiController]
    [Route("[controller]")]
    public class ResizeImageController : ControllerBase
    {
        private Resize resize;
        public ResizeImageController()
        {
        }

        [HttpGet("Version")]
        public object Version()
        {
            return "Resize Image Controller  v 1.0";
        }

        [HttpPost("ProcessImage")]
        public async Task<ActionResult<string>> ProcessImage(ResizeImageAttributes attributes)
        {
            resize = new Resize();
            return resize.ProcessImage(attributes);
        }

    }
    
}
