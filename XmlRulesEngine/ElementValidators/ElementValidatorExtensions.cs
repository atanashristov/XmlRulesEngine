using XmlRulesEngine.RulesData;
using System.Collections.Generic;
using System.Linq;

namespace XmlRulesEngine.ElementValidators
{
    public static class ElementValidatorExtensions
    {
        public static bool IsValid(this IEnumerable<BaseElementValidator> validators, BaseElement element)
        {
            return validators.All(ruleEvaluator => ruleEvaluator.Evaluate(element));
        }
    }
}
