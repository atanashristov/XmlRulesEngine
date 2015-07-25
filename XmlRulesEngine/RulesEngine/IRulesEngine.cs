using XmlRulesEngine.RulesData;

namespace XmlRulesEngine.RulesEngine
{
    /// <summary>
    /// Rules engine.
    /// </summary>
    public interface IRulesEngine
    {
        /// <summary>
        /// Loads the rules from XML file.
        /// </summary>
        /// <param name="xmlFileName">Name of the XML file.</param>
        void LoadRulesFromXmlFile(string xmlFileName);

        /// <summary>
        /// Loads the rules from XML string.
        /// </summary>
        /// <param name="xmlString">The XML string.</param>
        void LoadRulesFromXmlString(string xmlString);

        /// <summary>
        /// Assign rules from rules object
        /// </summary>
        /// <param name="rulesData">The rules data.</param>
        void LoadRulesFromRulesObject(RulesData.RulesData rulesData);

        /// <summary>
        /// Evaluates the loaded rules.
        /// </summary>
        /// <returns>Results of the evaluation.</returns>
        RuleResultsCollection Evaluate();

    }
}
