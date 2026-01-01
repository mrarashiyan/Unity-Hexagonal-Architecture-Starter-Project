using System;
using Project.Core.Enums;

namespace Project.Application.Ports.Persistance.LocalStorage.Interface
{
    public interface ILocalStorage
    {
        public IUserStorage User { get; set; }
    }
}