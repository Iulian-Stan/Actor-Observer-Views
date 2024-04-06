using System.Runtime.InteropServices;
using System.Windows;

namespace Example.Util
{
    public static partial class WindowHelper
    {
        [LibraryImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool SetCursorPos(int X, int Y);

        public static Point GetWindowCenter()
        {
            PresentationSource source = PresentationSource.FromVisual(Application.Current.MainWindow);
            Window window = Application.Current.MainWindow;
            return new Point((window.Left + window.Width / 2) * source.CompositionTarget.TransformToDevice.M11,
                (window.Top + window.Height / 2) * source.CompositionTarget.TransformToDevice.M22);
        }
    }
}
