using System;
using Project.Core.Enums;

namespace Project.Application.Ports.Persistance.LocalStorage.Interface
{
    public interface ILocalStorageVariable
    {
        Func<LocalStorageKey, object,bool> SetVariableFunc { get; set; }
        Func<LocalStorageKey,Type,object,object> GetVariableFunc { get; set; }
    }
}