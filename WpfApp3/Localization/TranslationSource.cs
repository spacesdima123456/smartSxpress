using System.Resources;
using WpfApp3.ViewModel;
using System.Globalization;

namespace WpfApp3.Localization
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
