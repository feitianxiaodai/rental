using Rental.Repository;
using Rental.Service.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rental.Service
{
    public class AboutService
    {
        private UnitOfWork _unitOfWork = null;
        public AboutService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public Rental.Service.domain.About GetHotelAbout()
        {
            var hotel = _unitOfWork.AboutRepository.Query().FirstOrDefault(s => s.Id == 1);
            var viewModel = new About();
        }
    }
}
