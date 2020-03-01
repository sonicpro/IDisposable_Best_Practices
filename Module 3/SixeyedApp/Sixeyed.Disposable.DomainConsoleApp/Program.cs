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
        private static IBookFeedRunner Runner;

        static void Main(string[] args)
        {
            Container.Configure();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("'s' to Start; 'p' to pause; 'gc' to Garbage Collect; 'x' to Exit");
            var command = "";
            while (command != "x")
            {
                command = Console.ReadLine();
                switch (command)
                {
                    case "s":
                        Start();
                        break;

                    case "p":
                        Stop();
                        break;

                    case "gc":
                        GC.Collect();
                        break;
                }
            }
        }

        private static void Start()
        {
            Runner = Container.Resolve<IBookFeedRunner>();
            Runner.Start();
        }

        private static void Stop()
        {
            if (Runner != null)
            {
                Runner.Dispose();
                Runner = null;
            }
        }
    }
}
