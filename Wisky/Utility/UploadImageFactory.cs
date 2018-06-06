using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using Wisky.Data.Utility;

namespace Wisky.Utility
{
    public class UploadViewModel
    {
        public string ImageUrl { get; set; }
    }

    interface IUploadImage
    {
        Task<UploadViewModel> Upload();
    }

    class UploadImageBlob : IUploadImage
    {
        public CloudBlobContainer ImagesBlobContainer{ get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        

        public async Task<UploadViewModel> Upload()
        {
            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            // Retrieve reference to a blob. 
            var dir = ImagesBlobContainer.GetDirectoryReference("WiskyCloudImage");
            CloudBlockBlob imageBlob = dir.GetBlockBlobReference(blobName);
            // Create the blob by uploading a local file.
            using (var fileStream = ImageFile.InputStream)
            {
                await imageBlob.UploadFromStreamAsync(fileStream);
            }
            var returnValue = new UploadViewModel { ImageUrl = imageBlob.Uri.ToString() };
            return returnValue;
        }
    }


    class UploadImageWiskyCDN : IUploadImage
    {
        public HttpPostedFileBase ImageFile { get; set; }
        

        public async Task<UploadViewModel> Upload()
        {
            string apiUrl = WebConfigurationManager.AppSettings["UploadApi"];
            string token = WebConfigurationManager.AppSettings["SecretKeyApi"];

            Stream image = ImageFile.InputStream;

            // Convert each of the three inputs into HttpContent objects
            HttpContent tokenContent = new StringContent(token);
            // examples of converting both Stream and byte [] to HttpContent objects
            // representing input type file
            HttpContent imageStreamContent = new StreamContent(image);
            //HttpContent bytesContent = new ByteArrayContent(fileBytes);

            // Submit the form using HttpClient and 
            // create form data as Multipart (enctype="multipart/form-data")

            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                // Add the HttpContent objects to the form data
                formData.Add(tokenContent, "token");
                // <input type="text" name="token" />

                formData.Add(imageStreamContent, ImageFile.FileName, ImageFile.FileName);
                // <input type="file" name="ImageFile.FileName" />
                //formData.Add(bytesContent, "file2", "file2");
            
                // a form with attributes (action="{url}" method="post")
                var response = await client.PostAsync(apiUrl, formData);

                // ensure the request was a success
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                string content = response.Content.ReadAsStringAsync().Result;
                string firstImageUrl = "";
                if (!string.IsNullOrEmpty(content))
                {
                    firstImageUrl = content.Split('|')[0];
                }
                var returnValue = new UploadViewModel { ImageUrl = firstImageUrl };
                return returnValue;
            }
        }
    }

    public class UploadImageFactory
    {

        IUploadImage uploadImageBlob;
        IUploadImage uploadImageWiskyCDN;

        public async Task<UploadViewModel>  UploadImageAsync(HttpPostedFileBase imageFile, CloudBlobContainer imagesBlobContainer = null, UploadCDNEnum type = UploadCDNEnum.CDNWiksy)
        {
            if (type == UploadCDNEnum.Blob)
            {
                uploadImageBlob = new UploadImageBlob()
                {
                    ImageFile = imageFile,
                    ImagesBlobContainer = imagesBlobContainer
                };
                var result = await uploadImageBlob.Upload();
                return result; 
            }
            else if (type == UploadCDNEnum.CDNWiksy)
            {
                uploadImageWiskyCDN = new UploadImageWiskyCDN
                {
                    ImageFile = imageFile
                };
                var result = await uploadImageWiskyCDN.Upload();
                return result;
            }
            return new UploadViewModel { ImageUrl = "nothing"};
        }

    }
}