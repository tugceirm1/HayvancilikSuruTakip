
using HayvancilikSuruTakip.Data;
using HayvancilikSuruTakip.Security;
using HayvancilikSuruTakip.Security.HayvancilikSuruTakip.Security;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HayvancilikSuruTakip
{
    public class HesapDegistirForm : Form
    {
        private TextBox txtYeniAd;
        private TextBox txtEskiParola;
        private TextBox txtYeniParola;
        private TextBox txtParolaTekrar;

        public HesapDegistirForm()
        {
            Text = "Hesap Bilgilerimi Değiştir";
            Size = new Size(420, 390);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            Label lblAd = new Label
            {
                Text = "Yeni Kullanıcı Adı",
                Location = new Point(30, 20),
                AutoSize = true
            };

            txtYeniAd = new TextBox
            {
                Location = new Point(30, 43),
                Width = 340,
                Text = Oturum.KullaniciAdi
            };

            Label lblEski = new Label
            {
                Text = "Mevcut Şifre",
                Location = new Point(30, 83),
                AutoSize = true
            };

            txtEskiParola = new TextBox
            {
                Location = new Point(30, 106),
                Width = 340,
                UseSystemPasswordChar = true
            };

            Label lblYeni = new Label
            {
                Text = "Yeni Şifre (en az 12 karakter)",
                Location = new Point(30, 146),
                AutoSize = true
            };

            txtYeniParola = new TextBox
            {
                Location = new Point(30, 169),
                Width = 340,
                UseSystemPasswordChar = true
            };

            Label lblTekrar = new Label
            {
                Text = "Yeni Şifre Tekrar",
                Location = new Point(30, 209),
                AutoSize = true
            };

            txtParolaTekrar = new TextBox
            {
                Location = new Point(30, 232),
                Width = 340,
                UseSystemPasswordChar = true
            };

            Button btnKaydet = new Button
            {
                Text = "Değişiklikleri Kaydet",
                Location = new Point(30, 282),
                Width = 340,
                Height = 38
            };

            btnKaydet.Click += BtnKaydet_Click;

            Controls.AddRange(new Control[]
            {
                lblAd, txtYeniAd,
                lblEski, txtEskiParola,
                lblYeni, txtYeniParola,
                lblTekrar, txtParolaTekrar,
                btnKaydet
            });

            AcceptButton = btnKaydet;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (txtYeniParola.Text != txtParolaTekrar.Text)
            {
                MessageBox.Show("Yeni şifreler eşleşmiyor.");
                return;
            }

            try
            {
                var repository = new KullaniciRepository();

                repository.HesapBilgileriniDegistir(
                    Oturum.KullaniciID,
                    txtEskiParola.Text,
                    txtYeniAd.Text,
                    txtYeniParola.Text);

                MessageBox.Show(
                    "Hesap bilgileriniz güncellendi. " +
                    "Yeni bilgilerinizle tekrar giriş yapın.");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                    MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor.");
                else
                    MessageBox.Show("Veritabanı güncellemesi başarısız.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HesapDegistirForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "HesapDegistirForm";
            this.Load += new System.EventHandler(this.HesapDegistirForm_Load);
            this.ResumeLayout(false);

        }

        private void HesapDegistirForm_Load(object sender, EventArgs e)
        {

        }
    }
}
