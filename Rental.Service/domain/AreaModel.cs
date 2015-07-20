using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Service.domain
{
    public class AreaModel
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public int AreaParent { get; set; }
        public string AreaLong { get; set; }
        public string AreaLang { get; set; }
    }
}
