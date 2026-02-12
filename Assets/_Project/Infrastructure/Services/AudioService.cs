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
        protected override async UniTask InitializeService()
        {
            
        }

        public void BindDependencies(AudioMixer audioMixer)
        {
            _audioMixer = audioMixer;
        }

        public void MuteAll()
        {
            _audioMixer.SetFloat("MasterVolume", Mathf.NegativeInfinity);
        }

        public void UnmuteAll()
        {
            _audioMixer.SetFloat("MasterVolume", 0);
        }

        public void ToggleMusic(bool isMuted)
        {
            _audioMixer.SetFloat("MusicVolume", isMuted ? Mathf.NegativeInfinity : 0);
        }

        public void ToggleSFX(bool isMuted)
        {
            _audioMixer.SetFloat("SfxVolume", isMuted ? Mathf.NegativeInfinity : 0);
        }
    }
}