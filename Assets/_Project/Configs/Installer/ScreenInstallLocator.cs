using UnityEngine;

namespace Project.Config.Installer
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(ScreenInstallLocator), menuName = "Game/GameInstaller/"+nameof(ScreenInstallLocator), order = 0)]
    public class ScreenInstallLocator : ScriptableObject
    {
        public GameObject LoadingOverlay;
        public GameObject DummyScreen;
        public GameObject HomeScreen;
        public GameObject SettingsScreen;
    }
}