using Wms.API.Models;

namespace Wms.Models
{
    public class Content: BoxesBase
    {
        private Ht _ht;
        public Ht Ht
        {
            get => _ht;
            set => Set(nameof(Ht), ref _ht, value);
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
