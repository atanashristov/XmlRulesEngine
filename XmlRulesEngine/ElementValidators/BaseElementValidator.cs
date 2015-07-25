using XmlRulesEngine.RulesData;

namespace XmlRulesEngine.ElementValidators
{
    public abstract class BaseElementValidator
    {
        public bool Evaluate(BaseElement element)
        {
            if (element == null)
                return false;

            return EvaluateInternal(element);
        }

        protected abstract bool EvaluateInternal(BaseElement element);
    }
}
