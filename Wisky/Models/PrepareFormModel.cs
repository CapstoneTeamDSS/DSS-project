using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wisky.Models.Old
{
    public class PrepareFormModel
    {
        public int SelectedLanguage { get; set; }

        public float WidthFormDesktop { get; set; }
        public float XFormDesktop { get; set; }
        public float YFormDesktop { get; set; }
        public float XFormMobile { get; set; }
        public float YFormMobile { get; set; }
        public float SeismicIntensity { get; set; }

        public string RegisterAttribute1Def { get; set; }
        public string RegisterAttribute2Def { get; set; }
        public string RegisterAttribute3Def { get; set; }
        public string RegisterAttribute4Def { get; set; }
        public string RegisterAttribute5Def { get; set; }

        public string RegisterAttribute1IsRequired { get; set; }
        public string RegisterAttribute2IsRequired { get; set; }
        public string RegisterAttribute3IsRequired { get; set; }
        public string RegisterAttribute4IsRequired { get; set; }
        public string RegisterAttribute5IsRequired { get; set; }

        //Button Attribute
        public string ClickThroughButtonBackgroundColor { get; set; }
        public string ClickThroughButtonTextColor { get; set; }



    }
}