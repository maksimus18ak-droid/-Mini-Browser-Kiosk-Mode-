// mini_browser.cs — C# версия

using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

class MiniBrowser : Form {
    private WebView2 webView;
    private string targetUrl;
    private bool fullscreen;

    public MiniBrowser(string url, bool fs) {
        targetUrl = url;
        fullscreen = fs;
        this.Text = "Mini-Browser (Kiosk)";
        this.WindowState = fs ? FormWindowState.Maximized : FormWindowState.Normal;
        this.FormBorderStyle = fs ? FormBorderStyle.None : FormBorderStyle.Sizable;
        this.TopMost = fs;

        webView = new WebView2();
        webView.Dock = DockStyle.Fill;
        this.Controls.Add(webView);

        // Инициализация WebView2
        webView.CoreWebView2InitializationCompleted += (sender, e) => {
            webView.CoreWebView2.Navigate(targetUrl);
            webView.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;
        };
        webView.EnsureCoreWebView2Async(null);

        this.KeyDown += (sender, e) => {
            if (e.KeyCode == Keys.Escape) {
                Application.Exit();
            }
            if (e.Control && e.Shift && e.KeyCode == Keys.Q) {
                Application.Exit();
            }
        };

        this.Load += (sender, e) => {
            Console.WriteLine("🌐 Mini-Browser (C#)");
            Console.WriteLine($"Открыт URL: {targetUrl}");
            Console.WriteLine("Для выхода нажмите Esc или Ctrl+Shift+Q");
        };
    }

    protected override void OnFormClosing(FormClosingEventArgs e) {
        webView?.Dispose();
        base.OnFormClosing(e);
    }

    public static void Main(string[] args) {
        string url = "https://example.com";
        bool fullscreen = true;

        for (int i = 0; i < args.Length; i++) {
            if (args[i] == "--url") url = args[++i];
            else if (args[i] == "--no-fullscreen") fullscreen = false;
        }

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MiniBrowser(url, fullscreen));
    }
}
