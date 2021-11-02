using Refit;
using System;
using Wms.Models;
using System.Linq;
using Wms.API.Models;
using System.Windows;
using System.IO.Ports;
using DevExpress.Mvvm;
using Wms.Services.ComPort;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using DevExpress.Mvvm.Native;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wms.UnitOfWorkAPI.Contract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using  static Wms.Helpers.ErrorValidation;

namespace Wms.ViewModel.Page
{
    public class PackageViewModel: BaseViewModel
    {

        private readonly IComPort _comPort;
        private readonly IUnitOfWork _unitOfWork;

        private DocType _docTypeSender;
        public DocType DocTypeSender
        {
            get => _docTypeSender;
            set
            {
                DocNumSenders?.Clear();
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

        private Brush _physWeightColor;
        public Brush PhysWeightColor
        {
            get => _physWeightColor;
            private set => Set(nameof(PhysWeightColor), ref _physWeightColor, value);
        }

        private Brush _volumeWeightColor;
        public Brush VolumeWeightColor
        {
            get => _volumeWeightColor;
            private set => Set(nameof(VolumeWeightColor), ref _volumeWeightColor, value);
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

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ??= new DelegateCommand(() =>
        {
            //Sender.Validate();
            //Recipient.Validate();
        });

        private ICommand _selectedRecipientCommand;
        public ICommand SelectedRecipientCommand => _selectedRecipientCommand ??= new AsyncCommand<string>(
            async (docNum) => 
            {
                var recipient = await _unitOfWork.PackageRepository.FindCustomerInfoAsync<Sender>("recipient", Recipient.DocTypeId, docNum);
                Recipient = recipient.Data;
            });

        private ICommand _selectedSenderCommand;
        public ICommand SelectedSenderCommand => _selectedSenderCommand ??= new AsyncCommand<string>(async (docNum) =>
            {
                var sender = await _unitOfWork.PackageRepository.FindCustomerInfoAsync<Sender>("sender", Sender.DocTypeId, docNum);
                Sender = sender.Data;
            });

        private ICommand _currentCellCommand;
        public ICommand CurrentCellCommand => _currentCellCommand??= new DelegateCommand(() =>
        {
            if (CellInfo.IsValid)
            {
                _comPort.DataReceived += DataReceived;
                _comPort.Open();
            }
            else
            {
                _comPort.DataReceived -= DataReceived;
                _comPort.Close();
            }
        });

        private DataGridCellInfo _cellInfo;
        public DataGridCellInfo CellInfo
        {
            get => _cellInfo;
            set => Set(nameof(CellInfo), ref _cellInfo, value);
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

        private int _selectedIndexBox;
        public  int SelectedIndexBox
        {
            get => _selectedIndexBox;
            set=> Set(nameof(SelectedIndexBox), ref _selectedIndexBox, value);
        }

        private BindingList<Content> _contents;
        public BindingList<Content> Contents
        {
            get => _contents;
            set => Set(nameof(Contents), ref _contents, value);
        }

        public static Visibility VisibilityCargo => Forwarders.Any() && Consignees.Any() ? Visibility.Visible : Visibility.Collapsed;

        public  ObservableCollection<Countries> CountriesRecipient { get;  }
        public static ObservableCollection<Ht> Hts => new ObservableCollection<Ht>(App.Data.Data.Hts);
        public static ObservableCollection<Cargo> Consignees => new ObservableCollection<Cargo>(App.Data.Data.Consignees);
        public static ObservableCollection<Cargo> Forwarders => new ObservableCollection<Cargo>(App.Data.Data.Forwarders);

        private ObservableCollection<string> _docNumSenders;
        public ObservableCollection<string> DocNumSenders
        {
            get => _docNumSenders;
            private set => Set(nameof(DocNumSenders), ref _docNumSenders, value);
        }

        private ObservableCollection<string> _docNumRecipients;
        public ObservableCollection<string> DocNumRecipients
        {
            get => _docNumRecipients;
            private set => Set(nameof(DocNumRecipients), ref _docNumRecipients, value);
        }

        public static  ObservableCollection<DocType> DocTypes => new ObservableCollection<DocType>(App.Data.Data.DocType);
        public static ObservableCollection<Countries> CountriesSender => new ObservableCollection<Countries>(App.Data.Data.Countries);

        public  Countries CountrySender
        {
            get
            {
                var country = CountriesSender.FirstOrDefault(f => f.Name == App.Data.Data.Customer.CountryName);
                Sender.CountryCode = country?.CountryCode;
                return country;
            }
        }

        public PackageViewModel(IUnitOfWork unitOfWork, IComPort comPort)
        {
            var countriesRecipient = App.Data.Data.Countries.Where(w => w.Name != App.Data.Data.Customer.CountryName).ToList();
            Contents = new BindingList<Content>{new Content{Number = 1, Ht = Hts.FirstOrDefault()}};
            CountriesRecipient = new ObservableCollection<Countries>(countriesRecipient);
            Boxes = new BindingList<Boxes> {new Boxes {Number = 1}};
            CountryRecipient = countriesRecipient[0];
            Boxes.ListChanged += ListChanged;
            DocTypeSender = DocTypes[0];
            _unitOfWork = unitOfWork;
            _comPort = comPort;
        }


        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (Boxes.Count > SelectedIndexBox)
            {
                var last = Boxes[SelectedIndexBox < 0 ? 0 : SelectedIndexBox];
                var data = ((SerialPort) sender).ReadExisting().Replace(".", ",");
                if (double.TryParse(data, out var weight))
                    last.Weight = weight;
            }
        }

        public async Task OnQuerySubmittedSenderAsync(string text)
        {
            await SearchVariantDocAsync(text, "senderDocIdVariants", Sender.DocTypeId, (collection, b) =>
            {
                DocNumSenders = collection;
                IsEnabledAddressSender = b != true && !string.IsNullOrWhiteSpace(text);

                if (IsEnabledAddressSender)
                    Sender.Clear();
                else
                {
                    if (!string.IsNullOrWhiteSpace(text) && collection.Any(a => a.Contains(text)))
                        SelectedSenderCommand.Execute(text);
                }
            });
        }
        public async Task OnQuerySubmittedRecipientAsync(string text)
        {
            await SearchVariantDocAsync(text, "recipientDocIdVariants", Recipient.DocTypeId, (collection, b) =>
            {
                DocNumRecipients = collection;
                IsEnabledAddressRecipient = b != true && !string.IsNullOrWhiteSpace(text);

                if (IsEnabledAddressRecipient)
                    Recipient.Clear();
                else
                {
                    if (!string.IsNullOrWhiteSpace(text) && collection.Any(a => a.Contains(text)))
                        SelectedRecipientCommand.Execute(text);
                }
            });
        }

        public void CalcWeight()
        {
            CalcPhysicalWeight();
            CalcVolumetricWeight();
        }

        private void ListChanged(object sender, ListChangedEventArgs e)
        {
            CalcWeight();
            SetWeightColor();
        }

        private async Task SearchVariantDocAsync(string text, string typeCustomer, int docTypeId, Action<ObservableCollection<string>, bool> callBack)
        {
            var immediatePopup = false;
            var data = Enumerable.Empty<string>();
            try
            {
                var documents = await _unitOfWork.PackageRepository.FindCustomerInfoAsync<List<string>>(typeCustomer, docTypeId, text);
                if (documents.Data.Any())
                {
                    data = documents.Data;
                    immediatePopup = true;
                }
            }
            catch (ApiException e)
            {
                await HandleGeneralErrorsAsync(e);
            }

            callBack(new ObservableCollection<string>(data), immediatePopup);
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
            VolumeWeightColor = Brushes.Black;
            PhysWeightColor = Brushes.Black;

            if (PhysicalWeight > VolumetricWeight)
                PhysWeightColor = Brushes.Green;

            if (PhysicalWeight < VolumetricWeight)
                VolumeWeightColor = Brushes.Red;
        }
        private static void RemoveItemBindingList<T>(T item, BindingList<T> bindingList) where T : BoxesBase
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
    }
}
