using Wms.Services.Window.Contract;

namespace Wms.Services.Window
{
    public class WindowDialog : IWindowFactory
    {
        private System.Windows.Window _window;

        public WindowDialog(System.Windows.Window window)
        {
            _window = window;
        }
        public void CreateWindow()
        {
            _window.ShowDialog();
        }
    }
}
