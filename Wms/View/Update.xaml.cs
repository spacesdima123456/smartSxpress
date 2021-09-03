using System.Windows;
using Wms.Services.Updater;
using Wms.ViewModel;

namespace Wms
{
    /// <summary>
    /// Логика взаимодействия для Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        public Update(InfoVersion info)
        {
            InitializeComponent();
            DataContext = new UpdateViewModel(info);
        }
    }
}
