using System.Windows.Data;

namespace WpfApp3.Localization
{
    public class LangExtension : Binding
    {
        public LangExtension(string name) : base("[" + name + "]")
        {
            Mode = BindingMode.OneWay;
            Source = TranslationSource.Instance;
        }
    }
}
