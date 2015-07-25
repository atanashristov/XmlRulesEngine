# XmlRulesEngine

The XmlRulesEngine is a generic xml based rules engine. 

The process of evaluation takes the following steps:

- the rules engine is given an xml file with rules
- the rules engine evaluates the rules one by one
- the evaluation is calling back evaluator classes provided by the client
- by default, the first rule that evaluates as true stops the process
- normally that rule also sets variable values
- the output of the evaluation is a set of variables and their values

## Usage Example

Following is an example of rules xml file with only one rule and it utilizes one condition:

    <?xml version="1.0" encoding="utf-8" ?>
    <rules-data 
        xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
        xmlns:xsd="http://www.w3.org/2001/XMLSchema"
        name="Rules Data">
    
        <default-results>
            <add name="Variable 1" value="false" />
            <add name="Variable 2" value="false" />
        </default-results>
    
        <rules>
            <add name="Rule Name">
                <conditions>
                    <add name="Condition Name" 
                        type="BooleanCondition" value="true" />
                </conditions>
                <on-success-results>
                    <add name="Variable 1" value="true" />
                </on-success-results>
            </add>
        </rules>
    
    </rules-data>

At the top of the rules xml file in section `default-rules` we initialize variables with default values:

        <default-results>
            <add name="Variable 1" value="false" />
            <add name="Variable 2" value="false" />
        </default-results>

Following is the section `rules` which in this example contains only one rule.

It utilizes a condition of type `BooleanCondition`. All that this condition does is, it'll evaluate the `value` attribute.

Having that, the output of the evaluation will contain 2 variables:

    Variable 1: true
    Variable 2: false

For more examples of using the rules engine check the unit tests in the project.


## Integration

This section outlines the steps to integrate the rules engine.

**Step 1:** Implement ConditionEvaluationFactory, to inject list of evaluators:


    public class ConditionEvaluationFactory : ConditionEvaluatorFactory
    {
        public ConditionEvaluationFactory(
            IEnumerable<IConditionEvaluator> evaluators)
            : base(evaluators) { }
    }


**Step 2:** IoC setup for the evaluator classes:

    var pluginType = typeof (XmlRulesEngine.ConditionEvaluators.IConditionEvaluator);

    var rulesAssembly = AppDomain.CurrentDomain.GetAssemblies()
        .FirstOrDefault(a => a.FullName.StartsWith("WhateverAssemblyName"));

    if (rulesAssembly != null)
    {
        _x.Scan(scanner =>
        {
            scanner.Assembly(rulesAssembly);
            scanner.AddAllTypesOf(pluginType);
        });
    }

**Step 3:** Instantiate and run the rules engine 

    public class DiscountOfferQuery
    {
        private const string RulesFileName = "discount.engine.rules";
        private readonly ConditionEvaluationFactory _conditions;

        public DiscountOfferQuery(ConditionEvaluationFactory conditions)
        {
            _conditions = conditions;
        }

        public DiscountInfo GetDiscountInfo()
        {
            string xmlFileName = GetResourceFileName(RulesFileName);
            var loader = new ResourceConfigLoader<RDat.RulesData>(true);
            var rulesData = loader.Load(xmlFileName);
            
           var rulesEngine = new REng.RulesEngine(_conditions);
           rulesEngine.LoadRulesFromRulesObject(rulesData);

           var ruleResults = rulesEngine.Evaluate();
           var res = new DiscountInfo(ruleResults);

           return res;
        }
        …

