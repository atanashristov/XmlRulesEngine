using XmlRulesEngine.Misc;
using XmlRulesEngine.RulesData;

namespace XmlRulesEngine.ElementValidators
{
    internal class IsElementActiveEvaluator : BaseElementValidator
    {
        private readonly TimeMachine _timeMachine;

        public IsElementActiveEvaluator(TimeMachine timeMachine)
        {
            _timeMachine = timeMachine;
        }

        protected override bool EvaluateInternal(BaseElement element)
        {
            if (element.Disabled)
                return false;

            if (element.StartDateTime.HasValue
                && element.StartDateTime.Value > _timeMachine.Now)
                return false;

            if (element.EndDateTime.HasValue
                && element.EndDateTime.Value < _timeMachine.Now)
                return false;

            return true;
        }
    }
}
