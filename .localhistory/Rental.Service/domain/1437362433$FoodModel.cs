using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Service.domain
{
    public class FoodModel
    {
        public Food()
        {
            this.FoodImageInfo = new HashSet<FoodImageInfo>();
        }
    
        public int Id { get; set; }
        public string TitleCN { get; set; }
        public string TitleTW { get; set; }
        public string TtitleEN { get; set; }
        public string ContentCN { get; set; }
        public string ContentTW { get; set; }
        public string ContentEN { get; set; }
    
        public virtual ICollection<FoodImageInfo> FoodImageInfo { get; set; }
    }
}
