using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.SceneManagement;

public class KTH_BossDead : MonoBehaviour
{
    [SerializeField]private SceneSO ss;
    
    private KTH_HealthSystem hs;
    private Animator animator;
    private SplineAnimate splineanim;
    private KTH_BossBase bossBase;
    private int i;
    
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
        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(1f);
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name !=ss.scene[i].name)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(ss.scene[i].name);
            i++;
        }
        else
        {
            i++;
            UnityEngine.SceneManagement.SceneManager.LoadScene(ss.scene[i].name);
        }
    }
}
