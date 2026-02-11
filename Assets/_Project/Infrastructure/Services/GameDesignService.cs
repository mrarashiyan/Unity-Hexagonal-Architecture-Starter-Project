using System;
using Cysharp.Threading.Tasks;
using Project.Application.Ports.Services;
using Project.Core.Data.GameDesignData;
using Project.Infrastructure.Base;

namespace Project.Infrastructure.Services
{
    public class GameDesignService : BaseService,IGameDesignService
    {
        public LevelData LevelData { get; protected set; } = new ();
        protected override async UniTask InitializeService()
        {
        }
    }
}