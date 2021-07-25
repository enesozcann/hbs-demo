using HastaBilgiSistemi.Entities.Concrete;
using HastaBilgiSistemi.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Data.Abstract
{
    public interface IDoctorRepository:IEntityRepository<Doctor>
    {
    }
}
