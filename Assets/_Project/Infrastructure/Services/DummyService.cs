using Cysharp.Threading.Tasks;
using Project.Infrastructure.Base;
using UnityEngine;

namespace Project.Infrastructure.Services
{
    public class DummyService : BaseService
    {
        protected override async UniTask InitializeService()
        {
            await UniTask.Yield();
        }
    }
}