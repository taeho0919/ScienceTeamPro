using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class KTH_BossDead : MonoBehaviour
{
    private KTH_HealthSystem hs;
    private Animator animator;
    private SplineAnimate splineanim;
    private KTH_BossBase bossBase;
    
    public readonly int DeadHash=Animator.StringToHash("IsDead");
    private const string DeathStateName = "Death";
    private bool isDead;

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
        if (isDead) return;
        isDead = true;
        Debug.Log("Boss Dead");

        bossBase.StopPatterns();
        splineanim.Pause();
        animator.SetTrigger(DeadHash);
        StartCoroutine(WaitDeathAnimation());
    }

    private IEnumerator WaitDeathAnimation()
    {
        float timeout = 5f;

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(DeathStateName) && timeout > 0f)
        {
            timeout -= Time.deltaTime;
            yield return null;
        }

        while (animator.GetCurrentAnimatorStateInfo(0).IsName(DeathStateName)
               && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f
               && timeout > 0f)
        {
            timeout -= Time.deltaTime;
            yield return null;
        }

        KTH_SceneManager.Instance.NextSceneGo();
    }
    
}
