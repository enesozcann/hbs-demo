using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.App.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int PolyclinicsCount { get; set; }
        public int AppointmentsCount { get; set; }
        public int UsersCount { get; set; }
    }
}
