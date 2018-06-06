using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wisky.Models.LoginMenu
{
    public class LoginMenuEnums
    {
        public enum MenuControlType
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