using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    private Dictionary<string, AudioClip> soundDict;

    private AudioSource BGM;
    private AudioSource SFX;
    private AudioSource MasterSound;
    
    [Header("Audio clips")]
    [SerializeField] private AudioClip[] audioClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//씬 넘어가도 계속 되게 하는거(몰랐던것들)
        }
    }
    private void Init()
    {
        soundDict = new Dictionary<string, AudioClip>();
        BGM.loop = true;//반복(몰랐던것들)

        foreach (var clip in audioClips)
        {
            soundDict[clip.name] = clip;
        }
    }

    public void PlaySFX(string soundName)
    {
        if (soundDict.TryGetValue(soundName, out var clip))
        {
            SFX.PlayOneShot(clip);//한번 재생 코드(몰랐던것들)
        }
    }

    public void PlayBGM(string bgmName)
    {
        if (soundDict.TryGetValue(bgmName, out var clip))
        {
            if (BGM.clip != clip)
            {
                BGM.clip = clip;
                BGM.Play();
            }
        }
    }
}
