using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rental.Model.Model;
using System.Data.Entity.Validation;
namespace Rental.Repository
{
    public class UnitOfWork
    {

        private RentalEntities _edm = null;
        private SliderRepository _sliderRepository = null;
        public UnitOfWork()
        {
            _edm = new RentalEntities();
        }

        public SliderRepository SliderRepository
        {
            get
            {
                if (_sliderRepository == null)
                {
                    _sliderRepository = new SliderRepository(_edm);
                }

                return _sliderRepository;
            }
        }


        public int Commit()
        {
            try
            {
                return _edm.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                return 0;
            }
        }
    }

}
