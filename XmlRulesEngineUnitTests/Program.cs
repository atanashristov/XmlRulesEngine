using System;

namespace XmlRulesEngineUnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            new RunTests().Run();

            Console.WriteLine("OK.");

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press <Enter> to close.");
                Console.ReadLine();
            }
        }
    }
}
