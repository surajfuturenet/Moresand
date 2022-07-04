using ImageProcessService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ImageProcessService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {

        private static IConfiguration configuration;
        private static IWebHostEnvironment webHostEnvironment;
        private string file_directory_path = "";

        public ImageUploadController(IWebHostEnvironment _webHostEnvironment, IConfiguration _configuration)
        {
            webHostEnvironment = _webHostEnvironment;
            configuration = _configuration;
        }

        /// <summary>
        /// This is the base method for the image edit. Client will send the Image Data along with a JSON formatted 
        /// list of transformations. 
        /// First image will be saved to a directory in wwwroot called 'ProcessingImages'.
        /// Then iterating through the transformations requested (e.g resize, dropshadow etc..)
        /// Changes will be done at each transformation level and update the same file in the directory saved.
        /// finally the file location will be sent as a result. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload")]
        public async Task<string> Upload([FromForm] ProcessingImage obj)
        {
            //base folder where file will be saved
            file_directory_path = webHostEnvironment.WebRootPath + "\\ProcessingImages\\";

            //image full path with name
            string imagefilefullpath = "";

            //transformations requested for the file. 
            var transformations = obj.transformations;

            if (obj.file != null && obj.file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(file_directory_path))
                    {
                        Directory.CreateDirectory(file_directory_path);
                    }
                    using (FileStream filestream = System.IO.File.Create(file_directory_path + obj.file.FileName))
                    {
                        obj.file.CopyTo(filestream);
                        filestream.Flush();

                        imagefilefullpath = file_directory_path + "\\" + obj.file.FileName;

                    }

                    // Call for all the transformations to the file based on requested transformations and their values.
                    await this.ImageTransformations(obj.transformations, imagefilefullpath);

                    return file_directory_path + "\\" + obj.file.FileName;


                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                return "No File Data to Upload";
            }




        }

        private async Task<string> ImageTransformations(string imagetransformations, string imagecopyfullpath)
        {
            string result = "";
            var baseurl = $"{Request.Scheme}://{Request.Host.Value}/";

            List<Root> imagechanges = JsonConvert.DeserializeObject<List<Root>>(imagetransformations);

            if (imagechanges.Count > 0)
            {
                foreach (Root root in imagechanges)
                {
                    if (root.DropShadowAttributes != null)
                    {
                        //passing the file saved location to plugin so that plugin can directly handle the transformations and replace the file.
                        root.DropShadowAttributes.ImageSavePath = file_directory_path;

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(String.Concat(baseurl, "DropShadow/"));
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                            var response = await client.PostAsJsonAsync("ProcessImage", root.DropShadowAttributes);

                            if (response.IsSuccessStatusCode)
                            {
                                string res = await response.Content.ReadFromJsonAsync<string>();
                            }

                            //need to handle errors
                        }

                    }
                    else if (root.ResizeImageAttributes != null)
                    {
                        //passing the file saved location to plugin so that plugin can directly handle the transformations and replace the file.
                        root.ResizeImageAttributes.ImageSavePath = file_directory_path;

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(String.Concat(baseurl, "ResizeImage/"));
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                            var response = await client.PostAsJsonAsync("ProcessImage", root.ResizeImageAttributes);

                            if (response.IsSuccessStatusCode)
                            {
                                string res = await response.Content.ReadFromJsonAsync<string>();
                            }

                            //need to handle errors
                        }

                    }
                    else
                    {
                        //throw new Exception("Requested transformation is not avialble.");
                    }

                }
            }

            return result;
        }
    }
}
