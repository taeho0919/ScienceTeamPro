using System;
using UnityEngine;

public class KTH_BossPattern3 : MonoBehaviour
{
    [SerializeField] private GameObject offCoil;
    [SerializeField]private GameObject onCoil;
    [SerializeField] private KTH_BaseSoundSO soundSO;

    public readonly int attackHash = Animator.StringToHash("IsAttack");
    
    private Animator animator;

    private int i = 0;
    
    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        PlaySound();
        ChangeCoilTransform();
    }

    private void PlaySound()
    {
        if (soundSO == null) return;
        soundSO.Play(soundSO.boss2Pattern3);
    }

    private void ChangeCoilTransform()
    {
        animator.SetTrigger(attackHash);
        
        if (i == 0)
        {
            offCoil.SetActive(false);
            onCoil.SetActive(true);
            i++;
        }
        else
        {
            offCoil.SetActive(true);
            onCoil.SetActive(false);
            i = 0;
        }
    }
}
