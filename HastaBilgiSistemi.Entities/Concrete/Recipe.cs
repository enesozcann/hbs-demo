using HastaBilgiSistemi.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Entities.Concrete
{
    public class Recipe : IEntity
    {
        public int Id { get; set; }
        public int DiagnosticId { get; set; }
        public Diagnostic Diagnostic { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
