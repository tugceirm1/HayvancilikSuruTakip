namespace HayvancilikSuruTakip
{
    partial class btnListele
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvHayvanlar = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.txtKupeNo = new System.Windows.Forms.TextBox();
            this.txtHayvanAdi = new System.Windows.Forms.TextBox();
            this.txtTur = new System.Windows.Forms.TextBox();
            this.txtCinsiyet = new System.Windows.Forms.TextBox();
            this.btnEkle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGuncelle = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHayvanlar)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHayvanlar
            // 
            this.dgvHayvanlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHayvanlar.Location = new System.Drawing.Point(12, 195);
            this.dgvHayvanlar.Name = "dgvHayvanlar";
            this.dgvHayvanlar.RowHeadersWidth = 51;
            this.dgvHayvanlar.RowTemplate.Height = 24;
            this.dgvHayvanlar.Size = new System.Drawing.Size(627, 164);
            this.dgvHayvanlar.TabIndex = 1;
            this.dgvHayvanlar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHayvanlar_CellContentClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(214, 48);
            this.button2.TabIndex = 2;
            this.button2.Text = "Hayvanları Listele";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtKupeNo
            // 
            this.txtKupeNo.Location = new System.Drawing.Point(37, 417);
            this.txtKupeNo.Name = "txtKupeNo";
            this.txtKupeNo.Size = new System.Drawing.Size(118, 22);
            this.txtKupeNo.TabIndex = 3;
            // 
            // txtHayvanAdi
            // 
            this.txtHayvanAdi.Location = new System.Drawing.Point(279, 417);
            this.txtHayvanAdi.Name = "txtHayvanAdi";
            this.txtHayvanAdi.Size = new System.Drawing.Size(118, 22);
            this.txtHayvanAdi.TabIndex = 4;
            // 
            // txtTur
            // 
            this.txtTur.Location = new System.Drawing.Point(37, 499);
            this.txtTur.Name = "txtTur";
            this.txtTur.Size = new System.Drawing.Size(118, 22);
            this.txtTur.TabIndex = 5;
            // 
            // txtCinsiyet
            // 
            this.txtCinsiyet.Location = new System.Drawing.Point(279, 499);
            this.txtCinsiyet.Name = "txtCinsiyet";
            this.txtCinsiyet.Size = new System.Drawing.Size(118, 22);
            this.txtCinsiyet.TabIndex = 6;
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(22, 565);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(110, 33);
            this.btnEkle.TabIndex = 7;
            this.btnEkle.Text = "Hayvan Ekle";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 375);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Küpe No:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 375);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Hayvan adı:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 454);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Tür:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(276, 463);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Cinsiyet:";
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.Location = new System.Drawing.Point(175, 565);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(142, 33);
            this.btnGuncelle.TabIndex = 12;
            this.btnGuncelle.Text = "Hayvan Güncelle";
            this.btnGuncelle.UseVisualStyleBackColor = true;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(358, 565);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(119, 32);
            this.btnSil.TabIndex = 13;
            this.btnSil.Text = "Hayvan sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnListele
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 610);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.txtCinsiyet);
            this.Controls.Add(this.txtTur);
            this.Controls.Add(this.txtHayvanAdi);
            this.Controls.Add(this.txtKupeNo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dgvHayvanlar);
            this.Name = "btnListele";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHayvanlar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvHayvanlar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtKupeNo;
        private System.Windows.Forms.TextBox txtHayvanAdi;
        private System.Windows.Forms.TextBox txtTur;
        private System.Windows.Forms.TextBox txtCinsiyet;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.Button btnSil;
    }
}

