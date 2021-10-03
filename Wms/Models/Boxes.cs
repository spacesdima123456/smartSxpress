namespace Wms.Models
{
    public class Boxes: BoxesBase
    {
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
