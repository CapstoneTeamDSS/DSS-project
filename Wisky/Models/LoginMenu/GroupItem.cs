using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wisky.Models.LoginMenu
{
    public class GroupItem
    {
        public string CssClass { get; set; }
        public string Css { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int DeviceType { get; set; }
        public int LoginModeCode { get; set; }
        /// <summary>
        /// Include button or button + textfields
        /// </summary>
        public List<ControlItem> ControlItems { get; set; }
        public bool IsShowing(int loginMode)
        {
            return (loginMode & LoginModeCode) != 0;
        }
    }
}