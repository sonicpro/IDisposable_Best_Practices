using System;

namespace GetDate.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("'g' to Get date; 'gc' to Garbage Collect; 'x' to exit");
            var command = "";
            while (command != "x")
            {
                command = Console.ReadLine();
                switch (command)
                {
                    case "g":
                        GetDate();
                        break;
                    case "gc":
                        GC.Collect();
                        break;
                }
            }
        }

        private static void GetDate()
        {
            // Making our app an "unmanaged memory hog". Chances are slim that some CG trigger starts collection in response of unmanaged heap bloating.
            // CG does not care about unmanaged resources!
            //using (UnmanagedDatabaseState databaseState = new UnmanagedDatabaseState())
            var databaseState = new UnmanagedDatabaseState();
            {
                Console.WriteLine(databaseState.GetDate());
            }
        }
    }
}
