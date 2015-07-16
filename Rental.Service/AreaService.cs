using Rental.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rental.Service.domain;
namespace Rental.Service
{
    public class AreaService
    {
        private UnitOfWork _unitOfWork = null;
        public AreaService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<AreaModel> GetArea(int id)
        {
            var areaList = _unitOfWork.AreaRepository.Query().Where(s => s.AreaParent == id).ToList();
            var areaViewList = new List<AreaModel>();
            if (areaList != null && areaList.Count > 0)
            {
                areaViewList = areaList.Select(s => new AreaModel
                {
                    AreaLang = s.AreaLang,
                    AreaLong = s.AreaLong,
                    AreaName = s.AreaName,
                    AreaParent = s.AreaParent,
                    Id = s.Id
                }).ToList();
            }
            return areaViewList;
        }
    }
}
