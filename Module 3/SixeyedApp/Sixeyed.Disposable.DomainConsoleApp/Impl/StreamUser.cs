using Sixeyed.Disposable.DomainConsoleApp.Interfaces;
using System.IO;

namespace Sixeyed.Disposable.DomainConsoleApp.Impl
{
    class StreamUser : IStreamUser
    {
        public void CopyFile(string sourcePath, string targetPath)
        {
            var inputStream = File.OpenRead(sourcePath);
            var outputStream = File.Create(targetPath);
            inputStream.CopyTo(outputStream);
        }
    }
}
