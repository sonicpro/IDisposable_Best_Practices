using System;

namespace Sixeyed.Disposable.DomainConsoleApp.Interfaces
{
    public interface IBookFeedRunner : IDisposable
    {
        void Start();
    }
}