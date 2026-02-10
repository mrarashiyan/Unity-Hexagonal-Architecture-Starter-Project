using System;
using Cysharp.Threading.Tasks;
using Project.Infrastructure.Enums;
using UnityEngine;

namespace Project.Infrastructure.Base
{
    public abstract class BaseService : MonoBehaviour
    {
        public ServiceInitializeStatus ServiceInitializeStatus { get; protected set; }

        public async UniTask Initialize()
        {
            ServiceInitializeStatus = ServiceInitializeStatus.InProgress;

            try
            {
                await InitializeService();
            }
            catch (Exception e)
            {
                Debug.LogError($"[{GetType().Name}] {e.Message}");
                ServiceInitializeStatus = ServiceInitializeStatus.Failed;
            }

            ServiceInitializeStatus = ServiceInitializeStatus.Succeeded;
        }


        protected abstract UniTask InitializeService();
    }
}