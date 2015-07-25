using System.Xml.Serialization;

namespace XmlRulesEngine.RulesData
{
    public class Rule : BaseElement
    {
        [XmlAttribute("continue-on-success")]
        public bool ContinueOnSuccess { get; set; }

        [XmlIgnore]
        public bool ContinueOnSuccessSpecified { get { return ContinueOnSuccess; } }


        [XmlElement("conditions")]
        public RuleConditionsCollection Conditions { get; set; }


        [XmlElement("on-success-results")]
        public RuleResultsCollection OnSuccessResults { get; set; }

        [XmlIgnore]
        public bool OnSuccessResultsSpecified { get { return OnSuccessResults != null && OnSuccessResults.Data.Count > 0; } }


        [XmlElement("on-failure-results")]
        public RuleResultsCollection OnFailureResults { get; set; }

        [XmlIgnore]
        public bool OnFailureResultsSpecified { get { return OnFailureResults != null && OnFailureResults.Data.Count > 0; } }


        public Rule()
        {
            Conditions = new RuleConditionsCollection();
            OnSuccessResults = new RuleResultsCollection();
            OnFailureResults = new RuleResultsCollection();
        }
    }
}
