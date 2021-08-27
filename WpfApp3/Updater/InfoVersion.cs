using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using WpfApp3.Models;
using WpfApp3.Properties;

namespace WpfApp3.Updater
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

        private static InfoVersion instance;

        public static InfoVersion Instance()
        {
            if (instance == null)
                instance = new InfoVersion();
            return instance;
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

                Url = info.Url;
                NewVersion = new Version(info.Version);
                CurVersion = Assembly.GetExecutingAssembly().GetName().Version;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
