using System.Resources;
using System.Globalization;
using Wms.ViewModel;

namespace Wms.Localization
{
    public class TranslationSource : BaseViewModel
    {
        private CultureInfo _currentCulture;
        private readonly ResourceManager _manager = new ResourceManager(typeof(Resources.Resources));

        public static TranslationSource Instance { get; }
        public CultureInfo CurrentCulture
        {
            get => _currentCulture;
            set=> Set(string.Empty, ref _currentCulture, value);
        }
        public string this[string key] => _manager.GetString(key, _currentCulture);

        static TranslationSource()
        {
            Instance = new TranslationSource();
        }
    }
}
