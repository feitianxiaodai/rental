using Rental.Repository;
using Rental.Service.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rental.Service
{
    public class ArrivalService
    {
        private UnitOfWork _unitOfWork = null;
        public ArrivalService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public Rental.Service.domain.ArrivalModel GetHotelAbout()
        {
            var hotel = _unitOfWork.ArrivalRepository.Query().FirstOrDefault(s => s.Id == 1);
            var viewModel = new ArrivalModel()
            {
                ContentCN = hotel.ContentCN,
                ContentEN = hotel.ContentEN,
                ContentTW = hotel.ContentTW,
                Id = hotel.Id
            };
            return viewModel;
        }

        public void Update(ArrivalModel model)
        {
            var dbModel = _unitOfWork.ArrivalRepository.Query().FirstOrDefault(s => s.Id == 1);
            dbModel.ContentTW = model.ContentTW;
            dbModel.ContentEN = model.ContentEN;
            dbModel.ContentCN = model.ContentCN;
            _unitOfWork.Commit();
        }

        public ArrivalCultureModel GetAboutViewModel(string culture)
        {
            ArrivalCultureModel model = new ArrivalCultureModel();
            var dbModel = _unitOfWork.ArrivalRepository.Query().FirstOrDefault(s => s.Id == 1);
            if (culture == "zh-CN")
            {
                model.Content = dbModel.ContentCN;
            }
            else if (culture == "zh-TW")
            {
                model.Content = dbModel.ContentTW;
            }
            else
            {
                model.Content = dbModel.ContentEN;
            }
            return model;
        }

        

    }
}
