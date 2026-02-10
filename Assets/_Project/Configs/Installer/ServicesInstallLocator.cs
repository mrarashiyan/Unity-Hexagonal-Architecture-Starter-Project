using UnityEngine;

namespace Project.Config.Installer
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(ServicesInstallLocator), menuName = "Game/GameInstaller/"+nameof(ServicesInstallLocator), order = 0)]
    public class ServicesInstallLocator : ScriptableObject
    {
        public GameObject DummyInstaller;
        public GameObject StorageInstaller;
    }
}