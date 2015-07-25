using System.Linq;
using System.Xml.Serialization;

namespace XmlRulesEngine.RulesData
{
    [XmlRoot("conditions")]
    public class RuleConditionsCollection : BaseCollection<RuleCondition>
    {
        [XmlAttribute("or")]
        public bool Or { get; set; }

        [XmlIgnore]
        public bool OrSpecified { get { return Or; } }


        [XmlElement("conditions")]
        public RuleConditionsCollection[] Conditions { get; set; }

        [XmlIgnore]
        public bool ConditionsSpecified { get { return Conditions != null && Conditions.Any(); } }



        public RuleConditionsCollection()
            : this(false)
        { }

        public RuleConditionsCollection(bool or)
        {
            Or = or;
        }

    }
}
