using System;
using Cysharp.Threading.Tasks;
using Project.Application.Ports.Persistence.Storage;
using Project.Core.Enums;

namespace Project.Application.Ports.Persistence.Variables
{
    public interface IStorageVariable
    { 
        UniTask<bool> SaveVariable();
        UniTask<bool> LoadVariable();
        UniTask<bool> DeleteVariable();
    }
}