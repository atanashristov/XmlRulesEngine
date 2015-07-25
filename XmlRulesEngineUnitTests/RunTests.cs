using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XmlRulesEngine.ConditionEvaluators;
using XmlRulesEngine.RulesData;
using XmlRulesEngine.RulesEngine;
using SUT = XmlRulesEngine.RulesEngine.RulesEngine;

namespace XmlRulesEngineUnitTests
{
    [TestFixture]
    public class RunTests
    {
        [Test]
        public void Run()
        {
            var sut =
                new SUT(new ConditionEvaluatorFactory(new List<IConditionEvaluator> { new BooleanCondition() }));

            var dir = Directory.GetCurrentDirectory() + "\\TestData";
            Console.WriteLine("Running tests from: {0} ...", dir);
            Console.WriteLine();

            var fileNames = Directory.GetFiles(dir, "*.xml")
                .OrderBy(s => s)
                .ToArray();

            foreach (var fileName in fileNames)
            {
                Console.WriteLine("File: {0}", Path.GetFileName(fileName));
                Console.WriteLine("---------------");

                sut.LoadRulesFromXmlFile(fileName);
                var results = sut.Evaluate();

                var given = results.Data.FirstOrDefault(v => v.Name.ToLower() == "given");
                var when = results.Data.FirstOrDefault(v => v.Name.ToLower() == "when");
                var then = results.Data.FirstOrDefault(v => v.Name.ToLower() == "then");
                var expected = results.Data.FirstOrDefault(v => v.Name.ToLower() == "expected");
                var actual = results.Data.FirstOrDefault(v => v.Name.ToLower() == "result");

                AssertNotNull(given, when, then, expected, actual);
                Console.WriteLine(" " + given.Value);
                Console.WriteLine(" " + when.Value);
                Console.WriteLine(" " + then.Value);

                Console.WriteLine("---------------");
                Console.WriteLine("    Expected: {0}", expected.Value);
                Console.WriteLine("    Actual  : {0}", actual.Value);
                Assert.AreEqual(expected.Value, actual.Value);
                Console.WriteLine("Ok.");
                Console.WriteLine();
            }
        }

        private void AssertNotNull(params RuleResult[] ruleResults)
        {
            foreach (var ruleResult in ruleResults)
            {
                Assert.NotNull(ruleResult);
                Assert.NotNull(ruleResult.Value);
            }
        }

    }
}
