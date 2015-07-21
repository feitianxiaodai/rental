using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rental.Service
{
    public class AboutService
    {
        private UnitOfWork _unitOfWork = null;
        public FoodService()
        {
            _unitOfWork = new UnitOfWork();
        }
    }
}
