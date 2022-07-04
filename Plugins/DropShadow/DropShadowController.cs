using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DropShadow
{
    [ApiController]
    [Route("[controller]")]
    public class DropShadowController : ControllerBase
    {
        private DropShadow dropShadow;

        public DropShadowController()
        {
            
        }

        [HttpGet("Version")]
        public object Version()
        {
            return "Drop Shadow Controller  v 1.0";
        }

        [HttpPost("ProcessImage")]
        public async Task<ActionResult<string>> ProcessImage(DropShadowAttributes attributes)
        {
            dropShadow = new DropShadow();
            return dropShadow.ProcessImage(attributes);
        }
    }
    
}
