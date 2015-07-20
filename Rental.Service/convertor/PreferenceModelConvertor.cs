using Rental.Service.domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Service.convertor
{
    public static class PreferenceModelConvertor
    {
        public static List<PreferenceModel> ConvertToDomainList(List<Rental.Model.Model.Preferential> preference)
        {
            string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
            List<PreferenceModel> preferenceModel = new List<PreferenceModel>();
            if (preference != null)
            {
                preferenceModel = preference.Select(s => new PreferenceModel
                {
                    Id = s.Id,
                    ContentCN = s.ContentCN,
                    ContentEN = s.ContentEN,
                    ContentTW = s.ContentTW,
                    TitleCN = s.TitleCN,
                    TtitleTW = s.TtitleTW,
                    TtitleEN = s.TtitleEN,

                    PreferenImageInfo = s.PreferenImageInfo.Select(b => new PreferenceImageInfo
                    {
                        Id = b.Id,
                        ImgUrl = hostName + "Upload/PreferenceImg/" + b.ImgUrl,

                    }).ToList(),
                }).ToList();
            }
            return preferenceModel;
        }

        public static Model.Model.Preferential ConvertToEFModel(Rental.Service.domain.PreferenceModel service)
        {
            var model = new Model.Model.Preferential();
            if (service != null)
            {
                model.Id = service.Id;
                model.TtitleTW = service.TtitleTW;
                model.TitleCN = service.TitleCN;
                model.TtitleEN = service.TtitleEN;
                model.ContentTW = service.ContentTW;
                model.ContentEN = service.ContentEN;
                model.ContentCN = service.ContentCN;
                if (service.PreferenImageInfo != null && service.PreferenImageInfo.Count > 0)
                {
                    model.PreferenImageInfo = service.PreferenImageInfo.Select(s => new Model.Model.PreferenImageInfo
                    {
                        ImgUrl = s.ImgUrl
                    }).ToList();
                }
            }
            return model;
        }

        public static Service.domain.PreferenceModel ConvertToViewModel(Model.Model.Preferential service)
        {
            var model = new Service.domain.PreferenceModel();
            if (service != null)
            {
                model.Id = service.Id;
                model.TtitleTW = service.TtitleTW;
                model.TitleCN = service.TitleCN;
                model.TtitleEN = service.TtitleEN;
                model.ContentTW = service.ContentTW;
                model.ContentEN = service.ContentEN;
                model.ContentCN = service.ContentCN;
                if (service.PreferenImageInfo != null && service.PreferenImageInfo.Count > 0)
                {
                    model.PreferenImageInfo = service.PreferenImageInfo.Select(s => new Service.domain.PreferenceImageInfo
                    {
                        ImgUrl = s.ImgUrl
                    }).ToList();
                }
            }
            return model;
        }
    }
}
