# Moresand
 This is a plugin management framework

Setup Image Process Service

Appsettings.json

1.1 DLL Folder Path
Change the following Path and set it to your local path. This is where the dll’s are residing. 

  "pluginspath": "D:\\Omni Technologies\\Moresand\\ImageEditor\\Plugins",

They are inside the “ImageEditor\Plugins” folder.

2.	Start the WEB API

Make sure project is running and web service is working.

 

3.	Check the Custom Plugins are working
Once plugins are imported correctly, you should be able to view the plugin versions via browser
http://localhost:53015/ResizeImage/version
http://localhost:53015/ResizeImage/version
 
If there is a problem you will need to check the dll file path is correctly set. 

Set up on Postman

Make sure “ImageProcessService” is running.

Change the URL as per your computer

http://localhost:53015/api/ImageUpload/upload

Method is “POST”

 
Set up form-data in Body section.

Key						value
file						select an image (now any file is fine)
transformations				look at the example below 

[
  {
    "ResizeImageAttributes": {
      "height": "100px",
      "width": "30px"
    }
  },
  {
    "DropShadowAttributes": {
      "keyword": "box-shadow",
      "offset-x": "20px",
      "offset-y": "10px"
    }
  }
]

Once you enter the json for transformations send the request.

Check the folder “wwwroot\ProcessingImages” of “ImageProcessService” for logs.




