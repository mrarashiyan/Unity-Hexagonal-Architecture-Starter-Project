using Cysharp.Threading.Tasks;

namespace Project.Application.UseCases.Level
{
    public interface ILoadLevelUseCase
    {
        UniTask Execute(int levelIndex);
    }
}