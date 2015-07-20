using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Service.domain
{
    public class FoodCultureModel
    {
        public FoodCultureModel()
        {
            this.ServiceImageInfo = new List<FoodImageInfoModel>();
        }

        public int Id { get; set; }

        public string Ttitle { get; set; }
        public string Content { get; set; }
        public List<FoodImageInfoModel> ServiceImageInfo { get; set; }
    }
}
