using System;
using System.IO;
using System.Threading;

namespace Sixeyed.Disposable.ConsoleApp
{
    public class FolderWatcher : IDisposable
    {
        // Because "Created" event handler keeps an event handler, it won't be disposed once it is
        // declared inside a function like a local variable and then goes out of scope.

        // Correct solution is to declare the FileSystemWatcher as the field of the class rather than the local variable.
        // As long as the class contains IDisposable fields, it should implement IDisposable.
        private FileSystemWatcher _fileSystemWatcher = new FileSystemWatcher();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _fileSystemWatcher != null)
            {
                _fileSystemWatcher.Dispose();
                _fileSystemWatcher = null;
            }
        }

        public void Start(string path, string filter, Action<string> onFileCreated)
        {
            _fileSystemWatcher = new FileSystemWatcher(path, filter);
            _fileSystemWatcher.Created += (x, y) =>
                {
                    //HACK - let the file write finish:
                    Thread.Sleep(1000);
                    Console.WriteLine("New file created: " + y.Name);
                    onFileCreated(y.FullPath);
                };
            _fileSystemWatcher.EnableRaisingEvents = true;
        }
    }
}
