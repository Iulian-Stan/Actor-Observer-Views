using System.Runtime.InteropServices;
using System.Windows;

namespace Example.Util
{
    public static class WindowHelper
    {
        [DllImport("User32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        public static Point GetWindowCenter()
        {
            PresentationSource source = PresentationSource.FromVisual(Application.Current.MainWindow);
            Window window = Application.Current.MainWindow;
            return new Point((window.Left + window.Width / 2) * source.CompositionTarget.TransformToDevice.M11,
                (window.Top + window.Height / 2) * source.CompositionTarget.TransformToDevice.M22);
        }
    }
}
