using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    public AudioSource bgmAudioSource; 
        public Slider volumeSlider;
    
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    
        void Start()
        {
            if (volumeSlider != null && bgmAudioSource != null)
            {
                bgmAudioSource.volume = volumeSlider.value;
            }
        }
    
        public void SetVolume(float volume)
        {
            if (bgmAudioSource != null)
            {
                bgmAudioSource.volume = volume;
            }
        }
}
