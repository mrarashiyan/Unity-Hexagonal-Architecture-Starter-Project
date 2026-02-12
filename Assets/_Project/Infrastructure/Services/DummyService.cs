using Cysharp.Threading.Tasks;
using Project.Application.Ports.Services;
using Project.Infrastructure.Base;
using UnityEngine;

namespace Project.Infrastructure.Services
{
    public class DummyService : BaseService,IDummyService
    {
        protected override async UniTask InitializeService()
        {
            await UniTask.Yield();
        }
    }
}