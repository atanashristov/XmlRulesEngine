using XmlRulesEngine.RulesData;
using System;

namespace XmlRulesEngine.ConditionEvaluators
{
    /// <summary>
    /// Base condition evaluator for convenience.
    /// </summary>
    public abstract class BaseConditionEvaluator : IConditionEvaluator
    {
        public bool IsResponsibleFor(RuleCondition ruleCondition)
        {
            if (ruleCondition == null)
                return false;

            return ruleCondition.Type.Equals(GetType().Name, StringComparison.InvariantCultureIgnoreCase)
                   || ruleCondition.Type.Equals(GetType().FullName, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool Evaluate(RuleCondition ruleCondition)
        {
            var result = EvaluateInternal(ruleCondition);
            return ruleCondition.Not ? !result : result;
        }

        protected abstract bool EvaluateInternal(RuleCondition ruleCondition);
    }
}
