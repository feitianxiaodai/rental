using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rental.Model.Model;
using System.Data.Entity;
namespace Rental.Repository
{
    public class ArrivalRepository : RepositoryBase<Arrival>
    {
        public ArrivalRepository(DbContext context)
            : base(context)
        {

        }
    }
}
