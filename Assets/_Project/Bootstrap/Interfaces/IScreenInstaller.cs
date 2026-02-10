using Project.Bootstrap.Enums;

namespace Project.Bootstrap.Interfaces
{
    public interface IScreenInstaller
    {
        public InstallStatus InstallStatus { get; }
    }
}