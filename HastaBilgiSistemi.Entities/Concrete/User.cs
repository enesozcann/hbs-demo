using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HastaBilgiSistemi.Shared.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace HastaBilgiSistemi.Entities.Concrete
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }

    }
}
