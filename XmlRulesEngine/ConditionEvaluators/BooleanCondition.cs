using System;

namespace XmlRulesEngine.ConditionEvaluators
{
    /// <summary>
    /// Example of implementing simple true/false evaluation.
    /// </summary>
    public class BooleanCondition : BaseConditionEvaluator
    {
        protected override bool EvaluateInternal(RulesData.RuleCondition ruleCondition)
        {
            bool testValue;
            Boolean.TryParse(ruleCondition.Value ?? string.Empty, out testValue);
            return testValue;
        }
    }
}
