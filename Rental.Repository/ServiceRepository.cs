using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rental.Model.Model;
namespace Rental.Repository
{
    public class ServiceRepository: RepositoryBase<Service>
    {
        public ServiceRepository(DbContext context)
            : base(context)
        {

        }
    }
    
}
