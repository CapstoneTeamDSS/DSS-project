using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wisky.Models.LoginMenu
{
    public class MultiLanguage
    {
        public IList<ControlIitemLanguage> ControlIitemLanguages { get; set; }
    }

    public class Languages
    {
        public string EngLish { get; set; }
        public string Vietnamese { get; set; }
    }

    public class ControlIitemLanguage
    {
        public string Id { get; set; }
        public Languages Languages { get; set; }
    }

    
}