using Microsoft.WindowsAzure.Storage.Blob;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Wisky.Data.Utility;
using Wisky.Redis;

namespace Wisky.Utility
{
    public static class Utility
    {
        public static int PriorityLoginMode()
        {
            //            return (int)LoginModeEnum.Membership
            //                   + (int)LoginModeEnum.Facebook
            //                   + (int)LoginModeEnum.FacebookCheckIn
            //                   + (int)LoginModeEnum.GooglePlus
            //                   + (int)LoginModeEnum.Account
            //                   + (int)LoginModeEnum.SMS
            //                   + (int)LoginModeEnum.Email;

            return (int)LoginModeEnum.Facebook
                   + (int)LoginModeEnum.GooglePlus;
        }

       
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static string RemoveUnuseLoginMethod(this string value, LoginModeEnum usingMode)
        {
            var mappingList = Enum.GetValues(typeof(LoginModeEnum)).Cast<int>().Where(a => a != (int)usingMode);
            return value.RemoveBlock(mappingList, "<!--type{0}-->");
        }

        public static string RemoveUnuseAdvertisingMethod(this string value, AdFormatEnum usingMethod)
        {
            var mappingList = Enum.GetValues(typeof(AdFormatEnum)).Cast<int>().Where(a => a != (int)usingMethod);
            return value.RemoveBlock(mappingList, "<!--advertising{0}-->");
        }

        private static string RemoveBlock(this string value, IEnumerable<int> mapingList, string mappingString)
        {
            //init string builder
            var stringBuilder = new StringBuilder(value);
            //last removed string length
            var length = 0;
            foreach (var item in mapingList)
            {
                //find tag mapping of app in html text
                var str = string.Format(mappingString, item);
                if (!value.Contains(str)) continue;
                //find start position
                var start = value.IndexOf(str, StringComparison.Ordinal) - length;
                //find end position
                var end = value.LastIndexOf(str, StringComparison.Ordinal) - length;
                //save length to match position with next string (string after remove)
                length = end - start;
                stringBuilder.Remove(start, end - start);
            }
            return stringBuilder.ToString();
        }

        public static string FillFormatString(this string value, params object[] param)
        {
            return string.Format(value, param);
        }

        //        public static string GetLandingAppInitScript(this IEnumerable<LandingApp> value)
        //        {
        //            var result = new StringBuilder();
        //            result.AppendLine("<script>");
        //            result.AppendLine("window['LandingApps'] = [];");
        //            foreach (var app in value)
        //            {
        //                result.AppendLine(Properties.Resources.LandingAppJsonTemplate
        //                    .FillFormatString(app.AppName, app.Description, app.AppHtmlClass, app.AppUrl));
        //            }
        //            result.AppendLine("</script>");
        //            return result.ToString();
        //        }
        public static string InitLandingAttribute(this string str, string initScript, string title, string logoUrl,
            string macAddress, int apId, params object[] formSubmitUrl)
        {
            //join all parameter to one array
            var listParam = new List<object>
            {
                title,
                initScript,
                logoUrl,
                macAddress,
                apId
            };
            if (formSubmitUrl != null)
            {
                listParam.AddRange(formSubmitUrl);
            }
            return string.Format(str, listParam.ToArray());
        }

        /// <summary>
        /// Initial for advertising page
        /// </summary>
        /// <param name="str">value</param>
        /// <param name="title">Page title</param>
        /// <param name="initScript">Script include in page</param>
        /// <param name="brandLogo">Brand logo</param>
        /// <param name="duration">Duration of advertising</param>
        /// <param name="contentUrl">Background image url/Video url</param>
        /// <param name="destinationUrl">Url to redirect to</param>
        /// <param name="advertisingText">Advertising text</param>
        /// <returns></returns>
        public static string InitAdvertisingAttribute(this string str, string title, string initScript,
            string brandLogo, int duration, string contentUrl, string destinationUrl, string advertisingText)
        {
            var listParam = new List<object>
            {
                title,
                initScript,
                brandLogo,
                duration,
                contentUrl,
                destinationUrl,
                advertisingText
            };
            return string.Format(str, listParam.ToArray());
        }

        public static DateTime? GetDate(this string value, string contries = null)
        {
            switch (contries)
            {
                case "vn":
                    {//date time format dd/MM/yyyy
                        if (string.IsNullOrEmpty(value))
                        {
                            return null;
                        }
                        var dateArr = value.Split('/', '-');
                        return new DateTime(int.Parse(dateArr[2]), int.Parse(dateArr[1]), int.Parse(dateArr[0]));
                    }
                case "us":
                    {//date time format MM/dd/yyyy
                        if (string.IsNullOrEmpty(value))
                        {
                            return null;
                        }
                        var dateArr = value.Split('/');
                        return new DateTime(int.Parse(dateArr[2]), int.Parse(dateArr[0]), int.Parse(dateArr[1]));
                    }
                default:
                    return null;
            }
        }


        //public static PublisherUser GetPublisherUser()
        //{
        //    var _db = new WiSkyEntities();

        //    var Username = HttpContext.Current.User.Identity.Name;
        //    AspNetUser user = _db.AspNetUsers.FirstOrDefault(u => u.UserName.Equals(Username));
        //    if (user != null)
        //    {
        //        var publisherUser = _db.PublisherUsers.FirstOrDefault(p => p.UserId == user.Id);
        //        if (publisherUser != null)
        //        {
        //            return publisherUser;
        //        }
        //    }

        //    return null;
        //}

        public static EType GetAttribute<EType>(this Enum value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            var descriptionAttributes = fieldInfo.GetCustomAttributes(
            typeof(EType), false) as EType[];
            if (descriptionAttributes == null || descriptionAttributes.Length == 0)
                return default(EType);
            return descriptionAttributes[0];
        }

        public static async Task<CloudBlockBlob> UploadAndSaveBlobAsync(CloudBlobContainer imagesBlobContainer, HttpPostedFileBase imageFile)
        {

            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            // Retrieve reference to a blob. 
            var dir = imagesBlobContainer.GetDirectoryReference("WiskyCloudImage");
            CloudBlockBlob imageBlob = dir.GetBlockBlobReference(blobName);
            // Create the blob by uploading a local file.
            using (var fileStream = imageFile.InputStream)
            {
                await imageBlob.UploadFromStreamAsync(fileStream);
            }


            return imageBlob;
        }
        public static async Task<CloudBlockBlob> UploadAndSaveBlobAsync1(CloudBlobContainer imagesBlobContainer, HttpPostedFileBase imageFile)
        {

            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            // Retrieve reference to a blob. 
            var dir = imagesBlobContainer.GetDirectoryReference("Media");
            CloudBlockBlob imageBlob = dir.GetBlockBlobReference(blobName);
            // Create the blob by uploading a local file.
            using (var fileStream = imageFile.InputStream)
            {
                await imageBlob.UploadFromStreamAsync(fileStream);
            }


            return imageBlob;
        }

        public static async Task<UploadViewModel> UploadAndSaveBlobAsyncNew(CloudBlobContainer imagesBlobContainer, HttpPostedFileBase imageFile)
        {
            UploadImageFactory uif = new UploadImageFactory();
            var result = await uif.UploadImageAsync(imageFile, imagesBlobContainer, UploadCDNEnum.Blob);
            return result;
        }

        #region connect to Redis
        public static IDatabase GetDatabase(IDatabase db)
        {
            if (db == null)
            {
                //Store to Redis
                var RedisConnection = RedisConnectionFactory.GetConnection();
                db = RedisConnection.GetDatabase();
            }
            return db;
        }
        #endregion connect to Redis


    }
}