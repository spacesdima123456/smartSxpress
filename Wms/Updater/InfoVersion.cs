using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using Wms.Properties;
using WpfApp3.Models;

namespace Wms.Updater
{

    public class InfoVersion
    {
        public string Url { get; private set; }
        public Version NewVersion { get; private set; }
        public Version CurVersion { get; private set; }

        private InfoVersion()
        {
            Init();
        }

        private static InfoVersion _instance;

        public static InfoVersion Instance()
        {
            if (_instance == null)
                _instance = new InfoVersion();
            return _instance;
        }

        private void Init()
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(Settings.Default.Version);
                var serializer = new XmlSerializer(typeof(Xml));
                using var reader = new StringReader(doc.InnerXml);
                var info = (Xml)serializer.Deserialize(reader);

                if (info != null)
                {
                    Url = info.Url;
                    NewVersion = new Version(info.Version);
                }

                CurVersion = Assembly.GetExecutingAssembly().GetName().Version;

            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
