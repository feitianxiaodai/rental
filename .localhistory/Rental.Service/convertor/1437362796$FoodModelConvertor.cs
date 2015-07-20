using Rental.Service.domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Service.convertor
{
    public static class FoodModelConvertor
    {
        public static List<FoodModel> ConvertToDomainList(List<Rental.Model.Model.Food> food)
        {
            string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
            List<FoodModel> foodModel = new List<FoodModel>();
            if (food != null)
            {
                foodModel = food.Select(s => new FoodModel
                {
                    Id = s.Id,
                    ContentCN = s.ContentCN,
                    ContentEN = s.ContentEN,
                    ContentTW = s.ContentTW,
                    TitleCN = s.TitleCN,
                    TitleTW = s.TitleTW,
                    TtitleEN = s.TtitleEN,

                    FoodImageInfo = s.FoodImageInfo.Select(b => new FoodImageInfoModel
                    {
                        Id = b.Id,
                        ImgUrl = hostName + "Upload/ServiceImg/" + b.ImgUrl,

                    }).ToList(),
                }).ToList();
            }
            return foodModel;
        }

        public static Model.Model.Food ConvertToEFModel(Rental.Service.domain.FoodModel service)
        {
            var model = new Model.Model.Food();
            if (service != null)
            {
                model.Id = service.Id;
                model.TitleTW = service.TitleTW;
                model.TitleCN = service.TitleCN;
                model.TtitleEN = service.TtitleEN;
                model.ContentTW = service.ContentTW;
                model.ContentEN = service.ContentEN;
                model.ContentCN = service.ContentCN;
                if (service.FoodImageInfo != null && service.FoodImageInfo.Count > 0)
                {
                    model.FoodImageInfo = service.FoodImageInfo.Select(s => new Model.Model.FoodImageInfo
                    {
                        ImgUrl = s.ImgUrl
                    }).ToList();
                }
            }
            return model;
        }

        public static Service.domain.FoodModel ConvertToViewModel(Model.Model.Food service)
        {
            var model = new Service.domain.FoodModel();
            if (service != null)
            {
                model.Id = service.Id;
                model.TitleTW = service.TitleTW;
                model.TitleCN = service.TitleCN;
                model.TtitleEN = service.TtitleEN;
                model.ContentTW = service.ContentTW;
                model.ContentEN = service.ContentEN;
                model.ContentCN = service.ContentCN;
                if (service.FoodImageInfo != null && service.FoodImageInfo.Count > 0)
                {
                    model.FoodImageInfo = service.FoodImageInfo.Select(s => new Service.domain.FoodImageInfo
                    {
                        ImgUrl = s.ImgUrl
                    }).ToList();
                }
            }
            return model;
        }
    }
}
