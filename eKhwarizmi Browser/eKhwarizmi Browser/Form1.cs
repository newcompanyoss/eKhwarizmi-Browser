using Microsoft.Web.WebView2.Core;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eKhwarizmi_Browser
{
  public partial class Browser : Form
  {
    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    private const int HOTKEY_F11 = 1;
    private const int HOTKEY_F5 = 2;
    private const int HOTKEY_F1 = 3;
    private const int HOTKEY_CTRL_O = 4;
    private const uint VK_F11 = 0x7A;
    private const uint VK_F5 = 0x74;
    private const uint VK_F1 = 0x70;
    private const uint VK_O = 0x4F;

    bool fullscreen;
    Rectangle oldBounds;
    FormBorderStyle oldBorder;
    bool showingErrorPage;
    bool suppressAddressBarSync;
    string lastFailedUrl = "";
    string currentUrl = "";
    string pendingUrl = "";
    bool isWebViewReady = false;
    bool isNavigating = false;

    public Browser()
    {
      InitializeComponent();
      this.DoubleBuffered = true;
      this.KeyPreview = true;
      MinimumSize = new Size(900, 600);
      WindowState = FormWindowState.Maximized;
      StartPosition = FormStartPosition.CenterScreen;
      this.FormClosing += Browser_FormClosing;
      this.Load += Browser_Load;
      this.DragEnter += Browser_DragEnter;
      this.DragDrop += Browser_DragDrop;

      LoadIconsImmediately();
      InitializeWebViewEarly();

      if (Environment.GetCommandLineArgs().Length > 1)
      {
        string arg = Environment.GetCommandLineArgs()[1];
        if (arg.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            arg.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
          NavigateToUrl(arg);
        }
        else if (File.Exists(arg))
        {
          pendingUrl = arg;
        }
      }
    }

    private void Browser_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.Copy;
    }

    private void Browser_DragDrop(object sender, DragEventArgs e)
    {
      string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
      if (files != null && files.Length > 0 && File.Exists(files[0]))
      {
        OpenLocalFile(files[0]);
      }
      e.Effect = DragDropEffects.Copy;
    }

    private void OpenLocalFile(string path)
    {
      if (isWebViewReady && webView21.CoreWebView2 != null)
      {
        string fileUrl = new Uri(path).AbsoluteUri;
        NavigateToUrl(fileUrl);
      }
      else
      {
        pendingUrl = path;
      }
    }

    private void Browser_Load(object sender, EventArgs e)
    {
      RegisterHotKey(this.Handle, HOTKEY_F11, 0U, VK_F11);
      RegisterHotKey(this.Handle, HOTKEY_F5, 0U, VK_F5);
      RegisterHotKey(this.Handle, HOTKEY_F1, 0U, VK_F1);
      RegisterHotKey(this.Handle, HOTKEY_CTRL_O, 0x0002U, VK_O);
    }

    private void Browser_FormClosing(object sender, FormClosingEventArgs e)
    {
      UnregisterHotKey(this.Handle, HOTKEY_F11);
      UnregisterHotKey(this.Handle, HOTKEY_F5);
      UnregisterHotKey(this.Handle, HOTKEY_F1);
      UnregisterHotKey(this.Handle, HOTKEY_CTRL_O);
      NetworkChange.NetworkAvailabilityChanged -= OnNetworkAvailabilityChanged;
    }

    protected override void WndProc(ref Message m)
    {
      const int WM_HOTKEY = 0x0312;
      if (m.Msg == WM_HOTKEY)
      {
        int id = m.WParam.ToInt32();
        if (id == HOTKEY_F11)
        {
          ToggleFullscreen();
          return;
        }
        else if (id == HOTKEY_F5)
        {
          RefreshPage();
          return;
        }
        else if (id == HOTKEY_F1)
        {
          NavigateToUrl("https://linktr.ee/NewCompany");
          return;
        }
        else if (id == HOTKEY_CTRL_O)
        {
          using (OpenFileDialog ofd = new OpenFileDialog())
          {
            ofd.Filter = "Web files|*.html;*.htm;*.xml;*.svg;*.xhtml|All files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
              OpenLocalFile(ofd.FileName);
            }
          }
          return;
        }
      }
      base.WndProc(ref m);
    }

    private void LoadIconsImmediately()
    {
      pictureBox1.Image = Properties.Resources.Untitled66_20260518130004;
      pictureBox2.Image = Properties.Resources.Untitled70_20260518132219;
      pictureBox3.Image = Properties.Resources.Untitled67_20260518130045;
      pictureBox4.Image = Properties.Resources.Untitled68_20260518131343;
      pictureBox5.Image = Properties.Resources.Untitled69_20260518131846;

      pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
      pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
      pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
      pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;

      pictureBox1.Cursor = Cursors.Hand;
      pictureBox2.Cursor = Cursors.Hand;
      pictureBox3.Cursor = Cursors.Hand;
      pictureBox4.Cursor = Cursors.Hand;
      pictureBox5.Cursor = Cursors.Hand;
    }

    private async void InitializeWebViewEarly()
    {
      string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "eKhwarizmi_Browser");
      var env = await CoreWebView2Environment.CreateAsync(userDataFolder: userDataFolder);
      await webView21.EnsureCoreWebView2Async(env);
      isWebViewReady = true;

      var settings = webView21.CoreWebView2.Settings;
      settings.IsBuiltInErrorPageEnabled = false;
      settings.AreDevToolsEnabled = false;
      settings.IsStatusBarEnabled = false;
      settings.IsSwipeNavigationEnabled = false;
      settings.AreBrowserAcceleratorKeysEnabled = false;

      webView21.CoreWebView2.NavigationStarting += (_, ev) =>
      {
        currentUrl = ev.Uri;
        if (!IsInternal(ev.Uri))
          textBox1.Text = ev.Uri;
        isNavigating = true;
        panel1.Cursor = Cursors.WaitCursor;
      };

      webView21.CoreWebView2.NavigationCompleted += (_, ev) =>
      {
        isNavigating = false;
        panel1.Cursor = Cursors.Default;
        if (suppressAddressBarSync)
        {
          suppressAddressBarSync = false;
          return;
        }
        if (ev.IsSuccess)
        {
          showingErrorPage = false;
          lastFailedUrl = "";
          if (webView21.Source != null && !IsInternal(webView21.Source.ToString()))
            textBox1.Text = webView21.Source.ToString();
        }
        else
        {
          bool isCaptchaOrNetworkError = ev.WebErrorStatus == CoreWebView2WebErrorStatus.ConnectionReset ||
                                         ev.WebErrorStatus == CoreWebView2WebErrorStatus.HostNameNotResolved ||
                                         ev.WebErrorStatus == CoreWebView2WebErrorStatus.OperationCanceled ||
                                         ev.WebErrorStatus == CoreWebView2WebErrorStatus.Timeout;
          if (!isCaptchaOrNetworkError && !string.IsNullOrWhiteSpace(currentUrl))
          {
            lastFailedUrl = string.IsNullOrWhiteSpace(currentUrl) ? textBox1.Text.Trim() : currentUrl;
            _ = ShowErrorPageAsync(lastFailedUrl);
          }
        }
      };

      NetworkChange.NetworkAvailabilityChanged += OnNetworkAvailabilityChanged;

      if (!string.IsNullOrWhiteSpace(pendingUrl))
      {
        string u = pendingUrl;
        pendingUrl = "";
        if (File.Exists(u))
          OpenLocalFile(u);
        else
          NavigateToUrl(u);
      }
      else
      {
        await LoadHomePageAsync();
      }
    }

    public async void NavigateToUrl(string url)
    {
      if (!isWebViewReady)
      {
        pendingUrl = url;
        return;
      }
      if (webView21.CoreWebView2 == null)
      {
        pendingUrl = url;
        return;
      }
      showingErrorPage = false;
      textBox1.Text = url;
      webView21.CoreWebView2.Navigate(url);
    }

    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
      panel1.Dock = DockStyle.Top;
      webView21.Dock = DockStyle.None;
      AdjustWebViewBounds();
      ArrangeToolbar();

      pictureBox1.Click += (_, __) => GoBack();
      pictureBox2.Click += (_, __) => RefreshPage();
      pictureBox3.Click += (_, __) => GoForward();
      pictureBox4.Click += (_, __) => _ = LoadHomePageAsync();
      pictureBox5.Click += (_, __) =>
      {
        var s = new Settings();
        s.OpenUrlInBrowser = NavigateToUrl;
        s.Show(this);
      };

      textBox1.KeyDown += (_, ev) =>
      {
        if (ev.KeyCode == Keys.Enter)
        {
          ev.SuppressKeyPress = true;
          NavigateOrSearch(textBox1.Text.Trim());
        }
      };

      Resize += (_, __) => { AdjustWebViewBounds(); ArrangeToolbar(); };
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      return base.ProcessCmdKey(ref msg, keyData);
    }

    private void AdjustWebViewBounds()
    {
      int top = fullscreen ? 0 : panel1.Height;
      webView21.SetBounds(0, top, ClientSize.Width, ClientSize.Height - top);
    }

    private void AdjustLayout()
    {
      if (fullscreen)
      {
        panel1.Visible = false;
      }
      else
      {
        panel1.Visible = true;
        ArrangeToolbar();
      }
      AdjustWebViewBounds();
    }

    private void ArrangeToolbar()
    {
      int b = 30, g = 8;
      int panelWidth = panel1.Width;
      if (panelWidth < 500) panelWidth = 500;
      pictureBox1.SetBounds(8, 9, b, b);
      pictureBox2.SetBounds(pictureBox1.Right + g, 9, b, b);
      pictureBox3.SetBounds(pictureBox2.Right + g, 9, b, b);
      pictureBox5.SetBounds(panelWidth - b - 8, 9, b, b);
      pictureBox4.SetBounds(pictureBox5.Left - g - b, 9, b, b);
      int textBoxLeft = pictureBox3.Right + 16;
      int textBoxWidth = Math.Max(120, pictureBox4.Left - textBoxLeft - 16);
      textBox1.SetBounds(textBoxLeft, 9, textBoxWidth, 30);
    }

    private void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
    {
      if (e.IsAvailable && showingErrorPage && !string.IsNullOrWhiteSpace(lastFailedUrl) && IsHandleCreated)
      {
        BeginInvoke(new Action(() => NavigateToUrl(lastFailedUrl)));
      }
    }

    void ToggleFullscreen()
    {
      fullscreen = !fullscreen;
      if (fullscreen)
      {
        oldBounds = Bounds;
        oldBorder = FormBorderStyle;
        FormBorderStyle = FormBorderStyle.None;
        WindowState = FormWindowState.Normal;
        Bounds = Screen.PrimaryScreen.Bounds;
        TopMost = true;
      }
      else
      {
        TopMost = false;
        FormBorderStyle = oldBorder;
        Bounds = oldBounds;
        WindowState = FormWindowState.Normal;
      }
      AdjustLayout();
    }

    void GoBack()
    {
      if (webView21.CoreWebView2?.CanGoBack == true && !isNavigating)
      {
        webView21.CoreWebView2.GoBack();
      }
    }

    void GoForward()
    {
      if (webView21.CoreWebView2?.CanGoForward == true && !isNavigating)
      {
        webView21.CoreWebView2.GoForward();
      }
    }

    void RefreshPage()
    {
      if (isNavigating) return;
      if (showingErrorPage && !string.IsNullOrWhiteSpace(lastFailedUrl))
      {
        NavigateToUrl(lastFailedUrl);
      }
      else
      {
        if (webView21.CoreWebView2 != null)
        {
          webView21.CoreWebView2.Reload();
        }
      }
    }

    void NavigateOrSearch(string input)
    {
      if (string.IsNullOrWhiteSpace(input)) return;
      string url = input.Trim();
      if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
          !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) &&
          !File.Exists(url))
      {
        if (url.Contains('.') && !url.Contains(' '))
          url = "https://" + url;
        else
          url = "https://www.google.com/search?q=" + Uri.EscapeDataString(url);
      }
      else if (File.Exists(url))
      {
        OpenLocalFile(url);
        return;
      }
      NavigateToUrl(url);
    }

    async Task LoadHomePageAsync()
    {
      if (!isWebViewReady) return;
      showingErrorPage = false;
      suppressAddressBarSync = true;
      webView21.NavigateToString("<!DOCTYPE html><html><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><style>body{margin:0;min-height:100vh;background:#fff;font-family:Arial,sans-serif;overflow:hidden}.home{position:absolute;left:50%;top:50%;transform:translate(-50%,-50%);text-align:center}.ar{font-size:96px;line-height:1;color:#4285F4;margin:0}.en{display:block;margin-top:8px;margin-bottom:26px;font-size:13px;font-style:italic;color:#4285F4}input{width:420px;max-width:84vw;height:32px;padding:4px 10px;font-size:15px;border:1px solid #777;box-sizing:border-box}</style></head><body><div class='home'><div class='ar'>جعفر</div><div class='en'>Gafar</div><input id='s' placeholder='Search or type URL'></div><script>document.getElementById('s').addEventListener('keydown',e=>{if(e.key==='Enter'){let q=e.target.value.trim();window.location=q.startsWith('http')?q:(q.includes('.')?'https://'+q:'https://www.google.com/search?q='+encodeURIComponent(q));}});</script></body></html>");
      textBox1.Text = "home";
    }

    async Task ShowErrorPageAsync(string failedUrl)
    {
      if (!isWebViewReady) return;
      showingErrorPage = true;
      suppressAddressBarSync = true;
      textBox1.Text = failedUrl;
      string safeUrl = WebUtility.HtmlEncode(failedUrl);
      string help = "https://linktr.ee/NewCompany";
      webView21.NavigateToString($@"<!DOCTYPE html>
<html>
<head>
<meta charset='UTF-8'>
<meta name='viewport' content='width=device-width, initial-scale=1.0'>
<style>
    body{{margin:0;min-height:100vh;background:#fff;font-family:Arial,sans-serif;overflow:hidden}}
    .brand{{position:absolute;left:20px;top:30px;text-align:left}}
    .ar{{font-size:44px;line-height:1;color:#4285F4;margin:0}}
    .en{{font-size:12px;font-style:italic;color:#4285F4}}
    .msg{{position:absolute;left:50%;top:42%;transform:translate(-50%,-50%);width:min(860px,92vw);text-align:center;font-size:17px;line-height:1.8;color:#222}}
    .help{{color:#4285F4;font-weight:600;text-decoration:none}}
    .url{{margin-top:18px;font-size:13px;color:#666;word-break:break-all}}
</style>
</head>
<body>
    <div class='brand'><div class='ar'>جعفر</div><div class='en'>Gafar</div></div>
    <div class='msg'>
        There was an error loading this page.<br>
        Please reconnect and try again, or get help from <a class='help' href='{help}'>this support page</a>.
        <div class='url'>{safeUrl}</div>
    </div>
</body>
</html>");
    }

    static bool IsInternal(string url) => !string.IsNullOrWhiteSpace(url) && (url.StartsWith("data:text/html", StringComparison.OrdinalIgnoreCase) || url.StartsWith("about:blank", StringComparison.OrdinalIgnoreCase));
  }
}