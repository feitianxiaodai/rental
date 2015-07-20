using Rental.Repository;
using System;
using System.Collections.Generic;
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
    }
}
