using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rental.Repository
{
    class AboutRepository: RepositoryBase<Food>
    {
        public FoodRepository(DbContext context)
            : base(context)
        {

        }
    }
}
