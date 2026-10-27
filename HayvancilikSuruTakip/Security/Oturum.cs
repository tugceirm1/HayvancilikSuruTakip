using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HayvancilikSuruTakip.Security
{
  


namespace HayvancilikSuruTakip.Security
    {
        public static class Oturum
        {
            public static int KullaniciID { get; private set; }

            public static string KullaniciAdi { get; private set; }

            public static string Rol { get; private set; }

            public static bool GirisYapildi
            {
                get { return KullaniciID > 0; }
            }

            public static bool CiftciMi
            {
                get { return GirisYapildi && Rol == "Ciftci"; }
            }

            public static void Baslat(
                int id, string kullaniciAdi, string rol)
            {
                KullaniciID = id;
                KullaniciAdi = kullaniciAdi;
                Rol = rol;
            }

            public static void Kapat()
            {
                KullaniciID = 0;
                KullaniciAdi = null;
                Rol = null;
            }

            public static void CiftciYetkisiGerekli()
            {
                if (!CiftciMi)
                    throw new System.UnauthorizedAccessException(
                        "Bu işlem için Çiftçi yetkisi gerekiyor.");
            }
        }
    }

}

