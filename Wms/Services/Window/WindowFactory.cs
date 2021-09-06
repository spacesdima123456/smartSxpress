using System;
using System.Reflection;
using Wms.Services.Window.Contract;

namespace Wms.Services.Window
{
    public class WindowFactory : IWindowFactory
    {
        private readonly System.Windows.Window _main;
        private readonly System.Windows.Window _current;

        public WindowFactory(System.Windows.Window current, System.Windows.Window main)
        {
            _main = main;
            _current = current;
        }

        public void CreateWindow()
        {
            _current.Owner = _main.Owner;
            _main.Show();
            _current.Close();
        }
    }
}
