using System.Xml.Serialization;

namespace XmlRulesEngine.RulesData
{
    public class RuleResultsCollection : BaseCollection<RuleResult>
    {
        [XmlElement("conditions")]
        public RuleConditionsCollection Conditions { get; set; }

        [XmlElement("results")]
        public RuleResultsCollection[] Results { get; set; }
    }
}
