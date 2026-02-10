using Project.Bootstrap.Enums;

namespace Project.Bootstrap.Interfaces
{
    public interface IServiceInstaller
    {
        public bool TimeoutCanBlockBoot { get; }
        public InstallStatus InstallStatus { get; }
        
    }
}