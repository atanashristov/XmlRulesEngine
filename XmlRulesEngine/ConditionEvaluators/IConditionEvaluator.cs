using XmlRulesEngine.RulesData;

namespace XmlRulesEngine.ConditionEvaluators
{
    /// <summary>
    /// All condition evaluators implement this interface.
    /// </summary>
    public interface IConditionEvaluator
    {
        /// <summary>
        /// Determines whether the concrete rule condition evaluator is responsible
        /// for handling the <param name="ruleCondition">rule condition</param>
        /// </summary>
        /// <param name="ruleCondition">The rule condition.</param>
        /// <returns>True if is responsible for handling the condition.</returns>
        bool IsResponsibleFor(RuleCondition ruleCondition);

        /// <summary>
        /// Evaluates the specified rule condition.
        /// </summary>
        /// <param name="ruleCondition">The rule condition to evaluate.</param>
        /// <param name="ctx">Context data, can be used to pass data to the rule evaluators.</param>
        /// <returns>Result of evaluation.</returns>
        bool Evaluate(RuleCondition ruleCondition);
    }
}
