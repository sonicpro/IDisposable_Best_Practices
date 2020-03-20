using System;
using System.Diagnostics;

namespace CheckingFinalizerExecution
{
    class ExampleClass
    {
        private Stopwatch sw;

        public ExampleClass()
        {
            sw = Stopwatch.StartNew();
            Console.WriteLine("Instantiated object");
        }

        public void ShowDuration()
        {
            Console.WriteLine($"This instance of {this} has been in existence for {sw.Elapsed}");
        }

        ~ExampleClass()
        {
            Console.WriteLine("Finalizing object");
            sw.Stop();
            ShowDuration();
        }
    }
}
