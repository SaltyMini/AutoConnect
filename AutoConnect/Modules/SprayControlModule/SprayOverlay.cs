using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace AutoConnect.Modules.SprayControlModule;

public partial class SprayOverlay : Window
{

    private Color _patternColour;
    private Color _sprayColour;
    
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        
        [DllImport("user32.dll")]
        static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
    
        const int GWL_EXSTYLE = -20;
        const uint WS_EX_LAYERED = 0x80000;
        const uint WS_EX_TRANSPARENT = 0x20;

    public SprayOverlay(Color? patternColour = null, Color? sprayColour = null)
    {
        _patternColour = patternColour ?? Color.FromArgb(255, 230, 200, 255);
        _sprayColour = sprayColour ?? Color.FromArgb(255, 255, 0, 0);
        
        InitializeComponent();
        ConfigWindow();
    }

    private void ConfigWindow()
    {
        this.WindowStyle = WindowStyle.None;
        this.AllowsTransparency = true;
        this.Background = Brushes.Transparent;
        this.Topmost = true;
        this.ShowInTaskbar = false;
        this.WindowStartupLocation = WindowStartupLocation.Manual;
        this.ResizeMode = ResizeMode.NoResize;
        
        this.Width = 300;
        this.Height = 500;

        this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 50;
        this.Top = 50;

        this.Loaded += OnWindowLoaded;
    }

    private void OnWindowLoaded(object sender, RoutedEventArgs e)
    {
        MakeClickPassthrough();
    }

    private void MakeClickPassthrough()
    {
        var hwnd = new WindowInteropHelper(this).Handle;
        var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
        SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
    }

    public void SpawnWindow()
    {
        this.Show();
    }
}