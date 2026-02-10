using System;
using Project.Core.Enums;

namespace Project.Application.Ports.Persistence.Storage
{
    public interface ILocalStorage : IStorage
    {
        bool SavePlayerPrefs(LocalStorageKey key, object value);
        object LoadPlayerPrefs(LocalStorageKey key, Type T, object defaultValue);
        bool DeletePlayerPrefs(LocalStorageKey key);
    }
}