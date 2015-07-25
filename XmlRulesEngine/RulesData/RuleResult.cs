using System.Xml.Serialization;

namespace XmlRulesEngine.RulesData
{
    public class RuleResult : BaseElement
    {
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}
