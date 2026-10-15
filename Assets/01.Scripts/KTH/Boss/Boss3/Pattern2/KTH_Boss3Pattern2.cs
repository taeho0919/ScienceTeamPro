using System;
using UnityEngine;
using UnityEngine.Splines;

public class KTH_Boss3Pattern2 : MonoBehaviour
{
    [SerializeField] private float windSpeed = 6f;
    private Vector3 windDirection=Vector3.left;
    private Rigidbody2D playerRb;
    
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
        playerRb = null;
    }

    private void FixedUpdate()
    {
        if (playerRb == null) return;

        Vector2 dir = new Vector2(windDirection.x, windDirection.y).normalized;
        playerRb.position += dir * windSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerRb = other.attachedRigidbody;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (other.attachedRigidbody == playerRb) playerRb = null;
    }
}
