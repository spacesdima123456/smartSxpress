using System.Windows;
using WpfApp3.Updater;
using WpfApp3.ViewModel;

namespace WpfApp3
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
