using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class KTH_Boss3Pattern1 : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]private Transform target;
    [SerializeField]private Animator animator;
    private SplineAnimate splineanim;

    public readonly int IsAttack1Hash = Animator.StringToHash("IsAttack1");
    public readonly int IsEnd = Animator.StringToHash("IsEnd");
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        splineanim=GetComponentInParent<SplineAnimate>();
        rb.gravityScale = 0;
    }

    private void OnEnable()
    {
        StartCoroutine(FanFire());
    }

    private void OnDisable()
    {
        animator.SetBool(IsEnd,true);
        splineanim.Play();
        rb.gravityScale = 0;
        transform.position = target.position;
        
    }
    
    private IEnumerator FanFire()
    {
        animator.SetBool(IsEnd,false);
        splineanim.Pause();
        animator.SetTrigger(IsAttack1Hash);
        yield return new WaitForSeconds(0.5f);
        rb.gravityScale = 1;
        rb.AddForce(Vector2.left * 100,ForceMode2D.Impulse);
        

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent(out KTH_HealthSystem hs))
            {
                hs.TakeDamage(1);
            }
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            KTH_CameraShakeManager.Instance.Shake(0.5f);
        }
    }
}
