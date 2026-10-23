
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
    }
}
