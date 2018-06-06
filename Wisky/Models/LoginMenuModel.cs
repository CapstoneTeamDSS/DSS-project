using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wisky.Models.Old
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
        /// <summary>
        /// Render vertical or justified group item
        /// 1: Justified, 2: Vertical
        /// </summary>
        public int GroupItemsType { get; set; }

        /// <summary>
        /// Include controls
        /// </summary>
        public List<GroupItem> GroupItems { get; set; }

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


        /// <summary>
        /// button or textfield
        /// </summary>
        public class ControlItem
        {
            public string CssClass { get; set; }

            /// <summary>
            /// Store style of control
            /// </summary>
            public string Css { get; set; }

            public bool IsRequired { get; set; }

            /// <summary>
            /// Type button or text...
            /// 1: button\n
            /// 2: input textfield\n
            /// 3: input password field\n
            /// 4: input email field\n
            /// </summary>
            public int Type { get; set; }
            /// <summary>
            /// TextField place holder
            /// </summary>
            public string PlaceHolder { get; set; }
            /// <summary>
            /// Detect the action onclick event
            /// </summary>
            public string Onclick { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            /// <summary>
            /// Include text of control
            /// </summary>
            public string Text { get; set; }
            public int X { get; set; }
            public  int Y { get; set; }
        }

        public enum  MenuControlType
        {
            Button = 1,
            TextBox = 2,
            Password = 3,
            Email = 4,
            Href = 5,
            DropdownList = 6,
            Checkbox = 7
        }

        public enum MenuType
        {
            Horizontal = 1,
            Vertical = 2,
            Custom = 3
        }
    }
}