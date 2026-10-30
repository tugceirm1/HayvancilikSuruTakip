
using HayvancilikSuruTakip.Security;
using HayvancilikSuruTakip.Security.HayvancilikSuruTakip.Security;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HayvancilikSuruTakip.Data
{
    public class HayvanRepository
    {
        // READ - Hayvanlari listele
        public DataTable HayvanlariListele()
        {
            DataTable tablo = new DataTable();

            using (SqlConnection baglanti =
                DatabaseConnection.GetConnection())
            {
                string sql =
                    @"SELECT HayvanID, KupeNo, HayvanAdi,
                             Tur, Cinsiyet
                      FROM dbo.Hayvanlar
                      ORDER BY HayvanID";

                using (SqlDataAdapter adapter =
                    new SqlDataAdapter(sql, baglanti))
                {
                    adapter.Fill(tablo);
                }
            }

            return tablo;
        }

        // CREATE - Yeni hayvan ekle
        public void HayvanEkle(
            string kupeNo,
            string hayvanAdi,
            string tur,
            string cinsiyet)
        {
            Oturum.CiftciYetkisiGerekli();

            if (string.IsNullOrWhiteSpace(kupeNo) ||
                string.IsNullOrWhiteSpace(hayvanAdi) ||
                string.IsNullOrWhiteSpace(tur) ||
                string.IsNullOrWhiteSpace(cinsiyet))
            {
                throw new Exception("Hayvan bilgileri boş bırakılamaz!");
            }

            using (SqlConnection kontrolBaglantisi =
                DatabaseConnection.GetConnection())
            {
                string kontrolSql =
                    "SELECT COUNT(*) FROM dbo.Hayvanlar WHERE KupeNo = @KupeNo";

                using (SqlCommand kontrolKomut =
                    new SqlCommand(kontrolSql, kontrolBaglantisi))
                {
                    kontrolKomut.Parameters.AddWithValue("@KupeNo", kupeNo.Trim());

                    kontrolBaglantisi.Open();

                    int kayitSayisi = (int)kontrolKomut.ExecuteScalar();

                    if (kayitSayisi > 0)
                    {
                        throw new Exception(
                            "Bu küpe numarasına sahip bir hayvan zaten kayıtlı!");
                    }
                }
            }

            using (SqlConnection baglanti =
                DatabaseConnection.GetConnection())
            {
                string sql =
                    @"INSERT INTO dbo.Hayvanlar
                      (KupeNo, HayvanAdi, Tur, Cinsiyet)
                      VALUES
                      (@KupeNo, @HayvanAdi, @Tur, @Cinsiyet)";

                using (SqlCommand komut =
                    new SqlCommand(sql, baglanti))
                {
                    komut.Parameters.AddWithValue(
                        "@KupeNo", kupeNo);
                    komut.Parameters.AddWithValue(
                        "@HayvanAdi", hayvanAdi);
                    komut.Parameters.AddWithValue(
                        "@Tur", tur);
                    komut.Parameters.AddWithValue(
                        "@Cinsiyet", cinsiyet);

                    baglanti.Open();
                    komut.ExecuteNonQuery();
                   
                }
            }
        }

        // UPDATE - Hayvan bilgilerini guncelle
        public void HayvanGuncelle(
            int hayvanID,
            string hayvanAdi,
            string tur,
            string cinsiyet)
        {
            Oturum.CiftciYetkisiGerekli();
            using (SqlConnection baglanti =
                DatabaseConnection.GetConnection())
            {
                string sql =
                    @"UPDATE dbo.Hayvanlar
                      SET HayvanAdi = @HayvanAdi,
                          Tur = @Tur,
                          Cinsiyet = @Cinsiyet
                      WHERE HayvanID = @HayvanID";

                using (SqlCommand komut =
                    new SqlCommand(sql, baglanti))
                {
                    komut.Parameters.AddWithValue(
                        "@HayvanID", hayvanID);
                    komut.Parameters.AddWithValue(
                        "@HayvanAdi", hayvanAdi);
                    komut.Parameters.AddWithValue(
                        "@Tur", tur);
                    komut.Parameters.AddWithValue(
                        "@Cinsiyet", cinsiyet);

                    baglanti.Open();
                    komut.ExecuteNonQuery();
                }
            }
        }

        // DELETE - Hayvan sil
        public void HayvanSil(int hayvanID)
        {
            Oturum.CiftciYetkisiGerekli();
            using (SqlConnection baglanti =
                DatabaseConnection.GetConnection())
            {
                string sql =
                    @"DELETE FROM dbo.Hayvanlar
                      WHERE HayvanID = @HayvanID";

                using (SqlCommand komut =
                    new SqlCommand(sql, baglanti))
                {
                    komut.Parameters.AddWithValue(
                        "@HayvanID", hayvanID);

                    baglanti.Open();
                    komut.ExecuteNonQuery();
                }
            }
        }

        public DataTable HayvanlariFiltrele(string kupeNo, string tur)
        {
            DataTable tablo = new DataTable();

            using (SqlConnection baglanti =
                DatabaseConnection.GetConnection())
            {
                string sql = @"
            SELECT HayvanID, KupeNo, HayvanAdi,
                   Tur, Cinsiyet
            FROM dbo.Hayvanlar
            WHERE (@KupeNo IS NULL OR KupeNo LIKE @KupeNo + '%')
              AND (@Tur IS NULL OR Tur = @Tur)
            ORDER BY HayvanID";

                using (SqlCommand komut =
                    new SqlCommand(sql, baglanti))
                {
                    komut.Parameters.Add("@KupeNo", SqlDbType.NVarChar, 100)
                        .Value = string.IsNullOrWhiteSpace(kupeNo)
                        ? (object)DBNull.Value : kupeNo.Trim();

                    komut.Parameters.Add("@Tur", SqlDbType.NVarChar, 100)
                        .Value = string.IsNullOrWhiteSpace(tur) || tur == "Tümü"
                        ? (object)DBNull.Value : tur;

                    using (SqlDataAdapter adapter =
                        new SqlDataAdapter(komut))
                    {
                        adapter.Fill(tablo);
                    }
                }
            }

            return tablo;
        }

    }
}
