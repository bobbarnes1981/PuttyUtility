using System.IO;
using System.Xml.Serialization;

namespace PuttyUtility
{
    class ConfigurationManager
    {
        private string m_path;

        private XmlSerializer m_serializer;

        public ConfigurationManager(string path)
        {
            m_path = path;
            m_serializer = new XmlSerializer(typeof(Configuration));

            if (!File.Exists(m_path))
            {
                Save(Configuration.Default);
            }
        }

        public Configuration Load()
        {
            using (FileStream fs = new FileStream(m_path, FileMode.Open))
            {
                return (Configuration)m_serializer.Deserialize(fs);
            }
        }

        public void Save(Configuration configuration)
        {
            using (FileStream fs = new FileStream(m_path, FileMode.OpenOrCreate))
            {
                m_serializer.Serialize(fs, configuration);
            }
        }
    }
}
