
namespace ValorantKolayGiris_FormDesktop_.forms
{
    partial class FrmAnasayfa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnasayfa));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButtonSecenekler = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bilgisayarİleBirlikteAçılsınToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oyunAçılıncaUygulamayıKapatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capslockKapatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makroKullanTavsiyeEdilenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonSecenekler});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(505, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButtonSecenekler
            // 
            this.toolStripDropDownButtonSecenekler.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonSecenekler.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.bilgisayarİleBirlikteAçılsınToolStripMenuItem,
            this.oyunAçılıncaUygulamayıKapatToolStripMenuItem,
            this.capslockKapatToolStripMenuItem,
            this.makroKullanTavsiyeEdilenToolStripMenuItem});
            this.toolStripDropDownButtonSecenekler.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonSecenekler.Image")));
            this.toolStripDropDownButtonSecenekler.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonSecenekler.Name = "toolStripDropDownButtonSecenekler";
            this.toolStripDropDownButtonSecenekler.Size = new System.Drawing.Size(34, 24);
            this.toolStripDropDownButtonSecenekler.Text = "seçenekler";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(451, 26);
            this.toolStripMenuItem1.Text = "şifre ekle";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // bilgisayarİleBirlikteAçılsınToolStripMenuItem
            // 
            this.bilgisayarİleBirlikteAçılsınToolStripMenuItem.Name = "bilgisayarİleBirlikteAçılsınToolStripMenuItem";
            this.bilgisayarİleBirlikteAçılsınToolStripMenuItem.Size = new System.Drawing.Size(451, 26);
            this.bilgisayarİleBirlikteAçılsınToolStripMenuItem.Text = "Bilgisayar ile birlikte açılsın";
            this.bilgisayarİleBirlikteAçılsınToolStripMenuItem.Click += new System.EventHandler(this.bilgisayarİleBirlikteAçılsınToolStripMenuItem_Click);
            // 
            // oyunAçılıncaUygulamayıKapatToolStripMenuItem
            // 
            this.oyunAçılıncaUygulamayıKapatToolStripMenuItem.Name = "oyunAçılıncaUygulamayıKapatToolStripMenuItem";
            this.oyunAçılıncaUygulamayıKapatToolStripMenuItem.Size = new System.Drawing.Size(451, 26);
            this.oyunAçılıncaUygulamayıKapatToolStripMenuItem.Text = "Oyun açılınca uygulamayı kapat";
            this.oyunAçılıncaUygulamayıKapatToolStripMenuItem.Click += new System.EventHandler(this.oyunAçılıncaUygulamayıKapatToolStripMenuItem_Click);
            // 
            // capslockKapatToolStripMenuItem
            // 
            this.capslockKapatToolStripMenuItem.Name = "capslockKapatToolStripMenuItem";
            this.capslockKapatToolStripMenuItem.Size = new System.Drawing.Size(451, 26);
            this.capslockKapatToolStripMenuItem.Text = "Capslock kapat";
            this.capslockKapatToolStripMenuItem.Click += new System.EventHandler(this.capslockKapatToolStripMenuItem_Click);
            // 
            // makroKullanTavsiyeEdilenToolStripMenuItem
            // 
            this.makroKullanTavsiyeEdilenToolStripMenuItem.Name = "makroKullanTavsiyeEdilenToolStripMenuItem";
            this.makroKullanTavsiyeEdilenToolStripMenuItem.Size = new System.Drawing.Size(451, 26);
            this.makroKullanTavsiyeEdilenToolStripMenuItem.Text = "Makro(mouse tekerlek tıklanma) Kullan(Tavsiye Edilen)";
            this.makroKullanTavsiyeEdilenToolStripMenuItem.Click += new System.EventHandler(this.makroKullanTavsiyeEdilenToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(505, 349);
            this.panel1.TabIndex = 2;
            // 
            // FrmAnasayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 376);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAnasayfa";
            this.Activated += new System.EventHandler(this.FrmAnasayfa_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAnasayfa_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LocationChanged += new System.EventHandler(this.FrmAnasayfa_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.FrmAnasayfa_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonSecenekler;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem bilgisayarİleBirlikteAçılsınToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oyunAçılıncaUygulamayıKapatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capslockKapatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makroKullanTavsiyeEdilenToolStripMenuItem;
    }
}

