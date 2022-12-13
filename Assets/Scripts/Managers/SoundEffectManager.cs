using System;
using UnityEngine;

namespace Managers
{
    public class SoundEffectManager : MonoBehaviour
    {
        #region --- Serialize Fields ---

        [SerializeField] private Sound[] sounds;

        #endregion Serialize Fields
    
    
        #region --- Mono Methods ---

        private void Start()
        {
            foreach (Sound sound in sounds)
            {
                sound.audioSource = gameObject.AddComponent<AudioSource>();
                sound.audioSource.clip = sound.clip;
                sound.audioSource.loop = sound.shouldLoop;
            }
        }

        #endregion Mono Methods


        #region --- Public Methods ---

        public void PlaySound(string soundName, float volume = 1)
        {
            foreach (Sound sound in sounds)
            {
                if (sound.name == soundName)
                {
                    sound.audioSource.volume = volume;
                    sound.audioSource.Play();
                }
            }
        }

        public void StopSound(string soundName)
        {
            foreach (Sound sound in sounds)
            {
                if (sound.name == soundName)
                {
                    sound.audioSource.Stop();
                }
            }
        }

        #endregion Public Methods


        #region --- Internal Classes ---

        [Serializable]
        public class Sound
        {
            public string name;
            public AudioClip clip;
            public bool shouldLoop;
            public AudioSource audioSource;
        }

        #endregion Internal Classes
    }
}