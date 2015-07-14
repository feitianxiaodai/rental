using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Rental.Service
{
    public class SliderModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage="*")]
        public string TitleCN { get; set; }

        public string TitleEN { get; set; }

        public string TitleTW { get; set; }

        [Required(ErrorMessage="*")]
        public string ImgUrl { get; set; }
    }
}
