using Gameplay.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public Sound[] sounds;

        void Awake()
        {
            Instance = this;

            foreach(Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.loop = s.loop;
                s.source.pitch = s.pitch;
            }
        }

        public void PlaySound(string name)
        {
            foreach (Sound s in sounds)
            {
                if (s.name == name)
                    s.source.Play();
            }
        }

        public void PauseSound(string name)
        {
            foreach (Sound s in sounds)
            {
                if (s.name == name)
                    s.source.Pause();
            }
        }
    }
}