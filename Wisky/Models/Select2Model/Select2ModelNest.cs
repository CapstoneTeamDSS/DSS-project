using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisky.Models.Select2Model
{
    public class Select2ModelNest
    {
        public string text { get; set; }
        public List<Select2Model> children { get; set; }
        public Select2ModelNest(string text, List<Select2Model> child)
        {
            this.text = text;
            this.children = child;
        }
    }
}
