using System.Xml.Serialization;

namespace XmlRulesEngine.RulesData
{
    [XmlRoot("rules-data")]
    public class RulesData : BaseElement
    {
        [XmlElement("rules")]
        public RulesCollection Rules { get; set; }

        [XmlElement("default-results")]
        public RuleResultsCollection DefaultResults { get; set; }


        public RulesData()
        {
            Rules = new RulesCollection();
            DefaultResults = new RuleResultsCollection();
        }
    }
}
