using System;
using System.Collections.Generic;
using System.Linq;
using Project.Application.Ports;
using Project.Application.Ports.ServiceLocator;
using Project.Application.Ports.Services;
using Project.Infrastructure.Base;
using Project.Infrastructure.Console;
using Project.Infrastructure.GameTime;
using Project.Infrastructure.Services;

namespace Project.Infrastructure.ServiceLocator
{
    public class ServiceLocator : IServiceLocator
    {
        public IGameTime GameTime { get; } = new UnityGameTime();
        public IConsole Console { get; } = new UnityConsole();
        public ISceneManagement SceneManagement { get; } = new UnitySceneManagement();

        
        public IUserInterfaceService UserInterface => TryGetService<IUserInterfaceService>();
        public IStorageService StorageService => TryGetService<IStorageService>();
        public IGameDesignService GameDesignService => TryGetService<IGameDesignService>();

        private Dictionary<Type, IService> _services = new();

        public void AddService<T>(T service) where T : IService
        {
            var serviceType = service.GetType();

            // If T is a concrete class, find its IService interface
            if (serviceType.IsClass)
            {
                var interfaceType = serviceType.GetInterfaces()
                    .FirstOrDefault(i => typeof(IService).IsAssignableFrom(i) && i != typeof(IService));

                if (interfaceType != null)
                {
                    _services.TryAdd(interfaceType, service);
                    return;
                }
            }

            // Fallback to using T directly
            _services.TryAdd(typeof(T), service);
        }

        private T TryGetService<T>() where T : IService
        {
            if (_services.TryGetValue(typeof(T), out IService foundService))
                return (T)foundService;
            else
                return default;
        }

        public bool RemoveService(Type service)
        {
            _services.Remove(service);
            return true;
        }
    }
}