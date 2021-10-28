using System;
using System.Collections.Generic;
using Wms.Models;
using System.Linq;
using Wms.API.Models;
using DevExpress.Mvvm;
using System.Windows.Media;
using System.Windows.Input;
using System.ComponentModel;
using DevExpress.Mvvm.Native;
using System.Collections.ObjectModel;
using Wms.UnitOfWorkAPI.Contract;

namespace Wms.ViewModel.Page
{
    public class PackageViewModel: BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;

        private DocType _docTypeSender;
        public DocType DocTypeSender
        {
            get => _docTypeSender;
            set
            {
                Sender.DocTypeId = value.Id;
                Set(nameof(DocTypeSender), ref _docTypeSender, value);
            }
        }

        public  DocType DocTypeRecipient
        {
            get
            {
                var docTypeRecipient  = App.Data.Data.DocType[1];
                Recipient.DocTypeId = docTypeRecipient.Id;
                return docTypeRecipient;
            }
        }

        private Countries _countryRecipient;
        public Countries CountryRecipient
        {
            get => _countryRecipient;
            set
            {
                Recipient.CountryCode = value.CountryCode;
                Set(nameof(CountryRecipient), ref _countryRecipient, value);
            }
        }

        private BindingList<Boxes> _boxes;
        public BindingList<Boxes> Boxes
        {
            get => _boxes;
            set => Set(nameof(Boxes), ref _boxes, value);
        }

        private Sender _sender;
        public Sender Sender
        {
            get => _sender??=new Sender();
            set => Set(nameof(Sender), ref _sender, value);
        }

        private Sender _recipient;
        public Sender Recipient
        {
            get => _recipient??=new Sender();
            set => Set(nameof(Recipient), ref _recipient, value);
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
            RemoveItemBindingList(box, Boxes);
        });

        private ICommand _removeContentCommand;
        public ICommand RemoveContentCommand => _removeContentCommand ??= new DelegateCommand<Content>((content) =>
        {
            RemoveItemBindingList(content, Contents);
        });

        private ICommand _addContentCommand;
        public ICommand AddContentCommand => _addContentCommand ??= new DelegateCommand<KeyEventArgs>((key) =>
        {
            var item = Contents.LastOrDefault();
            if (item != null && key.Key == Key.Down && item.Price != null && item.Price > 0 && item.Count != null && item.Count > 0 && item.Ht!=null)
                Contents.Add(new Content { Number = Contents.Count + 1 });
        });


        private ObservableCollection<string> _docNumSenders;
        public ObservableCollection<string> DocNumSenders
        {
            get => _docNumSenders;
            private set => Set(nameof(DocNumSenders), ref _docNumSenders, value);
        }

        //private string _searchDocNumSender;
        //public string SearchDocNumSender
        //{
        //    get => _searchDocNumSender;
        //    set
        //    {
        //        Set(nameof(SearchDocNumSender), ref _searchDocNumSender, value);

        //        //var test =  _unitOfWork.PackageRepository.FindCustomerInfoAsync<Sender>("recipient", Sender.DocTypeId, Sender.DocId).Result;

        //        //DocNumSenders = new ObservableCollection<string>(test.Data);
        //    }
        //}

        private ICommand _searchCustomersDocs;
        public ICommand SearchCustomersDocs => _searchCustomersDocs??=new AsyncCommand(async () =>
        {
            var documents = await _unitOfWork.PackageRepository.FindCustomerInfoAsync<List<string>>("recipientDocIdVariants", 2, "AN12437890");
            DocNumSenders = new ObservableCollection<string>(documents.Data);
        });



        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ??= new AsyncCommand(async () =>
        {
            //Sender.Validate();
            //Recipient.Validate();
            //var test = await _unitOfWork.PackageRepository.FindCustomerInfoAsync<Sender>("recipient", Sender.DocTypeId, Sender.DocId); ТЕСТ
            //var test2 = await _unitOfWork.PackageRepository.FindCustomerInfoAsync<List<string>>("recipientDocIdVariants", 2, "AN12437890"); ТЕСТ

        });

        private static void RemoveItemBindingList<T>(T item, BindingList<T> bindingList) where  T: BoxesBase
        {
            var index = 1;
            if (bindingList.Count > 1)
                bindingList.Remove(item);

            bindingList.ForEach(f =>
            {
                f.Number = index;
                index++;
            });
        }

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

        private BindingList<Content> _contents;
        public BindingList<Content> Contents
        {
            get => _contents;
            set => Set(nameof(Contents), ref _contents, value);
        }

        private bool _isEnabledAddressSender;
        public bool IsEnabledAddressSender
        {
            get => _isEnabledAddressSender;
            private set => Set(nameof(IsEnabledAddressSender), ref _isEnabledAddressSender, value);
        }

        private bool _isEnabledAddressRecipient;
        public bool IsEnabledAddressRecipient
        {
            get => _isEnabledAddressRecipient;
            private set => Set(nameof(IsEnabledAddressRecipient), ref _isEnabledAddressRecipient, value);
        }

        public  ObservableCollection<Countries> CountriesRecipient { get;  }
        public static ObservableCollection<Ht> Hts => new ObservableCollection<Ht>(App.Data.Data.Hts);

        public static  ObservableCollection<DocType> DocTypes => new ObservableCollection<DocType>(App.Data.Data.DocType);

        public  Countries CountrySender
        {
            get
            {
                var country = CountriesSender.FirstOrDefault(f => f.Name == App.Data.Data.Customer.CountryName);
                Sender.CountryCode = country?.CountryCode;
                return country;
            }
        }
        public static ObservableCollection<Countries> CountriesSender => new ObservableCollection<Countries>(App.Data.Data.Countries);

        public PackageViewModel(IUnitOfWork unitOfWork)
        {
            var countriesRecipient = App.Data.Data.Countries.Where(w => w.Name != App.Data.Data.Customer.CountryName).ToList();
            DocTypeSender = DocTypes[0];
            CountryRecipient = countriesRecipient[0];
            Boxes = new BindingList<Boxes> {new Boxes {Number = 1}};
            Contents = new BindingList<Content>{new Content{Number = 1, Ht = Hts.FirstOrDefault()}};

            Boxes.ListChanged += ListChanged;
            CountriesRecipient = new ObservableCollection<Countries>(countriesRecipient);
            _unitOfWork = unitOfWork;
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
