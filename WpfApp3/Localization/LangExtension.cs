using System.Windows.Data;

namespace Wms.Localization
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
