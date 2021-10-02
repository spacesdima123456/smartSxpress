using Wms.ViewModel;

namespace Wms.Models
{
    public class Boxes: BaseViewModel
    {
        //public int Number { get; set; }
        //public double? Length { get; set; }
        //public double? Width { get; set; }
        //public double? Height { get; set; }
        //public double? Weight { get; set; }
        private int _number;
        public int Number
        {
            get => _number;
            set => Set(nameof(Number), ref _number, value);
        }

        private double? _length;
        public double? Length
        {
            get => _length;
            set => Set(nameof(Length), ref _length, value);
        }

        private double? _width;
        public double? Width
        {
            get => _width;
            set => Set(nameof(Width), ref _width, value);
        }

        private double? _height;
        public double? Height
        {
            get => _height;
            set => Set(nameof(Height), ref _height, value);
        }

        private double? _weight;
        public double? Weight
        {
            get => _weight;
            set => Set(nameof(Weight), ref _weight, value);
        }
    }
}
