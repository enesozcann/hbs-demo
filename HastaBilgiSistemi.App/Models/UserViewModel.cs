using HastaBilgiSistemi.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }

    }
}
