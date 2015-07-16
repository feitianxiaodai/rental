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
            return _unitOfWork.SliderRepository.Query().OrderBy(s => s.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public bool Delete(int[] ids)
        {
            try
            {
                int id = ids.First();
                var model = _unitOfWork.SliderRepository.Query().FirstOrDefault(s => s.Id == id);
                _unitOfWork.SliderRepository.Delete(model);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public bool Add(SliderModel model)
        {
            Model.Model.Slider dbModel = new Model.Model.Slider();
            dbModel.CreateTime = DateTime.Now;
            dbModel.ImgUrl = model.ImgUrl;
            dbModel.TitleCN = model.TitleCN;
            dbModel.TitleEN = model.TitleEN;
            dbModel.TitleTW = model.TitleTW;
            _unitOfWork.SliderRepository.Insert(dbModel);
            return _unitOfWork.Commit() > 0;
        }

        public List<SliderModel> GetHotelAlbumList (string title)
        {
            var hotelList = new SliderModel();
        }

    }
}
