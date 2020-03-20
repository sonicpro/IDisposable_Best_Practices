using System;

namespace CheckingFinalizerExecution
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateLocalExampleClassInstance();

            // Forcing the collection. Alternatively, you can see the moment the application terminates (run this program in the command interpreter).
            GC.Collect();

            Console.ReadLine();
        }

        private static void CreateLocalExampleClassInstance()
        {
            var example = new ExampleClass();
            example.ShowDuration();
        }
    }
}
