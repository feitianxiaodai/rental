using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rental.Model.Model;
using System.Data.Entity;
namespace Rental.Repository
{
    public class FoodRepository: RepositoryBase<Food>
    {
        public FoodRepository(DbContext context)
            : base(context)
        {

        }
    }
}
