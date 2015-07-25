using XmlRulesEngine.ElementValidators;
using XmlRulesEngine.Misc;
using XmlRulesEngine.RulesData;
using System.Collections.Generic;
using System.Linq;

namespace XmlRulesEngine.RulesEngine
{
    public class RulesEngine : IRulesEngine
    {

        private RulesData.RulesData _rulesData;
        private readonly RulesDataLoader _rulesDataLoader;
        private readonly IEnumerable<BaseElementValidator> _validators;

        private readonly ConditionEvaluatorFactory _evaluators;

        private RulesEngine()
        {
            _rulesData = new RulesData.RulesData();
            _rulesDataLoader = new RulesDataLoader();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RulesEngine"/> class.
        /// </summary>
        /// <param name="evaluators">Functor to resolve all evaluators.</param>
        /// <example>
        /// This is an example of using the rules engine only one condition evaluator, which is the included example <see cref="BooleanCondition"/>.
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
        public RulesEngine(ConditionEvaluatorFactory evaluators)
            : this(evaluators, new TimeMachine())
        { }

        internal RulesEngine(ConditionEvaluatorFactory evaluators, TimeMachine timeMachine)
            : this()
        {
            _evaluators = evaluators;

            _validators = new List<BaseElementValidator>
            {
                new IsElementActiveEvaluator(timeMachine),
            };
        }

        #region IRulesEngine implementation

        public virtual void LoadRulesFromXmlFile(string xmlFileName)
        {
            _rulesData = _rulesDataLoader.LoadFromXmlFile(xmlFileName);
        }

        public virtual void LoadRulesFromXmlString(string xmlString)
        {
            _rulesData = _rulesDataLoader.LoadFromXmlString(xmlString);
        }

        public virtual void LoadRulesFromRulesObject(RulesData.RulesData rulesData)
        {
            _rulesData = rulesData;
        }

        public virtual RuleResultsCollection Evaluate()
        {
            var result = new RuleResultsCollection();

            if (!_validators.IsValid(_rulesData))
                return result;

            var filters = new ResultsFilters(_validators);
            filters.MergeActiveResults(_rulesData.DefaultResults, result, Evaluate);

            if (!_validators.IsValid(_rulesData.Rules))
                return result;

            foreach (var rule in _rulesData.Rules.Data)
            {
                if (!_validators.IsValid(rule))
                    continue;

                if (!_validators.IsValid(rule.Conditions))
                    continue;

                var success = Evaluate(rule.Conditions);

                if (success)
                {
                    filters.MergeActiveResults(rule.OnSuccessResults, result, Evaluate);
                    if (!rule.ContinueOnSuccess)
                        break;
                }
                else
                {
                    filters.MergeActiveResults(rule.OnFailureResults, result, Evaluate);
                }
            }

            return result;
        }

        #endregion // IRulesEngine implementation



        #region Evaluate helpers

        private bool Evaluate(RuleCondition ruleCondition)
        {
            var result = _evaluators.Evaluate(ruleCondition);
            return result;
        }

        private bool Evaluate(RuleConditionsCollection conditions)
        {
            var result = conditions.Or ? EvaluateWithOr(conditions) : EvaluateWithAnd(conditions);
            return result;
        }

        private bool EvaluateWithOr(RuleConditionsCollection conditions)
        {
            if (conditions.Data != null && conditions.Data.Any())
            {
                if (conditions.Data.Where(condition => _validators.IsValid(condition))
                    .Any(Evaluate))
                {
                    return true;
                }
            }

            if (conditions.Conditions != null && conditions.Conditions.Any())
            {
                var result = false;
                foreach (RuleConditionsCollection conditioncollection in conditions.Conditions)
                {
                    if (_validators.IsValid(conditioncollection))
                        result = result || Evaluate(conditioncollection);

                    if (result)
                        return true;
                }
            }

            return false;
        }


        private bool EvaluateWithAnd(RuleConditionsCollection conditions)
        {
            if (conditions.Data != null && conditions.Data.Any())
            {
                if (conditions.Data.Where(condition => _validators.IsValid(condition))
                    .Any(condition => !Evaluate(condition)))
                {
                    return false;
                }
            }

            if (conditions.Conditions != null && conditions.Conditions.Any())
            {
                var result = true;
                foreach (RuleConditionsCollection conditioncollection in conditions.Conditions)
                {
                    if (_validators.IsValid(conditioncollection))
                        result = result && Evaluate(conditioncollection);

                    if (!result)
                        return false;
                }
            }

            return true;
        }


        #endregion // Evaluate helpers

    }
}
