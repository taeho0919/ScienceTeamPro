using System;
using UnityEngine;

public class KTH_BossPattern3 : MonoBehaviour
{
    [SerializeField] private GameObject offCoil;
    [SerializeField]private GameObject onCoil;

    public readonly int attackHash = Animator.StringToHash("IsAttack");
    
    private Animator animator;

    private int i = 0;
    
    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        ChangeCoilTransform();
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
