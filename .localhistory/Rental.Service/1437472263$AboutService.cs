using Rental.Repository;
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

        }
    }
}
