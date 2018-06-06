using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Wisky.Models.LoginMenu.LoginMenuEnums;

namespace Wisky.Models.LoginMenu
{
    /// <summary>
    /// button or textfield
    /// </summary>
    public class ControlItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        
        /// <summary>
        /// Type button or text...
        /// 1: button\n
        /// 2: input textfield\n
        /// 3: input password field\n
        /// 4: input email field\n
        /// </summary>
        public MenuControlType Type { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public string Values { get; set; }

        public virtual string CssClassALayer1 { get; set; }
        public virtual string CssClassSpanLayer1 { get; set; }
        public virtual string CssClassDivLayer1 { get; set; }
        public virtual string CssInlineLayer1 { get; set; }
        public virtual string CssClassLabelLayer2 { get; set; }
        public virtual string CssClassInputLayer2 { get; set; }
        public virtual string CssClassPLayer2 { get; set; }
        public virtual string CssClassSelectLayer2 { get; set; }
        public virtual string CssClassDivLayer2 { get; set; }
        public virtual string CssClassLabelLayer3 { get; set; }
        public virtual string CssClassInputLayer3 { get; set; }
        public virtual string CssClassOptionLayer3 { get; set; }
        public virtual string CssClassSpanLayer3 { get; set; }
        public virtual string CssClassDivLayer3 { get; set; }

        public virtual string CssMainClass { get; set; }
        public virtual string CssMainInlineLayer { get; set; }
        public string CssIcon { get; set; }


        public string Onclick { get; set; }
        public string PlaceHolder { get; set; }
        public bool IsRequired { get; set; }

    }
    
    public class ControlCheckBox: ControlItem
    {
        
    }

    public class ControlDropdownList : ControlItem
    {
        public override string CssClassDivLayer1 { get; set; } = "content col-md-12";
        public override string CssMainClass { get; set; } = "selectpicker";

    }

    public class ControlButton : ControlItem
    {
        public override string CssClassDivLayer1 { get; set; } = "col-md-12";
        public override string CssClassDivLayer3 { get; set; } = "ripple-container";
    }

    public class ControlHref : ControlItem
    {
       
    }

    public class ControlTextBox: ControlItem
    {
        public override string CssClassDivLayer1 { get; set; } = "col-md-12";
        public override string CssClassDivLayer2 { get; set; } = "form-group label-floating is-empty";
        public override string CssClassLabelLayer3 { get; set; } = "control-label";
        public override string CssMainClass { get; set; } = "form-control";
        public override string CssClassSpanLayer3 { get; set; } = "material-input";

    }

}