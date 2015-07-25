using System.Xml.Serialization;

namespace XmlRulesEngine.RulesData
{
    [XmlRoot("conditions")]
    public class RuleCondition : BaseElement
    {

        [XmlAttribute("type")]
        public string Type { get; set; }


        [XmlAttribute("value")]
        public string Value { get; set; }
        [XmlIgnore]
        public bool ValueSpecified { get { return !string.IsNullOrEmpty((Value ?? string.Empty).Trim()); } }


        [XmlAttribute("gt")]
        public string GreaterThan { get; set; }
        [XmlIgnore]
        public bool GreaterThanSpecified { get { return !string.IsNullOrEmpty((GreaterThan ?? string.Empty).Trim()); } }


        [XmlAttribute("lt")]
        public string LessThan { get; set; }
        [XmlIgnore]
        public bool LessThanSpecified { get { return !string.IsNullOrEmpty((LessThan ?? string.Empty).Trim()); } }


        [XmlAttribute("not")]
        public bool Not { get; set; }
        [XmlIgnore]
        public bool NotSpecified { get { return Not; } }

    }
}
