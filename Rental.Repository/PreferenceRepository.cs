﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Rental.Model.Model;
namespace Rental.Repository
{
    public class PreferenceRepository : RepositoryBase<Preferential>
    {
        public PreferenceRepository(DbContext context)
            : base(context)
        {

        }
    }
}
