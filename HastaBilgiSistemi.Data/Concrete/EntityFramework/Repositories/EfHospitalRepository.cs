using HastaBilgiSistemi.Data.Abstract;
using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Data.Concrete.EntityFramework.Repositories
{
    public class EfHospitalRepository : EfEntityRepositoryBase<Hospital>, IHospitalRepository
    {
        public EfHospitalRepository(DbContext context) : base(context)
        {
        }
    }
}
