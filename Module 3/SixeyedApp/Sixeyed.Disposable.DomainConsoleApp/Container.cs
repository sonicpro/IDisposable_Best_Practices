using Microsoft.Practices.Unity;
using Sixeyed.Disposable.DomainConsoleApp.Impl;
using Sixeyed.Disposable.DomainConsoleApp.Interfaces;

namespace Sixeyed.Disposable.DomainConsoleApp
{
    public static class Container
    {
        private static IUnityContainer _container;

       public static void Configure()
        {
            _container = new UnityContainer();
            _container.RegisterType<IBookFeedRepository, BookFeedRepository>(new TransientLifetimeManager());
            _container.RegisterType<IApiClient, ApiClient>(new TransientLifetimeManager());
            _container.RegisterType<IFileArchiver, FileArchiver>(new TransientLifetimeManager());
            _container.RegisterType<IStreamUser, StreamUser>(new TransientLifetimeManager());
            _container.RegisterType<IBookFeedRunner, BookFeedRunner>(new TransientLifetimeManager());
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}