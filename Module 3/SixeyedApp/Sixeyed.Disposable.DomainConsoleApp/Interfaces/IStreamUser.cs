using System;

namespace Sixeyed.Disposable.DomainConsoleApp.Interfaces
{
    public interface IStreamUser
    {
        void CopyFile(string sourcePath, string targetPath);
    }
}