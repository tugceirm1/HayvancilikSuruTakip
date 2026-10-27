
using HayvancilikSuruTakip.Security;
using HayvancilikSuruTakip.Security.HayvancilikSuruTakip.Security;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HayvancilikSuruTakip.Data
{
    public class KullaniciRepository
    {
        public void KayitOl(string kullaniciAdi, string parola)
        {
            if (string.IsNullOrWhiteSpace(kullaniciAdi))
                throw new Exception("Kullanıcı adı boş olamaz.");

            kullaniciAdi = kullaniciAdi.Trim();

            if (kullaniciAdi.Length < 3 || kullaniciAdi.Length > 50)
                throw new Exception("Kullanıcı adı 3-50 karakter olmalıdır.");

            if (string.IsNullOrEmpty(parola) || parola.Length < 12)
                throw new Exception("Parola en az 12 karakter olmalıdır.");

            byte[] hash;
            byte[] salt;

            PasswordHelper.HashPassword(parola, out hash, out salt);

            using (SqlConnection baglanti = DatabaseConnection.GetConnection())
            {
                string sql = @"
                    INSERT INTO dbo.Kullanicilar
                    (KullaniciAdi, ParolaHash, ParolaSalt, ParolaIterasyon, Rol)
                    VALUES
                    (@ad, @hash, @salt, @iterasyon, 'Ciftci')";

                using (SqlCommand komut = new SqlCommand(sql, baglanti))
                {
                    komut.Parameters.Add("@ad", SqlDbType.NVarChar, 50).Value = kullaniciAdi;
                    komut.Parameters.Add("@hash", SqlDbType.VarBinary, 32).Value = hash;
                    komut.Parameters.Add("@salt", SqlDbType.VarBinary, 16).Value = salt;
                    komut.Parameters.Add("@iterasyon", SqlDbType.Int).Value =
                        PasswordHelper.Iterations;

                    baglanti.Open();
                    komut.ExecuteNonQuery();
                }
            }
        }

        public bool GirisYap(string kullaniciAdi, string parola)
        {
            Oturum.Kapat();

            if (string.IsNullOrWhiteSpace(kullaniciAdi) ||
                string.IsNullOrEmpty(parola))
                return false;

            using (SqlConnection baglanti = DatabaseConnection.GetConnection())
            {
                baglanti.Open();

                int id;
                string ad;
                string rol;
                byte[] hash;
                byte[] salt;
                int iterasyon;
                DateTime? kilitBitis;

                using (SqlCommand komut = new SqlCommand(@"
                    SELECT KullaniciID, KullaniciAdi, Rol,
                           ParolaHash, ParolaSalt, ParolaIterasyon,
                           KilitBitis
                    FROM dbo.Kullanicilar
                    WHERE KullaniciAdi = @ad", baglanti))
                {
                    komut.Parameters.Add("@ad", SqlDbType.NVarChar, 50).Value =
                        kullaniciAdi.Trim();

                    using (SqlDataReader reader = komut.ExecuteReader())
                    {
                        if (!reader.Read())
                            return false;

                        id = Convert.ToInt32(reader["KullaniciID"]);
                        ad = Convert.ToString(reader["KullaniciAdi"]);
                        rol = Convert.ToString(reader["Rol"]);
                        hash = (byte[])reader["ParolaHash"];
                        salt = (byte[])reader["ParolaSalt"];
                        iterasyon = Convert.ToInt32(reader["ParolaIterasyon"]);

                        kilitBitis = reader["KilitBitis"] == DBNull.Value
                            ? (DateTime?)null
                            : Convert.ToDateTime(reader["KilitBitis"]);
                    }
                }

                if (kilitBitis.HasValue && kilitBitis.Value > DateTime.Now)
                    return false;

                bool dogru = PasswordHelper.VerifyPassword(
                    parola, hash, salt, iterasyon);

                if (!dogru)
                {
                    using (SqlCommand komut = new SqlCommand(@"
                        UPDATE dbo.Kullanicilar
                        SET HataliGirisSayisi =
                            CASE
                                WHEN KilitBitis IS NOT NULL
                                     AND KilitBitis <= SYSDATETIME()
                                THEN 1
                                ELSE HataliGirisSayisi + 1
                            END,
                            KilitBitis =
                            CASE
                                WHEN
                                    (CASE
                                        WHEN KilitBitis IS NOT NULL
                                             AND KilitBitis <= SYSDATETIME()
                                        THEN 1
                                        ELSE HataliGirisSayisi + 1
                                    END) >= 5
                                THEN DATEADD(MINUTE, 15, SYSDATETIME())
                                ELSE NULL
                            END
                        WHERE KullaniciID = @id", baglanti))
                    {
                        komut.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        komut.ExecuteNonQuery();
                    }

                    return false;
                }

                using (SqlCommand komut = new SqlCommand(@"
                    UPDATE dbo.Kullanicilar
                    SET HataliGirisSayisi = 0,
                        KilitBitis = NULL
                    WHERE KullaniciID = @id", baglanti))
                {
                    komut.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    komut.ExecuteNonQuery();
                }

                Oturum.Baslat(id, ad, rol);
                return true;
            }
        }

        public void HesapBilgileriniDegistir(
            int kullaniciID,
            string eskiParola,
            string yeniKullaniciAdi,
            string yeniParola)
        {
            if (kullaniciID <= 0)
                throw new Exception("Önce giriş yapmalısınız.");

            if (string.IsNullOrWhiteSpace(yeniKullaniciAdi))
                throw new Exception("Kullanıcı adı boş olamaz.");

            yeniKullaniciAdi = yeniKullaniciAdi.Trim();

            if (yeniKullaniciAdi.Length < 3 || yeniKullaniciAdi.Length > 50)
                throw new Exception("Kullanıcı adı 3-50 karakter olmalıdır.");

            if (string.IsNullOrEmpty(yeniParola) || yeniParola.Length < 12)
                throw new Exception("Yeni parola en az 12 karakter olmalıdır.");

            using (SqlConnection baglanti = DatabaseConnection.GetConnection())
            {
                baglanti.Open();

                byte[] eskiHash;
                byte[] eskiSalt;
                int eskiIterasyon;

                using (SqlCommand komut = new SqlCommand(@"
            SELECT ParolaHash, ParolaSalt, ParolaIterasyon
            FROM dbo.Kullanicilar
            WHERE KullaniciID = @id", baglanti))
                {
                    komut.Parameters.Add("@id", SqlDbType.Int).Value = kullaniciID;

                    using (SqlDataReader reader = komut.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new Exception("Kullanıcı bulunamadı.");

                        eskiHash = (byte[])reader["ParolaHash"];
                        eskiSalt = (byte[])reader["ParolaSalt"];
                        eskiIterasyon = Convert.ToInt32(reader["ParolaIterasyon"]);
                    }
                }

                bool eskiParolaDogru = PasswordHelper.VerifyPassword(
                    eskiParola, eskiHash, eskiSalt, eskiIterasyon);

                if (!eskiParolaDogru)
                    throw new Exception("Mevcut şifreniz yanlış.");

                byte[] yeniHash;
                byte[] yeniSalt;

                PasswordHelper.HashPassword(
                    yeniParola, out yeniHash, out yeniSalt);

                using (SqlCommand komut = new SqlCommand(@"
            UPDATE dbo.Kullanicilar
            SET KullaniciAdi = @ad,
                ParolaHash = @hash,
                ParolaSalt = @salt,
                ParolaIterasyon = @iterasyon
            WHERE KullaniciID = @id", baglanti))
                {
                    komut.Parameters.Add("@id", SqlDbType.Int).Value = kullaniciID;
                    komut.Parameters.Add("@ad", SqlDbType.NVarChar, 50).Value = yeniKullaniciAdi;
                    komut.Parameters.Add("@hash", SqlDbType.VarBinary, 32).Value = yeniHash;
                    komut.Parameters.Add("@salt", SqlDbType.VarBinary, 16).Value = yeniSalt;
                    komut.Parameters.Add("@iterasyon", SqlDbType.Int).Value =
                        PasswordHelper.Iterations;

                    komut.ExecuteNonQuery();
                }
            }
        }

    }
}
