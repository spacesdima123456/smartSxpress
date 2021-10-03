namespace Wms.Models
{
    public class Content: BoxesBase
    {
        private string _htsCode;
        public string HtsCode
        {
            get => _htsCode;
            set => Set(nameof(HtsCode), ref _htsCode, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(nameof(Name), ref _name, value);
        }

        private int? _count;
        public int? Count
        {
            get => _count;
            set
            {
                Set(nameof(Count), ref _count, value);
                CalcTotal();
            }
        }

        private double? _price;
        public double? Price
        {
            get => _price;
            set
            {
                Set(nameof(Price), ref _price, value);
                CalcTotal();
            }
        }

        private double? _total;
        public double? Total
        {
            get => _total;
            private set => Set(nameof(Total), ref _total, value);
        }

        private void CalcTotal()
        {
            Total = Count * Price;
        }
    }
}
