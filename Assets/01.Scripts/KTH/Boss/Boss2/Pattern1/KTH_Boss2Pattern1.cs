using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class KTH_Boss2Pattern1 : MonoBehaviour
{
    [SerializeField]private GameObject[] bakery;
    [SerializeField] private KTH_BaseSoundSO soundSO;

    private Animator animator;
    private SplineAnimate splineAnimate;

    public readonly int IsAttackHash = Animator.StringToHash("IsAttack");
    
    private void Awake()
    {
        splineAnimate = GetComponentInParent<SplineAnimate>();
        animator = GetComponentInParent<Animator>();
            
    }

    private void OnEnable()
    {
        StartCoroutine(BakeryOn());
    }

    private IEnumerator BakeryOn()
    {
        splineAnimate.Pause();
        
        foreach (GameObject obj in bakery)
        {
            animator.SetTrigger(IsAttackHash);
            obj.SetActive(true);
            PlaySound();
            yield return new WaitForSeconds(1f);
        }
    }

    private void PlaySound()
    {
        if (soundSO == null) return;
        soundSO.Play(soundSO.boss2Pattern1);
    }

    private void OnDisable()
    {
        splineAnimate.Play();
        
        foreach (GameObject obj in bakery)
        {
            obj.SetActive(false);
        }
    }
}
