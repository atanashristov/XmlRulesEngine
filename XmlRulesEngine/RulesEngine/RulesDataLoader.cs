using System.IO;
using System.Xml.Serialization;

namespace XmlRulesEngine.RulesEngine
{
    public class RulesDataLoader
    {
        public RulesData.RulesData LoadFromXmlFile(string xmlFileName)
        {
            xmlFileName = xmlFileName ?? string.Empty;
            RulesData.RulesData result = null;

            if (File.Exists(xmlFileName))
            {
                var serializer = new XmlSerializer(typeof(RulesData.RulesData));
                using (TextReader reader = new StreamReader(xmlFileName))
                {
                    result = (RulesData.RulesData)serializer.Deserialize(reader);
                }
            }

            return result;
        }

        public RulesData.RulesData LoadFromXmlString(string xmlString)
        {
            var result = XmlSerializationHelpers.FromXml<RulesData.RulesData>(xmlString);
            return result;
        }

        public void WriteToFile(RulesData.RulesData rulesData, string xmlFileName)
        {
            xmlFileName = xmlFileName ?? string.Empty;
            if (string.IsNullOrEmpty(xmlFileName))
                return;

            var serializer = new XmlSerializer(typeof(RulesData.RulesData));
            using (TextWriter writer = new StreamWriter(xmlFileName))
            {
                serializer.Serialize(writer, rulesData);
            }
        }

        public string WriteToString(RulesData.RulesData rulesData)
        {
            string result = XmlSerializationHelpers.ToXml(rulesData);
            return result;
        }

    }
}
