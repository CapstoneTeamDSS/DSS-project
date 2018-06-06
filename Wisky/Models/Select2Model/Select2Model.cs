using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisky.Models.Select2Model
{
    public class Select2Model
    {
        public string id { get; set; }
        public string text { get; set; }

        public string other { get; set; }

        public Select2Model(string id, string text, string other)
        {
            this.id = id;
            this.text = text;
            this.other = other;
        }
    }
}
