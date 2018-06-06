using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisky.Models.Select2Model
{
    public class Select2PageResults
    {
        public int Total;
        public List<Select2Model> Results { get; set; }
    }
    public class Select2PageResultsNest
    {
        public int Total;
        public List<Select2ModelNest> Results { get; set; }
    }
}
