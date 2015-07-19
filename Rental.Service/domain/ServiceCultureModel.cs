using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Service.domain
{
    public class ServiceCultureModel
    {
        public ServiceCultureModel()
        {
            this.ServiceImageInfo = new List<ServiceImageInfoModel>();
        }

        public int Id { get; set; }
        
        public string Ttitle { get; set; }
        public string Content { get; set; }
        public virtual List<ServiceImageInfoModel> ServiceImageInfo { get; set; }
    }

    
}
