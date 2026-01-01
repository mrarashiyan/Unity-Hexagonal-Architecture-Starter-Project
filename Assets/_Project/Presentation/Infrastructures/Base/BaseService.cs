using System;
using Cysharp.Threading.Tasks;
using Project.Presentation._Project.Presentation.Common.Enums;
using UnityEngine;

namespace Project.Presentation.Infrastructures.Base
{
    public abstract class BaseService : MonoBehaviour
    {
        public InitializeStatus InitializeStatus { get; protected set; }

        public async UniTask Initialize()
        {
            InitializeStatus = InitializeStatus.InProgress;

            try
            {
                await InitializeService();
            }
            catch (Exception e)
            {
                Debug.LogError($"[{GetType().Name}] {e.Message}");
                InitializeStatus = InitializeStatus.Failed;
            }

            InitializeStatus = InitializeStatus.Succeeded;
        }


        protected abstract UniTask InitializeService();
    }
}