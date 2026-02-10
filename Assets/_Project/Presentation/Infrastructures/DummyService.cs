using Cysharp.Threading.Tasks;
using Project.Presentation.Infrastructures.Base;
using UnityEngine;

namespace Project.Presentation.Infrastructures
{
    public class DummyService : BaseService
    {
        protected override async UniTask InitializeService()
        {
            await UniTask.Yield();
        }
    }
}