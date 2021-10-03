using Wms.ViewModel;

namespace Wms.Models
{
    public class BoxesBase : BaseViewModel
    {
        private int _number;
        public int Number
        {
            get => _number;
            set => Set(nameof(Number), ref _number, value);
        }
    }
}
