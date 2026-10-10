using System;
using UnityEngine;
using UnityEngine.Splines;

public class KTH_BossDead : MonoBehaviour
{
    private KTH_HealthSystem hs;
    private Animator animator;
    private SplineAnimate splineanim;
    private KTH_BossBase bossBase;
    public readonly int DeadHash=Animator.StringToHash("IsDead");

    private void Awake()
    {
        hs = GetComponent<KTH_HealthSystem>();
        animator = GetComponent<Animator>();
        splineanim = GetComponent<SplineAnimate>();
        bossBase = GetComponent<KTH_BossBase>();
    }

    private void OnEnable()
    {
        hs.OnDeath += BossDead;
    }

    private void OnDisable()
    {
        hs.OnDeath -= BossDead;
    }


    private void BossDead()
    {
        bossBase.StopPatterns();
        splineanim.Pause();
        animator.SetTrigger(DeadHash);
    }
}
