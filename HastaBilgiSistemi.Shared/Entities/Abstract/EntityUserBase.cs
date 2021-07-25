using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Shared.Entities.Abstract
{
    public abstract class EntityUserBase : EntityBase
    {
        public virtual string UserName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual byte[] Password { get; set; }
        public virtual string Picture { get; set; }
    }
}
