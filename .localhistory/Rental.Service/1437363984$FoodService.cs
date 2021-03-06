﻿using Rental.Repository;
using Rental.Service.domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Service
{
    public class FoodService
    {
        private UnitOfWork _unitOfWork = null;
        public FoodService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<FoodModel> GetServicePageList(int pageIndex, int pageSize, out int pageCount)
        {
            int count = 0;
            var model = _unitOfWork.FoodRepository.Query().OrderBy(s => s.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            count = _unitOfWork.FoodRepository.Query().Count();
            pageCount = count;
            var serviceView = Rental.Service.convertor.FoodModelConvertor.ConvertToDomainList(model);
            return serviceView;
        }

        public bool Save(Rental.Service.domain.FoodModel service)
        {
            var model = Rental.Service.convertor.FoodModelConvertor.ConvertToEFModel(service);
            _unitOfWork.FoodRepository.Insert(model);
            return _unitOfWork.Commit() > 0;
        }

        public bool Delete(int[] ids)
        {
            try
            {
                int id = ids.First();
                var model = _unitOfWork.FoodRepository.Query().FirstOrDefault(s => s.Id == id);
                _unitOfWork.FoodRepository.Delete(model);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public Rental.Service.domain.FoodModel GetServiceById(int id)
        {
            var model = _unitOfWork.FoodRepository.Query().FirstOrDefault(s => s.Id == id);
            var viewModel = Rental.Service.convertor.FoodModelConvertor.ConvertToViewModel(model);
            return viewModel;
        }

        public bool Update(Rental.Service.domain.ServiceModel service)
        {
            var model = _unitOfWork.FoodRepository.Query().FirstOrDefault(s => s.Id == service.Id);
            if (model != null && service != null)
            {
                model.TitleCN = service.TitleCN;
                model.TitleTW = service.TitleTW;
                model.TtitleEN = service.TtitleEN;
                model.ContentEN = service.ContentEN;
                model.ContentCN = service.ContentCN;
                model.ContentTW = service.ContentTW;
            }
            return _unitOfWork.Commit() > 0;
        }

        //public List<ServiceCultureModel> GetServiceViewModel(string culture)
        //{
        //    string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
        //    List<ServiceCultureModel> viewModel = new List<ServiceCultureModel>();
        //    var model = _unitOfWork.ServiceRepository.Query().ToList();
        //    if (culture == "zh-CN")
        //    {
        //        viewModel = model.Select(s => new ServiceCultureModel
        //        {
        //            Content = s.ContentCN,
        //            Id = s.Id,
        //            Ttitle = s.TitleCN,
        //            ServiceImageInfo = s.ServiceImageInfo.Select(b => new ServiceImageInfoModel
        //            {
        //                ImgUrl = hostName + "Upload/ServiceImg/" + b.ImgUrl,
        //                ThumbImgUrl = hostName + "Upload/ServiceImg/thumb/" + b.ImgUrl
        //            }).ToList(),
        //        }).ToList();
        //    }
        //    else if (culture == "zh-TW")
        //    {
        //        viewModel = model.Select(s => new ServiceCultureModel
        //        {
        //            Content = s.ContentTW,
        //            Id = s.Id,
        //            Ttitle = s.TitleTW,
        //            ServiceImageInfo = s.ServiceImageInfo.Select(b => new ServiceImageInfoModel
        //            {
        //                ImgUrl = hostName + "Upload/ServiceImg/" + b.ImgUrl,
        //                ThumbImgUrl = hostName + "Upload/ServiceImg/thumb" + b.ImgUrl
        //            }).ToList(),
        //        }).ToList();
        //    }
        //    else
        //    {
        //        viewModel = model.Select(s => new ServiceCultureModel
        //        {
        //            Content = s.ContentEN,
        //            Id = s.Id,
        //            Ttitle = s.TtitleEN,
        //            ServiceImageInfo = s.ServiceImageInfo.Select(b => new ServiceImageInfoModel
        //            {
        //                ImgUrl = hostName + "Upload/ServiceImg/" + b.ImgUrl,
        //                ThumbImgUrl = hostName + "Upload/ServiceImg/thumb" + b.ImgUrl
        //            }).ToList(),
        //        }).ToList();
        //    }
        //    return viewModel;
        //}

        //public ServiceCultureModel GetServiceViewModelById(int id, string culture)
        //{
        //    string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
        //    var viewModel = new ServiceCultureModel();
        //    var model = _unitOfWork.ServiceRepository.Query().FirstOrDefault(s => s.Id == id);
        //    if (culture == "zh-CN")
        //    {
        //        viewModel.Id = model.Id;
        //        viewModel.Ttitle = model.TitleCN;
        //        viewModel.Content = model.ContentCN;
        //        viewModel.ServiceImageInfo = model.ServiceImageInfo.Select(b => new ServiceImageInfoModel
        //        {
        //            ImgUrl = hostName + "Upload/ServiceImg/" + b.ImgUrl,
        //            ThumbImgUrl = hostName + "Upload/ServiceImg/thumb/" + b.ImgUrl
        //        }).ToList();
        //    }
        //    return viewModel;
        //}
    }
}
