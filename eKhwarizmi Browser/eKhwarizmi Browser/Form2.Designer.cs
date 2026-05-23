namespace eKhwarizmi_Browser
{
  partial class Settings
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
      pictureBox1 = new PictureBox();
      pictureBox2 = new PictureBox();
      label1 = new Label();
      label2 = new Label();
      richTextBox1 = new RichTextBox();
      button1 = new Button();
      button2 = new Button();
      button3 = new Button();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
      ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
      SuspendLayout();
      // 
      // pictureBox1
      // 
      pictureBox1.Image = Properties.Resources.kmc_20260520_180346;
      pictureBox1.Location = new Point(42, 12);
      pictureBox1.Name = "pictureBox1";
      pictureBox1.Size = new Size(216, 105);
      pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      pictureBox1.TabIndex = 0;
      pictureBox1.TabStop = false;
      // 
      // pictureBox2
      // 
      pictureBox2.Image = Properties.Resources.kmc_20260520_180647;
      pictureBox2.Location = new Point(108, 359);
      pictureBox2.Name = "pictureBox2";
      pictureBox2.Size = new Size(75, 69);
      pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
      pictureBox2.TabIndex = 1;
      pictureBox2.TabStop = false;
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label1.Location = new Point(21, 431);
      label1.Name = "label1";
      label1.Size = new Size(260, 25);
      label1.TabIndex = 2;
      label1.Text = "New Company © 2025 - 2026";
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Location = new Point(74, 120);
      label2.Name = "label2";
      label2.Size = new Size(148, 30);
      label2.TabIndex = 4;
      label2.Text = "    Browser Version 1.0.0.0\r\n eKhwarizmi Version 1.0.0.0";
      // 
      // richTextBox1
      // 
      richTextBox1.Location = new Point(12, 153);
      richTextBox1.Name = "richTextBox1";
      richTextBox1.Size = new Size(274, 134);
      richTextBox1.TabIndex = 5;
      richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // button1
      // 
      button1.Location = new Point(21, 293);
      button1.Name = "button1";
      button1.Size = new Size(114, 23);
      button1.TabIndex = 6;
      button1.Text = "Check for updates";
      button1.UseVisualStyleBackColor = true;
      // 
      // button2
      // 
      button2.Location = new Point(173, 293);
      button2.Name = "button2";
      button2.Size = new Size(113, 23);
      button2.TabIndex = 7;
      button2.Text = "Help and support";
      button2.UseVisualStyleBackColor = true;
      // 
      // button3
      // 
      button3.Location = new Point(108, 322);
      button3.Name = "button3";
      button3.Size = new Size(75, 23);
      button3.TabIndex = 8;
      button3.Text = "Cancel";
      button3.UseVisualStyleBackColor = true;
      // 
      // Settings
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(298, 456);
      Controls.Add(button3);
      Controls.Add(button2);
      Controls.Add(button1);
      Controls.Add(richTextBox1);
      Controls.Add(label2);
      Controls.Add(label1);
      Controls.Add(pictureBox2);
      Controls.Add(pictureBox1);
      Icon = (Icon)resources.GetObject("$this.Icon");
      MaximizeBox = false;
      Name = "Settings";
      ShowIcon = false;
      Text = "Settings > About";
      TopMost = true;
      ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
      ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private PictureBox pictureBox1;
    private PictureBox pictureBox2;
    private Label label1;
    private Label label2;
    private RichTextBox richTextBox1;
    private Button button1;
    private Button button2;
    private Button button3;
  }
}