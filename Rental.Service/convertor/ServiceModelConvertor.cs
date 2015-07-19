using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rental.Service.domain;
using System.Configuration;
using Rental.Model.Model;
namespace Rental.Service.convertor
{
    public static class ServiceModelConvertor
    {
        public static List<ServiceModel> ConvertToDomainList(List<Rental.Model.Model.Service> service)
        {
            string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
            List<ServiceModel> serviceModel = new List<ServiceModel>();
            if (service != null)
            {
                serviceModel = service.Select(s => new ServiceModel
                {
                    Id = s.Id,
                    ContentCN = s.ContentCN,
                    ContentEN = s.ContentEN,
                    ContentTW = s.ContentTW,
                    TitleCN = s.TitleCN,
                    TitleTW = s.TitleTW,
                    TtitleEN = s.TtitleEN,
                    ServiceImageInfo = s.ServiceImageInfo.Select(b => new ServiceImageInfoModel
                    {
                        Id = b.Id,
                        ImgUrl = hostName + "Upload/ServiceImg/" + b.ImgUrl,

                    }).ToList(),
                }).ToList();
            }
            return serviceModel;
        }

        public static Model.Model.Service ConvertToEFModel(Rental.Service.domain.ServiceModel service)
        {
            var model = new Model.Model.Service();
            if(service!=null)
            {
                model.Id=service.Id;
                model.TitleTW=service.TitleTW;
                model.TitleCN = service.TitleCN;
                model.TtitleEN=service.TtitleEN;
                model.ContentTW =service.ContentTW;
                model.ContentEN = service.ContentEN;
                model.ContentCN = service.ContentCN;
                if(service.ServiceImageInfo!=null && service.ServiceImageInfo.Count>0)
                {
                    model.ServiceImageInfo = service.ServiceImageInfo.Select(s=>new Model.Model.ServiceImageInfo{
                        ImgUrl= s.ImgUrl
                    }).ToList();
                }
            }
            return model;
        }

        public static Service.domain.ServiceModel ConvertToViewModel(Model.Model.Service service)
        {
            var model = new Service.domain.ServiceModel();
            if (service != null)
            {
                model.Id = service.Id;
                model.TitleTW = service.TitleTW;
                model.TitleCN = service.TitleCN;
                model.TtitleEN = service.TtitleEN;
                model.ContentTW = service.ContentTW;
                model.ContentEN = service.ContentEN;
                model.ContentCN = service.ContentCN;
                if (service.ServiceImageInfo != null && service.ServiceImageInfo.Count > 0)
                {
                    model.ServiceImageInfo = service.ServiceImageInfo.Select(s => new Service.domain.ServiceImageInfoModel
                    {
                        ImgUrl = s.ImgUrl
                    }).ToList();
                }
            }
            return model;
        }
    }
}
