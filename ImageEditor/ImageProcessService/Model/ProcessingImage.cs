using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageProcessService.Model
{
    public class ProcessingImage
    {
        public IFormFile file { get; set; }
        public string transformations { get; set; } 
    }
}
