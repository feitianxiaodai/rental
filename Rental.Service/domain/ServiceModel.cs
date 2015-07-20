using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Service.domain
{
    public class ServiceModel
    {
        public ServiceModel()
        {
            this.ServiceImageInfo = new List<ServiceImageInfoModel>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string TitleCN { get; set; }
        [Required(ErrorMessage = "*")]
        public string TitleTW { get; set; }
        [Required(ErrorMessage = "*")]
        public string TtitleEN { get; set; }
        public string ContentCN { get; set; }
        public string ContentTW { get; set; }
        public string ContentEN { get; set; }
        public System.DateTime CreateTime { get; set; }

        public virtual List<ServiceImageInfoModel> ServiceImageInfo { get; set; }
    }

    public class ServiceImageInfoModel
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }

        public string ThumbImgUrl { get; set; }
        public string OriginUrl { get; set; }
    }
}
