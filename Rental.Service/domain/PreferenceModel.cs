using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rental.Service.domain
{
    public class PreferenceModel
    {
        public PreferenceModel()
        {
            this.PreferenImageInfo = new List<PreferenceImageInfo>();
        }

        public int Id { get; set; }
        public string TitleCN { get; set; }
        public string TtitleTW { get; set; }
        public string TtitleEN { get; set; }
        public string ContentCN { get; set; }
        public string ContentTW { get; set; }
        public string ContentEN { get; set; }

        public List<PreferenceImageInfo> PreferenImageInfo { get; set; }
    }

    public class PreferenceImageInfo
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }

        public string ThumbImgUrl { get; set; }
        public string OriginUrl { get; set; }
    }
}
