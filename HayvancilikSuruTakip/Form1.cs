using HayvancilikSuruTakip.Security;
using HayvancilikSuruTakip.Security.HayvancilikSuruTakip.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HayvancilikSuruTakip
{
    public partial class btnListele : Form
    {
        public btnListele()
        {
            
            InitializeComponent();
            if (Oturum.Rol == "Uzman")
            {
                btnEkle.Enabled = false;
                btnGuncelle.Enabled = false;
                btnSil.Enabled = false;
            }

            Label lblOturum = new Label
            {
                Text = "Kullanıcı: " + Oturum.KullaniciAdi +
           " | Rol: " + Oturum.Rol,
                AutoSize = true,
                Location = new System.Drawing.Point(20, 60),
                ForeColor = System.Drawing.Color.DarkBlue
            };

            Controls.Add(lblOturum);
            lblOturum.BringToFront();
            Button btnHesap = new Button
            {
                Text = "Hesap Bilgilerimi Değiştir",
                Width = 210,
                Height = 35,
                Location = new Point(20, 20)
            };

            btnHesap.Click += (sender, e) =>
            {
                using (var form = new HesapDegistirForm())
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        Close();
                    }
                }
            };

            Controls.Add(btnHesap);
            btnHesap.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                using (var connection =
                    HayvancilikSuruTakip.Data.DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    MessageBox.Show("Veritabanı bağlantısı başarılı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı hatası: " + ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                var repository =
                    new HayvancilikSuruTakip.Data.HayvanRepository();

                dgvHayvanlar.DataSource =
                    repository.HayvanlariListele();

                MessageBox.Show("Hayvanlar başarıyla listelendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(txtKupeNo.Text) ||
                    string.IsNullOrWhiteSpace(txtHayvanAdi.Text) ||
                    string.IsNullOrWhiteSpace(txtTur.Text) ||
                    string.IsNullOrWhiteSpace(txtCinsiyet.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.");
                    return;
                }

                var repository =
                    new HayvancilikSuruTakip.Data.HayvanRepository();

                repository.HayvanEkle(
                    txtKupeNo.Text.Trim(),
                    txtHayvanAdi.Text.Trim(),
                    txtTur.Text.Trim(),
                    txtCinsiyet.Text.Trim()
                );

                MessageBox.Show("Hayvan başarıyla eklendi!");

                dgvHayvanlar.DataSource =
                    repository.HayvanlariListele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt hatası: " + ex.Message);
            }

        }

        private void dgvHayvanlar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow satir =
                dgvHayvanlar.Rows[e.RowIndex];

            if (satir.Cells["HayvanID"].Value == null)
                return;

            txtKupeNo.Text =
                Convert.ToString(satir.Cells["KupeNo"].Value);

            txtHayvanAdi.Text =
                Convert.ToString(satir.Cells["HayvanAdi"].Value);

            txtTur.Text =
                Convert.ToString(satir.Cells["Tur"].Value);

            txtCinsiyet.Text =
                Convert.ToString(satir.Cells["Cinsiyet"].Value);
        }


    }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {


            try
            {
                if (dgvHayvanlar.CurrentRow == null ||
                    dgvHayvanlar.CurrentRow.IsNewRow)
                {
                    MessageBox.Show("Önce listeden bir hayvan seçin.");
                    return;
                }

                int hayvanID = Convert.ToInt32(
                    dgvHayvanlar.CurrentRow.Cells["HayvanID"].Value);

                string yeniAd = txtHayvanAdi.Text.Trim();
                string yeniTur = txtTur.Text.Trim();
                string yeniCinsiyet = txtCinsiyet.Text.Trim();

                if (string.IsNullOrWhiteSpace(yeniAd) ||
                    string.IsNullOrWhiteSpace(yeniTur) ||
                    string.IsNullOrWhiteSpace(yeniCinsiyet))
                {
                    MessageBox.Show("Lütfen tüm bilgileri doldurun.");
                    return;
                }

                var repository =
                    new HayvancilikSuruTakip.Data.HayvanRepository();

                repository.HayvanGuncelle(
                    hayvanID,
                    yeniAd,
                    yeniTur,
                    yeniCinsiyet
                );

                MessageBox.Show("Güncelleme komutu çalıştırıldı. Listeyi kontrol edin.");

                dgvHayvanlar.DataSource =
                    repository.HayvanlariListele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme hatası: " + ex.Message);
            }

            MessageBox.Show("Hayvan başarıyla güncellendi!");
            }

        private void btnSil_Click(object sender, EventArgs e)
        {

            try
            {
                if (dgvHayvanlar.CurrentRow == null ||
                    dgvHayvanlar.CurrentRow.IsNewRow ||
                    dgvHayvanlar.CurrentRow.Cells["HayvanID"].Value == null)
                {
                    MessageBox.Show("Lütfen listeden bir hayvan seçin.");
                    return;
                }

                int hayvanID = Convert.ToInt32(
                    dgvHayvanlar.CurrentRow.Cells["HayvanID"].Value);

                string hayvanAdi = Convert.ToString(
                    dgvHayvanlar.CurrentRow.Cells["HayvanAdi"].Value);

                DialogResult cevap = MessageBox.Show(
                    hayvanAdi + " adlı hayvanı silmek istiyor musunuz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (cevap != DialogResult.Yes)
                    return;

                var repository =
                    new HayvancilikSuruTakip.Data.HayvanRepository();

                repository.HayvanSil(hayvanID);

                MessageBox.Show("Hayvan başarıyla silindi!");

                dgvHayvanlar.DataSource =
                    repository.HayvanlariListele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme hatası: " + ex.Message);
            }

        }
    }
    }

