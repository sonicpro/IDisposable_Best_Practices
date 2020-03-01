using Sixeyed.Disposable.DomainConsoleApp.Interfaces;
using System;

/// <summary>
/// This demo app is a revision of original app from Module 1 with the addition of DI and Repository pattern instead of SqlClient.
/// The app does not comply with the IDisposable Best Practices.
/// The demo shows that DDD by itself does not save from resource leaks.
/// </summary>

namespace Sixeyed.Disposable.DomainConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Container.Configure();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("'s' to Start; 'gc' to Garbage Collect; 'x' to Exit");
            var command = "";
            while (command != "x")
            {
                command = Console.ReadLine();
                switch (command)
                {
                    case "s":
                        Start();
                        break;

                    case "gc":
                        GC.Collect();
                        break;
                }
            }
        }

        private static void Start()
        {
            var runner = Container.Resolve<IBookFeedRunner>();
            runner.Start();
        }
    }
}
