using Rental.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rental.Service.domain;
using System.Configuration;
namespace Rental.Service
{
    public class PreferenceService
    {
          private UnitOfWork _unitOfWork = null;
          public PreferenceService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<PreferenceModel> GetServicePageList(int pageIndex, int pageSize, out int pageCount)
        {
            int count = 0;
            var model = _unitOfWork.PreferenceRepository.Query().OrderBy(s => s.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            count = _unitOfWork.PreferenceRepository.Query().Count();
            pageCount = count;
            var serviceView = Rental.Service.convertor.PreferenceModelConvertor.ConvertToDomainList(model);
            return serviceView;
        }

        public bool Save(Rental.Service.domain.PreferenceModel service)
        {
            var model = Rental.Service.convertor.PreferenceModelConvertor.ConvertToEFModel(service);
            _unitOfWork.PreferenceRepository.Insert(model);
            return _unitOfWork.Commit() > 0;
        }

        public bool Delete(int[] ids)
        {
            try
            {
                int id = ids.First();
                var model = _unitOfWork.PreferenceRepository.Query().FirstOrDefault(s => s.Id == id);
                _unitOfWork.PreferenceRepository.Delete(model);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public Rental.Service.domain.PreferenceModel GetServiceById(int id)
        {
            var model = _unitOfWork.PreferenceRepository.Query().FirstOrDefault(s => s.Id == id);
            var viewModel = Rental.Service.convertor.PreferenceModelConvertor.ConvertToViewModel(model);
            return viewModel;
        }

        public bool Update(Rental.Service.domain.PreferenceModel service)
        {
            var model = _unitOfWork.PreferenceRepository.Query().FirstOrDefault(s => s.Id == service.Id);
            if (model != null && service != null)
            {
                model.TitleCN = service.TitleCN;
                model.TtitleTW = service.TtitleTW;
                model.TtitleEN = service.TtitleEN;
                model.ContentEN = service.ContentEN;
                model.ContentCN = service.ContentCN;
                model.ContentTW = service.ContentTW;
            }
            return _unitOfWork.Commit() > 0;
        }

        public List<PreferenceCultureModel> GetServiceViewModel(string culture)
        {
            string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
            List<PreferenceCultureModel> viewModel = new List<PreferenceCultureModel>();
            var model = _unitOfWork.PreferenceRepository.Query().ToList();
            if (culture == "zh-CN")
            {
                viewModel = model.Select(s => new PreferenceCultureModel
                {
                    Content = s.ContentCN,
                    Id = s.Id,
                    Ttitle = s.TitleCN,
                    ServiceImageInfo = s.PreferenImageInfo.Select(b => new PreferenceImageInfo
                    {
                        ImgUrl = hostName + "Upload/PreferenceImg/" + b.ImgUrl,
                        ThumbImgUrl = hostName + "Upload/PreferenceImg/thumb/" + b.ImgUrl
                    }).ToList(),
                }).ToList();
            }
            else if (culture == "zh-TW")
            {
                viewModel = model.Select(s => new PreferenceCultureModel
                {
                    Content = s.ContentTW,
                    Id = s.Id,
                    Ttitle = s.TtitleTW,
                    ServiceImageInfo = s.PreferenImageInfo.Select(b => new PreferenceImageInfo
                    {
                        ImgUrl = hostName + "Upload/PreferenceImg/" + b.ImgUrl,
                        ThumbImgUrl = hostName + "Upload/PreferenceImg/thumb" + b.ImgUrl
                    }).ToList(),
                }).ToList();
            }
            else
            {
                viewModel = model.Select(s => new PreferenceCultureModel
                {
                    Content = s.ContentEN,
                    Id = s.Id,
                    Ttitle = s.TtitleEN,
                    ServiceImageInfo = s.PreferenImageInfo.Select(b => new PreferenceImageInfo
                    {
                        ImgUrl = hostName + "Upload/PreferenceImg/" + b.ImgUrl,
                        ThumbImgUrl = hostName + "Upload/PreferenceImg/thumb" + b.ImgUrl
                    }).ToList(),
                }).ToList();
            }
            return viewModel;
        }

        public PreferenceCultureModel GetServiceViewModelById(int id, string culture)
        {
            string hostName = ConfigurationSettings.AppSettings["HOSTNAME"].ToString();
            var viewModel = new PreferenceCultureModel();
            var model = _unitOfWork.PreferenceRepository.Query().FirstOrDefault(s => s.Id == id);
            if (culture == "zh-CN")
            {
                viewModel.Id = model.Id;
                viewModel.Ttitle = model.TitleCN;
                viewModel.Content = model.ContentCN;
                viewModel.ServiceImageInfo = model.PreferenImageInfo.Select(b => new PreferenceImageInfo
                    {
                        ImgUrl = hostName + "Upload/PreferenceImg/" + b.ImgUrl,
                        ThumbImgUrl = hostName + "Upload/PreferenceImg/thumb/" + b.ImgUrl
                    }).ToList();
            }
            else if (culture == "zh-TW")
            {
                viewModel.Id = model.Id;
                viewModel.Ttitle = model.TtitleTW;
                viewModel.Content = model.ContentTW;
                viewModel.ServiceImageInfo = model.PreferenImageInfo.Select(b => new PreferenceImageInfo
                {
                    ImgUrl = hostName + "Upload/PreferenceImg/" + b.ImgUrl,
                    ThumbImgUrl = hostName + "Upload/PreferenceImg/thumb/" + b.ImgUrl
                }).ToList();
            }
            else
            {
                viewModel.Id = model.Id;
                viewModel.Ttitle = model.TtitleEN;
                viewModel.Content = model.ContentEN;
                viewModel.ServiceImageInfo = model.PreferenImageInfo.Select(b => new PreferenceImageInfo
                {
                    ImgUrl = hostName + "Upload/PreferenceImg/" + b.ImgUrl,
                    ThumbImgUrl = hostName + "Upload/PreferenceImg/thumb/" + b.ImgUrl
                }).ToList();
            }
            return viewModel;
        }
    }
}
