using Rental.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Service
{
    public class SliderService
    {
        private UnitOfWork _unitOfWork = null;
        public SliderService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<Rental.Model.Model.Slider> GetSliderPageList(int pageIndex, int pageSize, out int totalCount)
        {
            int count = 0;
            count = _unitOfWork.SliderRepository.Query().Where(s => true).Count();
            totalCount = count;
            return _unitOfWork.SliderRepository.Query().OrderBy(s => s.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

    }
}
