using UnityEngine;

[CreateAssetMenu(fileName = "BaseSoundSO", menuName = "KTH/BaseSoundSO")]
public class KTH_BaseSoundSO : ScriptableObject
{
    [Header("기본 공격")]
    public AudioClip baseAttack1;
    public AudioClip baseAttack2;
    public AudioClip baseAttack3;

    [Header("보스1")]
    public AudioClip boss1Pattern1;
    public AudioClip boss1Pattern2;

    [Header("보스2")]
    public AudioClip boss2Pattern2;
    public AudioClip boss2Pattern3;

    [Header("보스3")]
    public AudioClip boss3Pattern1;
    public AudioClip boss3Pattern2;
    public AudioClip boss3Pattern3;

    public void Play(AudioClip clip)
    {
        if (clip == null || SoundManager.instance == null) return;
        SoundManager.instance.PlaySFX(clip);
    }
}
