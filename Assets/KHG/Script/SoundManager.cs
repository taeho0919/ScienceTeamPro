
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip[] audioClips;

    [Range(0f, 1f)]
    [SerializeField] private float masterVolume = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float bgmVolume = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float sfxVolume = 1f;

    private Dictionary<string, AudioClip> soundDict;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        AudioSource[] sources = GetComponents<AudioSource>();

        if (sources.Length >= 2)
        {
            bgmSource = sources[0];
            sfxSource = sources[1];
        }
        else
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        bgmSource.playOnAwake = false;
        bgmSource.loop = true;

        sfxSource.playOnAwake = false;
        sfxSource.loop = false;

        soundDict = new Dictionary<string, AudioClip>();

        foreach (AudioClip clip in audioClips)
        {
            if (clip != null)
            {
                soundDict[clip.name] = clip;
            }
        }

        UpdateVolume();
    }
    private void Start()
    {
        PlayBGM(audioClips[0].name);
    }

    private void UpdateVolume()
    {
        bgmSource.volume = masterVolume * bgmVolume;
        sfxSource.volume = masterVolume * sfxVolume;
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = Mathf.Clamp01(value);
        UpdateVolume();
    }

    public void SetBGMVolume(float value)
    {
        bgmVolume = Mathf.Clamp01(value);
        UpdateVolume();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        UpdateVolume();
    }

    public void PlayBGM(string soundName)
    {
        if (!soundDict.TryGetValue(soundName, out AudioClip clip))
        {
            Debug.LogWarning("배경음악을 찾을 수 없습니다: " + soundName);
            return;
        }

        if (bgmSource.clip == clip && bgmSource.isPlaying)
            return;

        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(string soundName)
    {
        if (!soundDict.TryGetValue(soundName, out AudioClip clip))
        {
            Debug.LogWarning("효과음을 찾을 수 없습니다: " + soundName);
            return;
        }

        sfxSource.PlayOneShot(clip, masterVolume * sfxVolume);
    }
}
