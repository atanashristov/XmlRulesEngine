using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XmlRulesEngine.RulesEngine
{
    public static class XmlSerializationHelpers
    {
        public static T FromXml<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(xml ?? string.Empty))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public static string ToXml<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            var sb = new StringBuilder();
            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, obj);
            }
            return sb.ToString();
        }
    }
}
