using System;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioObject;
        [SerializeField] private SoundClip buttonClickSound;
    
        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Found more than one Audio Manager in the scene.");
                Destroy(this);
                return;
            }
            Instance = this;
        }

        public void PlaySoundFXClip(SoundClip clip, Transform spawnTransform)
        {
            if (clip == null) { return; }
        
            AudioSource audioSource = Instantiate(audioObject, spawnTransform.position, Quaternion.identity);

            audioSource.clip = clip.audioClip;
            audioSource.volume = clip.volume;
            audioSource.Play();

            float clipLength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
    
        public void PlayRandomSoundFXWithSource(AudioSource audioSource, SoundClip[] clips)
        {
            if (clips.Length == 0 || audioSource == null) { return; }
        
            int rand = UnityEngine.Random.Range(0, clips.Length);
            var clip = clips[rand];

            audioSource.clip = clip.audioClip;
            audioSource.volume = clip.volume;
            audioSource.Play();
        }

        public void PlayButtonClickSound()
        {
            PlaySoundFXClip(buttonClickSound, transform);
        }
    }
    
    [Serializable]
    public class SoundClip
    {
        public AudioClip audioClip;
        [Range(0, 1)] public float volume;
    }
}
