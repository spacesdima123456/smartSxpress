using System;
using Wms.Models;
using System.Linq;
using Wms.API.Models;
using DevExpress.Mvvm;
using System.Windows.Media;
using System.Windows.Input;
using System.ComponentModel;
using DevExpress.Mvvm.Native;
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

        private BindingList<Boxes> _boxes;
        public BindingList<Boxes> Boxes
        {
            get => _boxes;
            set => Set(nameof(Boxes), ref _boxes, value);
        }

        private ICommand _addBoxCommand;
        public ICommand AddBoxCommand => _addBoxCommand??=new DelegateCommand(() =>
        {
            var item = Boxes.LastOrDefault();
            var boxes = new Boxes {Number = Boxes.Max(m => m.Number) + 1};
            if (item!=null && item.Height > 0 && item.Length > 0 && item.Weight > 0 && item.Width > 0)
                Boxes.Add(boxes);
        });

        private ICommand _removeBoxCommand;
        public ICommand RemoveBoxCommand => _removeBoxCommand ??= new DelegateCommand<Boxes>((box) =>
        {
            var index = 1;
            if (Boxes.Count>1)
                Boxes.Remove(box);

            Boxes.ForEach(f=> { 
                f.Number = index;
                index++;
            });

        });

        private double? _physicalWeight;
        public double? PhysicalWeight
        {
            get => _physicalWeight;
            private set => Set(nameof(PhysicalWeight), ref _physicalWeight, value);
        }

        private double? _volumetricWeight;
        public double? VolumetricWeight
        {
            get => _volumetricWeight;
            private set => Set(nameof(VolumetricWeight), ref _volumetricWeight, value);
        }

        private Brush _physWeightColor;
        public Brush PhysWeightColor
        {
            get => _physWeightColor;
            private set => Set(nameof(PhysWeightColor), ref _physWeightColor, value);
        }

        private Brush _volumWeightColor;
        public Brush VolumWeightColor
        {
            get => _volumWeightColor;
            private set => Set(nameof(VolumWeightColor), ref _volumWeightColor, value);
        }

        public  ObservableCollection<Countries> CountriesRecipient { get;  }

        public static  ObservableCollection<DocType> DocTypes => new ObservableCollection<DocType>(App.Data.Data.DocType);
        public static Countries CountrySender => CountriesSender.FirstOrDefault(f => f.Name == App.Data.Data.Customer.CountryName);
        public static ObservableCollection<Countries> CountriesSender => new ObservableCollection<Countries>(App.Data.Data.Countries);
        public PackageViewModel()
        {
            var countriesRecipient = App.Data.Data.Countries.Where(w => w.Name != App.Data.Data.Customer.CountryName).ToList();
            DocTypeSender = DocTypes[0];
            CountryRecipient = countriesRecipient[0];
            Boxes = new BindingList<Boxes> {new Boxes {Number = 1}};
            Boxes.ListChanged += ListChanged;
            CountriesRecipient = new ObservableCollection<Countries>(countriesRecipient);
        }

        private void ListChanged(object sender, ListChangedEventArgs e)
        {
            CalcWeight();
            SetWeightColor();
        }

        public void CalcWeight()
        {
            CalcPhysicalWeight();
            CalcVolumetricWeight();
        }

        private  void CalcPhysicalWeight()
        {
            PhysicalWeight = Math.Round(Convert.ToDouble(Boxes.Sum(s => s.Weight)));
        }

        private void CalcVolumetricWeight()
        {
            VolumetricWeight = Math.Round(Convert.ToDouble(Boxes.Select(s => (s.Height * s.Length * s.Width) / 6000).Sum()), 2);
        }

        private  void SetWeightColor()
        {
            VolumWeightColor = Brushes.Black;
            PhysWeightColor = Brushes.Black;

            if (PhysicalWeight > VolumetricWeight)
                PhysWeightColor = Brushes.Green;

            if (PhysicalWeight < VolumetricWeight)
                VolumWeightColor = Brushes.Red;
        }
    }
}
