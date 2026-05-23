using System;
using System.Windows.Forms;

namespace eKhwarizmi_Browser
{
  public partial class Settings : Form
  {
    public Action<string> OpenUrlInBrowser;

    public Settings()
    {
      InitializeComponent();
      StartPosition = FormStartPosition.CenterScreen;
      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = MinimizeBox = false;
      ShowIcon = false;
      AutoScaleMode = AutoScaleMode.None;
      SizeGripStyle = SizeGripStyle.Hide;
      richTextBox1.ReadOnly = true;
      richTextBox1.ShortcutsEnabled = false;
      richTextBox1.Cursor = Cursors.Default;
      label1.AutoSize = true;
      label1.Cursor = Cursors.Hand;
      label1.Text = $"New Company © 2025 - {DateTime.Now.Year}";
      button1.Click += (s, e) => OpenInBrowserAndClose("https://newcompanystore.itch.io/");
      button2.Click += (s, e) => OpenInBrowserAndClose("https://linktr.ee/NewCompany");
      button3.Click += (s, e) => Close();
      label1.Click += (s, e) => OpenInBrowserAndClose("https://linktr.ee/NewCompany");
    }

    private void OpenInBrowserAndClose(string url)
    {
      OpenUrlInBrowser?.Invoke(url);
      Close();
    }
  }
}