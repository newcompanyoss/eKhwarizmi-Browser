namespace eKhwarizmi_Browser
{
  partial class Browser
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Browser));
      pictureBox1 = new PictureBox();
      pictureBox2 = new PictureBox();
      pictureBox3 = new PictureBox();
      pictureBox4 = new PictureBox();
      pictureBox5 = new PictureBox();
      textBox1 = new TextBox();
      webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
      panel1 = new Panel();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
      ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
      ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
      ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
      ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
      ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
      panel1.SuspendLayout();
      SuspendLayout();
      // 
      // pictureBox1
      // 
      pictureBox1.BackColor = Color.FromArgb(128, 255, 128);
      pictureBox1.Image = Properties.Resources.Untitled66_20260518130004;
      pictureBox1.Location = new Point(0, 0);
      pictureBox1.Name = "pictureBox1";
      pictureBox1.Size = new Size(51, 36);
      pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      pictureBox1.TabIndex = 0;
      pictureBox1.TabStop = false;
      // 
      // pictureBox2
      // 
      pictureBox2.BackColor = Color.FromArgb(128, 255, 128);
      pictureBox2.Image = Properties.Resources.Untitled70_20260518132219;
      pictureBox2.Location = new Point(57, 0);
      pictureBox2.Name = "pictureBox2";
      pictureBox2.Size = new Size(51, 36);
      pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
      pictureBox2.TabIndex = 1;
      pictureBox2.TabStop = false;
      // 
      // pictureBox3
      // 
      pictureBox3.BackColor = Color.FromArgb(128, 255, 128);
      pictureBox3.Image = Properties.Resources.Untitled67_20260518130045;
      pictureBox3.Location = new Point(114, 0);
      pictureBox3.Name = "pictureBox3";
      pictureBox3.Size = new Size(51, 36);
      pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
      pictureBox3.TabIndex = 2;
      pictureBox3.TabStop = false;
      // 
      // pictureBox4
      // 
      pictureBox4.BackColor = Color.FromArgb(128, 255, 128);
      pictureBox4.Image = Properties.Resources.Untitled68_20260518131343;
      pictureBox4.Location = new Point(671, 0);
      pictureBox4.Name = "pictureBox4";
      pictureBox4.Size = new Size(51, 36);
      pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
      pictureBox4.TabIndex = 3;
      pictureBox4.TabStop = false;
      // 
      // pictureBox5
      // 
      pictureBox5.BackColor = Color.FromArgb(128, 255, 128);
      pictureBox5.Image = Properties.Resources.Untitled69_20260518131846;
      pictureBox5.Location = new Point(737, 0);
      pictureBox5.Name = "pictureBox5";
      pictureBox5.Size = new Size(51, 36);
      pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
      pictureBox5.TabIndex = 4;
      pictureBox5.TabStop = false;
      // 
      // textBox1
      // 
      textBox1.Location = new Point(171, 3);
      textBox1.Name = "textBox1";
      textBox1.Size = new Size(483, 23);
      textBox1.TabIndex = 5;
      // 
      // webView21
      // 
      webView21.AllowExternalDrop = true;
      webView21.BackColor = SystemColors.Window;
      webView21.CreationProperties = null;
      webView21.DefaultBackgroundColor = Color.White;
      webView21.Dock = DockStyle.Fill;
      webView21.Location = new Point(0, 49);
      webView21.Name = "webView21";
      webView21.Size = new Size(800, 401);
      webView21.TabIndex = 6;
      webView21.ZoomFactor = 1D;
      // 
      // panel1
      // 
      panel1.BackColor = Color.FromArgb(190, 255, 190);
      panel1.Controls.Add(textBox1);
      panel1.Controls.Add(pictureBox1);
      panel1.Controls.Add(pictureBox2);
      panel1.Controls.Add(pictureBox3);
      panel1.Controls.Add(pictureBox4);
      panel1.Controls.Add(pictureBox5);
      panel1.Dock = DockStyle.Top;
      panel1.Location = new Point(0, 0);
      panel1.Name = "panel1";
      panel1.Size = new Size(800, 49);
      panel1.TabIndex = 7;
      // 
      // Browser
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = SystemColors.Window;
      ClientSize = new Size(800, 450);
      Controls.Add(webView21);
      Controls.Add(panel1);
      Icon = (Icon)resources.GetObject("$this.Icon");
      Name = "Browser";
      Text = "eKhwarizmi Browser";
      ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
      ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
      ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
      ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
      ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
      ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
      panel1.ResumeLayout(false);
      panel1.PerformLayout();
      ResumeLayout(false);
    }

    #endregion

    private PictureBox pictureBox1;
    private PictureBox pictureBox2;
    private PictureBox pictureBox3;
    private PictureBox pictureBox4;
    private PictureBox pictureBox5;
    private TextBox textBox1;
    private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
    private Panel panel1;
  }
}
