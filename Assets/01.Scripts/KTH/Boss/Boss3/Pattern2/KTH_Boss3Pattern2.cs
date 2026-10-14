using System;
using UnityEngine;
using UnityEngine.Splines;

public class KTH_Boss3Pattern2 : MonoBehaviour
{
    [SerializeField] private float windPower=5f;
    private Vector3 windDirection=Vector3.left;
    
    private Animator animator;
    private SplineAnimate splineAnimate;
    private ParticleSystem ps;
    
    public readonly int IsAttack2Hash = Animator.StringToHash("IsAttack2");
    public readonly int AttackEndHash = Animator.StringToHash("IsAttack2End");
    
    private void Awake()
    {
        splineAnimate = GetComponentInParent<SplineAnimate>();
        animator = GetComponentInParent<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        splineAnimate.Pause();   
        animator.SetBool(AttackEndHash,false);
        animator.SetTrigger(IsAttack2Hash);
        ps.Play();
    }

    private void OnDisable()
    {
        splineAnimate.Play();
        animator.SetBool(AttackEndHash,true);
        ps.Stop();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!(other.CompareTag("Player")))return;
        
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(new Vector2(windDirection.x,windDirection.y).normalized*windPower, ForceMode2D.Force);
        }
    }
}
