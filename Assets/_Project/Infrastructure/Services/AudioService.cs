using Cysharp.Threading.Tasks;
using Project.Application.Ports.Services;
using Project.Infrastructure.Base;
using UnityEngine;
using UnityEngine.Audio;

namespace Project.Infrastructure.Services
{
    public class AudioService : BaseService, IAudioService
    {
        private AudioMixer _audioMixer;
        private const string MasterVolumeKey = "MasterVolume";
        private const string MusicVolumeKey = "MusicVolume";
        private const string SfxVolumeKey = "SfxVolume";
        protected override async UniTask InitializeService()
        {
            
        }

        public void BindDependencies(AudioMixer audioMixer)
        {
            _audioMixer = audioMixer;
        }

        public void MuteAll()
        {
            _audioMixer.SetFloat(MasterVolumeKey, Mathf.NegativeInfinity);
        }

        public void UnmuteAll()
        {
            _audioMixer.SetFloat(MasterVolumeKey, 0);
        }

        public void ToggleMusic(bool isMuted)
        {
            _audioMixer.SetFloat(MusicVolumeKey, isMuted ? -80 : 0);
        }

        public void ToggleSFX(bool isMuted)
        {
            _audioMixer.SetFloat(SfxVolumeKey, isMuted ? -80 : 0);
        }

        public bool IsMasterMute()
        {
            _audioMixer.GetFloat(MasterVolumeKey,out float value);
            return value < 0;
        }

        public bool IsMusicMute()
        {
            _audioMixer.GetFloat(MusicVolumeKey,out float value);
            return value < 0;
        }

        public bool IsSfxMute()
        {
            _audioMixer.GetFloat(SfxVolumeKey,out float value);
            return value < 0;
        }
    }
}