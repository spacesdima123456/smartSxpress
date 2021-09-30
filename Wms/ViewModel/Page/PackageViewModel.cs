using Wms.Models;
using System.Linq;
using Wms.API.Models;
using DevExpress.Mvvm;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Wms.ViewModel.Page
{
    public class PackageViewModel: BaseViewModel
    {
        private DocType _docTypeSender;
        public DocType DocTypeSender
        {
            get => _docTypeSender;
            set => Set(nameof(DocTypeSender), ref _docTypeSender, value);
        }

        public static DocType DocTypeRecipient => App.Data.Data.DocType[1];

        private Countries _countryRecipient;
        public Countries CountryRecipient
        {
            get => _countryRecipient;
            set => Set(nameof(CountryRecipient), ref _countryRecipient, value);
        }

        private ObservableCollection<Boxes> _boxes;
        public ObservableCollection<Boxes> Boxes
        {
            get => _boxes;
            set => Set(nameof(Boxes), ref _boxes, value);
        }

        private ICommand _addBoxCommand;
        public ICommand AddBoxCommand => _addBoxCommand??=new DelegateCommand(() =>
        {
            var item = Boxes.LastOrDefault();
            if (item!=null && item.Height > 0 && item.Length > 0 && item.Weight > 0 && item.Width > 0)
                Boxes.Add(new Boxes { Number = Boxes.Max(m=>m.Number) + 1 });
        });

        private ICommand _removeBoxCommand;
        public ICommand RemoveBoxCommand => _removeBoxCommand ??= new DelegateCommand<Boxes>((box) =>
        {
            if (Boxes.Count>1)
                Boxes.Remove(box);
        });

        public  ObservableCollection<Countries> CountriesRecipient { get;  }

        public static  ObservableCollection<DocType> DocTypes => new ObservableCollection<DocType>(App.Data.Data.DocType);
        public static Countries CountrySender => CountriesSender.FirstOrDefault(f => f.Name == App.Data.Data.Customer.CountryName);
        public static ObservableCollection<Countries> CountriesSender => new ObservableCollection<Countries>(App.Data.Data.Countries);
        public PackageViewModel()
        {
            var countriesRecipient = App.Data.Data.Countries.Where(w => w.Name != App.Data.Data.Customer.CountryName).ToList();
            DocTypeSender = DocTypes[0];
            CountryRecipient = countriesRecipient[0];
            Boxes = new ObservableCollection<Boxes> {new Boxes {Number = 1}};
            CountriesRecipient = new ObservableCollection<Countries>(countriesRecipient);
        }
    }
}
