using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Wisky.Data.Utility
{
    public static class Utility
    {
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

        

        public static DateTime GetEndOfDate(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
        }

        public static DateTime GetStartOfDate(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
        }

        public static DateTime GetCurrentDateTime()
        {

            #region Get DateTime.Now
            //Get time UTC 
            DateTime utcNow = DateTime.UtcNow;
            //Parse UTC to time SE Asia
            DateTime datetimeNow = TimeZoneInfo.ConvertTime(utcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            #endregion

            return datetimeNow;
        }
    }
}