using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Microsoft.WindowsAPICodePack.Dialogs;

public class WindowsSO {

    /// <summary>
    /// Shows folder browser a dialog.
    /// NuGet: Install-Package Microsoft.WindowsAPICodePack-Shell
    /// </summary>
    /// <param name="path">Choosen folder path</param>
    /// <returns>If the user has choosen a folder, then return true. False, otherwise.</returns>
    public static bool ChooseFolder(out string path) {
        using (var dialog = new CommonOpenFileDialog { IsFolderPicker = true }) {
            path = null;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                path = dialog.FileName;
                return true;
            }
        }
        return false;
    }

    public static bool ChooseFile(out string file, string defaultDirectory = "C:/", string filters = "All|*.*", Window window = null) {
        using (var dialog = new CommonOpenFileDialog { IsFolderPicker = false }) {
            Regex regex = new Regex(@"(\s|[0-9]|[a-zA-Z0-9])+\|((\*\.[a-zA-Z0-9]+);?)+", RegexOptions.CultureInvariant);
            Match match;
            while ((match = regex.Match(filters)).Success) {
                var parts = match.Value.Split('|');
                dialog.Filters.Add(new CommonFileDialogFilter(parts[0], parts[1]));
                filters = filters.Substring(match.Value.Length);
            }
            dialog.DefaultDirectory = defaultDirectory;
            file = null;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                file = dialog.FileName;
                if (window != null) window.Focus();
                return true;
            }
        }
        if (window != null) window.Focus();
        return false;
    }

    public static void ShowError (string message, string title = "Error") {
        MessageBox.Show(message,title,MessageBoxButton.OK,MessageBoxImage.Error);
    }

    public static void ToCenterOfScreen (Window window) {
        double screenWidth = SystemParameters.PrimaryScreenWidth;
        double screenHeight = SystemParameters.PrimaryScreenHeight;
        double windowWidth = window.Width;
        double windowHeight = window.Height;
        window.Left = (screenWidth / 2) - (windowWidth / 2);
        window.Top = (screenHeight / 2) - (windowHeight / 2);
    }

    public static void InitializeWindow(Page page, string title, int width, int height, WindowStyle ws = WindowStyle.SingleBorderWindow) {
        var w = Window.GetWindow(page);
        w.Title = title;
        w.WindowStyle = ws;
        if (w.WindowState == WindowState.Normal) {
            w.Width = width;
            w.Height = height;
        }
        ToCenterOfScreen(w);
    }

    private const UInt32 FLASHW_STOP = 0; //Stop flashing. The system restores the window to its original state.        
    private const UInt32 FLASHW_CAPTION = 1; //Flash the window caption.        
    private const UInt32 FLASHW_TRAY = 2; //Flash the taskbar button.        
    private const UInt32 FLASHW_ALL = 3; //Flash both the window caption and taskbar button.        
    private const UInt32 FLASHW_TIMER = 4; //Flash continuously, until the FLASHW_STOP flag is set.        
    private const UInt32 FLASHW_TIMERNOFG = 12; //Flash continuously until the window comes to the foreground.  


    [StructLayout(LayoutKind.Sequential)]
    private struct FLASHWINFO {
        public UInt32 cbSize; //The size of the structure in bytes.            
        public IntPtr hwnd; //A Handle to the Window to be Flashed. The window can be either opened or minimized.


        public UInt32 dwFlags; //The Flash Status.            
        public UInt32 uCount; // number of times to flash the window            
        public UInt32 dwTimeout; //The rate at which the Window is to be flashed, in milliseconds. If Zero, the function uses the default cursor blink rate.        
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);



    public static void FlashWindow(Window win, UInt32 count = UInt32.MaxValue) {
        //Don't flash if the window is active            
        if (win.IsActive) return;
        WindowInteropHelper h = new WindowInteropHelper(win);
        FLASHWINFO info = new FLASHWINFO {
            hwnd = h.Handle,
            dwFlags = FLASHW_ALL | FLASHW_TIMER,
            uCount = count,
            dwTimeout = 0
        };

        info.cbSize = Convert.ToUInt32(Marshal.SizeOf(info));
        FlashWindowEx(ref info);
    }

    public static void StopFlashingWindow(Window win) {
        WindowInteropHelper h = new WindowInteropHelper(win);
        FLASHWINFO info = new FLASHWINFO();
        info.hwnd = h.Handle;
        info.cbSize = Convert.ToUInt32(Marshal.SizeOf(info));
        info.dwFlags = FLASHW_STOP;
        info.uCount = UInt32.MaxValue;
        info.dwTimeout = 0;
        FlashWindowEx(ref info);
    }

}