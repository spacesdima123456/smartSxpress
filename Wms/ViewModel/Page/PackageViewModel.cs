using System.Collections.ObjectModel;
using Wms.API.Models;

namespace Wms.ViewModel.Page
{
    public class PackageViewModel: BaseViewModel
    {
        public ObservableCollection<Countries> Countries => new ObservableCollection<Countries>(App.Data.Data.Countries);
    }
}
