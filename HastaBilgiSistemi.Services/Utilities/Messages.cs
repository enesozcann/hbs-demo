using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaBilgiSistemi.Services.Utilities
{
    public static class Messages
    {
        public static class Base
        {
            public static string UnExpectedError()
            {
                return "Beklenmeyen bir hata ile karşılaşıldı.";
            }
        }
        public static class Appointment
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir randevu bulunamadı.";
                return "Belirtilen randevu bulunamadı.";
            }
            public static string NotFoundWithPredicate(string predicate)
            {
                return $"Belirtilen {predicate} ait randevu bulunamadı.";
            }
            public static string Add(DateTime date)
            {
                return $"{date} tarihli randevu oluşturulmuştur.";
            }
            public static string Update(DateTime date)
            {
                return $"{date} tarihli randevu güncellenmiştir.";
            }
            public static string Delete(DateTime date)
            {
                return $"{date} tarihli randevu iptal edilmiştir.";
            }
            public static string HardDelete(DateTime date)
            {
                return $"{date} tarihli randevu veritabanından silinmiştir.";
            }
        }
        public static class Polyclinic
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir poliklinik bulunamadı.";
                return "Belirtilen poliklinik bulunamadı.";
            }
            public static string NotFoundWithPredicate(string predicate)
            {
                return $"Belirtilen {predicate} ait poliklinik bulunamadı.";
            }
            public static string Add(string name)
            {
                return $"{name} adlı poliklinik oluşturulmuştur.";
            }
            public static string Update(string name)
            {
                return $"{name} adlı poliklinik güncellenmiştir.";
            }
            public static string Delete(string name)
            {
                return $"{name} adlı poliklinik silinmiştir.";
            }
            public static string HardDelete(string name)
            {
                return $"{name} adlı poliklinik veritabanından silinmiştir.";
            }
        }
        public static class Hospital
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir hastane bulunamadı.";
                return "Belirtilen hastane bulunamadı.";
            }
            public static string NotFoundWithPredicate(string predicate)
            {
                return $"Belirtilen {predicate} ait hastane bulunamadı.";
            }
            public static string Add(string name)
            {
                return $"{name} adlı hastane oluşturulmuştur.";
            }
            public static string Update(string name)
            {
                return $"{name} adlı hastane güncellenmiştir.";
            }
            public static string Delete(string name)
            {
                return $"{name} adlı hastane silinmiştir.";
            }
            public static string HardDelete(string name)
            {
                return $"{name} adlı hastane veritabanından silinmiştir.";
            }
        }
        public static class Patient
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir hasta bulunamadı.";
                return "Belirtilen hasta bulunamadı.";
            }
            public static string NotFoundWithPredicate(string predicate)
            {
                return $"Belirtilen {predicate} ait hasta bulunamadı.";
            }
            public static string Add(string name)
            {
                return $"{name} adlı hasta oluşturulmuştur.";
            }
            public static string Update(string name)
            {
                return $"{name} adlı hasta güncellenmiştir.";
            }
            public static string Delete(string name)
            {
                return $"{name} adlı hasta silinmiştir.";
            }
            public static string HardDelete(string name)
            {
                return $"{name} adlı hasta veritabanından silinmiştir.";
            }
        }
        public static class Doctor
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir doktor bulunamadı.";
                return "Belirtilen doktor bulunamadı.";
            }
            public static string NotFoundWithPredicate(string predicate)
            {
                return $"Belirtilen {predicate} ait doktor bulunamadı.";
            }
            public static string Add(string name)
            {
                return $"{name} adlı doktor oluşturulmuştur.";
            }
            public static string Update(string name)
            {
                return $"{name} adlı doktor güncellenmiştir.";
            }
            public static string Delete(string name)
            {
                return $"{name} adlı doktor silinmiştir.";
            }
            public static string HardDelete(string name)
            {
                return $"{name} adlı doktor veritabanından silinmiştir.";
            }
        }
        public static class Diagnostic
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir doktor bulunamadı.";
                return "Belirtilen doktor bulunamadı.";
            }
            public static string NotFoundWithPredicate(string predicate)
            {
                return $"Belirtilen {predicate} ait doktor bulunamadı.";
            }
            public static string Add(string name)
            {
                return $"{name} adlı doktor oluşturulmuştur.";
            }
            public static string Update(string name)
            {
                return $"{name} adlı doktor güncellenmiştir.";
            }
            public static string Delete(string name)
            {
                return $"{name} adlı doktor silinmiştir.";
            }
            public static string HardDelete(string name)
            {
                return $"{name} adlı doktor veritabanından silinmiştir.";
            }
        }
        public static class Recipe
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir doktor bulunamadı.";
                return "Belirtilen doktor bulunamadı.";
            }
            public static string NotFoundWithPredicate(string predicate)
            {
                return $"Belirtilen {predicate} ait doktor bulunamadı.";
            }
            public static string Add(string name)
            {
                return $"{name} adlı doktor oluşturulmuştur.";
            }
            public static string Update(string name)
            {
                return $"{name} adlı doktor güncellenmiştir.";
            }
            public static string Delete(string name)
            {
                return $"{name} adlı doktor silinmiştir.";
            }
            public static string HardDelete(string name)
            {
                return $"{name} adlı doktor veritabanından silinmiştir.";
            }
        }
        public static class Medicine
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir doktor bulunamadı.";
                return "Belirtilen doktor bulunamadı.";
            }
            public static string NotFoundWithPredicate(string predicate)
            {
                return $"Belirtilen {predicate} ait doktor bulunamadı.";
            }
            public static string Add(string name)
            {
                return $"{name} adlı doktor oluşturulmuştur.";
            }
            public static string Update(string name)
            {
                return $"{name} adlı doktor güncellenmiştir.";
            }
            public static string Delete(string name)
            {
                return $"{name} adlı doktor silinmiştir.";
            }
            public static string HardDelete(string name)
            {
                return $"{name} adlı doktor veritabanından silinmiştir.";
            }
        }
    }
}
