using Project.Core.Data.GameDesignData;

namespace Project.Application.Ports.Services
{
    public interface IGameDesignService:IService
    {
        LevelData LevelData { get; }
    }
}