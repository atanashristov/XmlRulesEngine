using XmlRulesEngine.ElementValidators;
using XmlRulesEngine.RulesData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XmlRulesEngine.RulesEngine
{
    public class ResultsFilters
    {
        private readonly IEnumerable<BaseElementValidator> _validators;


        public ResultsFilters(IEnumerable<BaseElementValidator> validators)
        {
            _validators = validators;
        }

        public IEnumerable<RuleResult> GetActiveResults(RuleResultsCollection fromCollection)
        {
            if (fromCollection == null
                || fromCollection.Data == null)
                return new List<RuleResult>();

            if (!_validators.IsValid(fromCollection))
                return new List<RuleResult>();

            return fromCollection.Data.Where(ruleResult => _validators.IsValid(ruleResult));
        }

        public void MergeActiveResults(RuleResultsCollection fromCollection, RuleResultsCollection toCollection, Predicate<RuleConditionsCollection> rulesEvaluator)
        {
            if (fromCollection == null || fromCollection.Data == null) return;
            if (toCollection == null || toCollection.Data == null) return;
            if (!_validators.IsValid(fromCollection)) return;

            if (fromCollection.Conditions != null
                && rulesEvaluator != null
                && !rulesEvaluator(fromCollection.Conditions))
            {
                return;
            }

            var activeResults = GetActiveResults(fromCollection).ToList();
            foreach (var activeResult in activeResults)
            {
                toCollection.Data.RemoveAll(d => d.Name == activeResult.Name);
                toCollection.Data.Add(activeResult);
            }

            if (fromCollection.Results != null)
            {
                foreach (var innerResults in fromCollection.Results)
                    MergeActiveResults(innerResults, toCollection, rulesEvaluator);
            }
        }

    }
}
