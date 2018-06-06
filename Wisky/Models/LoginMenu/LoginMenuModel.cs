using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Wisky.Models.LoginMenu.LoginMenuEnums;

namespace Wisky.Models.LoginMenu
{
    public class LoginMenuModel
    {   
        public string Id { get; set; }
        public string InlineCss { get; set; }
        public double SeismicIntensity { get; set; }
        public int SelectedLanguage { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double XMobile { get; set; }
        public double YMobile { get; set; }
        public double Width { get; set; }
        public double WidthMobile { get; set; }
        public string CssClass { get; set; }
        public string CssPosition { get; set; } = "position-middle";
        /// <summary>
        /// Render vertical or justified group item
        /// 1: Justified, 2: Vertical
        /// </summary>
        public MenuType GroupItemsType { get; set; }

        /// <summary>
        /// Include controls
        /// </summary>
        public List<GroupItem> GroupItems { get; set; }
        public string WelcomeUserPart1 { get; set; } = "Welcome back ";
        public string WelcomeUserDefault { get; set; } = "!";
        public string WelcomeUserPart2 { get; set; } = " Click to enjoy free wifi";
        public string WelcomeUserBtn { get; set; } = "Continue";
    }
}