using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class KTH_Boss1Pattern2 : MonoBehaviour
{
    [SerializeField] private float swingAngle = 60f;
    [SerializeField] private float swingTime = 0.4f;
    [SerializeField] private float resetTime = 0.2f;
    private Rigidbody2D rb;
    private Animator anim;
    private SplineAnimate splineAnimate;
    private Collider2D col;
    private KTH_HealthSystem hs;
    private Tween swingTween;
    
    public readonly int IsAttackHash = Animator.StringToHash("IsAttack2");
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();
        splineAnimate=GetComponentInParent<SplineAnimate>();
        col = GetComponentInParent<Collider2D>();
        hs = GetComponentInParent<KTH_HealthSystem>();
    }

    private void OnEnable()
    {
        TurnOff();
    }

    private void OnDisable()
    {
        TurnOn();
    }

    private void TurnOff()
    {
        hs.HealOnHit = true;
        rb.gravityScale = 4;
        anim.SetBool(IsAttackHash,true);
        col.enabled = true;
        splineAnimate.Pause();
        
        Transform boss = rb.transform;
        swingTween?.Kill();
        float angle = Random.Range(-swingAngle, swingAngle);
        swingTween = boss.DORotate(new Vector3(0f, 0f, angle), swingTime).SetEase(Ease.OutSine);
    }

    private void TurnOn()
    {
        hs.HealOnHit = false;
        swingTween?.Kill();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        swingTween = rb.transform.DORotate(Vector3.zero, resetTime).SetEase(Ease.OutSine);
        
        rb.gravityScale = 0;
        anim.SetBool(IsAttackHash,false);
        col.enabled = false;
        splineAnimate.Play();
    }
}
