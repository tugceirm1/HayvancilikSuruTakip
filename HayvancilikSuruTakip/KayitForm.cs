
using System;
using System.Drawing;
using System.Windows.Forms;
using HayvancilikSuruTakip.Data;


namespace HayvancilikSuruTakip
{
    public class KayitForm : Form
    {
        private TextBox txtAd;
        private TextBox txtParola;
        private TextBox txtTekrar;

        public KayitForm()
        {
            Text = "Yeni Kullanıcı Kaydı";
            Size = new Size(390, 330);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            Label lblAd = new Label
            {
                Text = "Kullanıcı Adı",
                Location = new Point(30, 25),
                AutoSize = true
            };

            txtAd = new TextBox
            {
                Location = new Point(30, 48),
                Width = 310
            };

            Label lblParola = new Label
            {
                Text = "Parola (en az 12 karakter)",
                Location = new Point(30, 85),
                AutoSize = true
            };

            txtParola = new TextBox
            {
                Location = new Point(30, 108),
                Width = 310,
                UseSystemPasswordChar = true
            };

            Label lblTekrar = new Label
            {
                Text = "Parola Tekrar",
                Location = new Point(30, 145),
                AutoSize = true
            };

            txtTekrar = new TextBox
            {
                Location = new Point(30, 168),
                Width = 310,
                UseSystemPasswordChar = true
            };

            Button btnKaydet = new Button
            {
                Text = "Kayıt Ol",
                Location = new Point(30, 215),
                Width = 310,
                Height = 35
            };

            btnKaydet.Click += BtnKaydet_Click;

            Controls.AddRange(new Control[]
            {
                lblAd, txtAd, lblParola, txtParola,
                lblTekrar, txtTekrar, btnKaydet
            });

            AcceptButton = btnKaydet;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (txtParola.Text != txtTekrar.Text)
            {
                MessageBox.Show("Parolalar eşleşmiyor.");
                return;
            }

            try
            {
                var repository = new global::HayvancilikSuruTakip.Data.KullaniciRepository();
                repository.KayitOl(
                    txtAd.Text,
                    txtParola.Text);

                MessageBox.Show(
                    "Kayıt başarılı! Şimdi giriş yapabilirsiniz.");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                    MessageBox.Show("Bu kullanıcı adı zaten kayıtlı.");
                else
                    MessageBox.Show("Veritabanı işlemi başarısız.");
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
            // KayitForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "KayitForm";
            this.Load += new System.EventHandler(this.KayitForm_Load);
            this.ResumeLayout(false);

        }

        private void KayitForm_Load(object sender, EventArgs e)
        {

        }
    }
}
