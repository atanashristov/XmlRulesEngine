using XmlRulesEngine.ConditionEvaluators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XmlRulesEngine.RulesEngine
{
    /// <summary>
    /// Used to map which classes should handle the different rule conditions.
    /// </summary>
    /// <example>
    /// This is an example of initializing ConditionEvaluatorFactory with resolver callback that returns only one implementation of <see cref="BooleanCondition"/>.
    /// <code><![CDATA[
    ///    var rulesEngine =
    ///        new RulesEngine.RulesEngine(
    ///            new ConditionEvaluatorFactory(() => new List<IConditionEvaluator> { new BooleanCondition() }));
    /// ]]>
    /// </code>
    /// We can directly pass IEnumerable object to the  constructor:
    /// <code><![CDATA[
    ///    var rulesEngine =
    ///        new RulesEngine.RulesEngine(
    ///            new ConditionEvaluatorFactory(new List<IConditionEvaluator> { new BooleanCondition() }));
    /// ]]>
    /// </code>
    /// </example>
    public class ConditionEvaluatorFactory
    {
        private readonly IEnumerable<IConditionEvaluator> _implementedTypes;

        public ConditionEvaluatorFactory(Func<IEnumerable<IConditionEvaluator>> resolveAllFunc)
        {
            _implementedTypes = resolveAllFunc();
        }

        public ConditionEvaluatorFactory(IEnumerable<IConditionEvaluator> implementedTypes)
        {
            _implementedTypes = implementedTypes;
        }

        public virtual bool Evaluate(RulesData.RuleCondition condition)
        {
            var evaluator = GetEvaluator(condition);
            var result = evaluator != null && evaluator.Evaluate(condition);
            return result;
        }

        protected IConditionEvaluator GetEvaluator(RulesData.RuleCondition condition)
        {
            var evaluator = _implementedTypes.FirstOrDefault(ev => ev.IsResponsibleFor(condition));
            return evaluator;
        }
    }
}
