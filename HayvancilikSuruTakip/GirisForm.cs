
using HayvancilikSuruTakip.Data;
using HayvancilikSuruTakip.Security;
using HayvancilikSuruTakip.Security.HayvancilikSuruTakip.Security;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HayvancilikSuruTakip
{
    public class GirisForm : Form
    {
        private TextBox txtKullaniciAdi;
        private TextBox txtParola;

        public GirisForm()
        {
            Text = "Hayvancılık Sürü Takip - Giriş";
            Size = new Size(420, 330);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            Label lblBaslik = new Label
            {
                Text = "HAYVANCILIK SÜRÜ TAKİP",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(45, 20),
                AutoSize = true
            };

            Label lblKullanici = new Label
            {
                Text = "Kullanıcı Adı",
                Location = new Point(45, 75),
                AutoSize = true
            };

            txtKullaniciAdi = new TextBox
            {
                Location = new Point(45, 98),
                Width = 310
            };

            Label lblParola = new Label
            {
                Text = "Parola",
                Location = new Point(45, 135),
                AutoSize = true
            };

            txtParola = new TextBox
            {
                Location = new Point(45, 158),
                Width = 310,
                UseSystemPasswordChar = true
            };

            Button btnGiris = new Button
            {
                Text = "Giriş Yap",
                Location = new Point(45, 205),
                Width = 145,
                Height = 35
            };

            Button btnKayit = new Button
            {
                Text = "Kayıt Ol",
                Location = new Point(210, 205),
                Width = 145,
                Height = 35
            };

            btnGiris.Click += BtnGiris_Click;
            btnKayit.Click += BtnKayit_Click;

            Controls.AddRange(new Control[]
            {
                lblBaslik,
                lblKullanici,
                txtKullaniciAdi,
                lblParola,
                txtParola,
                btnGiris,
                btnKayit
            });

            AcceptButton = btnGiris;
        }

        private void BtnKayit_Click(object sender, EventArgs e)
        {
            using (KayitForm form = new KayitForm())
            {
                form.ShowDialog(this);
            }
        }

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                var repository = new KullaniciRepository();

                bool basarili = repository.GirisYap(
                    txtKullaniciAdi.Text,
                    txtParola.Text);

                if (!basarili)
                {
                    MessageBox.Show(
                        "Giriş başarısız. Kullanıcı adı veya parola " +
                        "hatalı olabilir ya da hesap kilitlenmiş olabilir.");
                    return;
                }

                MessageBox.Show("Giriş başarılı!");

                Hide();

                try
                {
                    using (var anaForm = new btnListele())
                    {
                        anaForm.ShowDialog();
                    }
                }
                finally
                {
            {
                Oturum.Kapat();
                txtParola.Clear();
                Show();
            }
        }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Giriş işlemi başarısız: " + ex.Message);
            }
        }
    }
}
