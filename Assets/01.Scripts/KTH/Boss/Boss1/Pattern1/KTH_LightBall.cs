using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class KTH_LightBall : MonoBehaviour
{
    private Transform player;
    private Vector3 originPos;
    private Tween tween;    
    private KTH_LightBallSpawn spawn;
    private Animator anim;

    public readonly int attack1Hash = Animator.StringToHash("IsAttack1");
    
    private void Awake()
    {
        originPos = transform.localPosition;
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spawn=GetComponentInParent<KTH_LightBallSpawn>();
        anim=transform.parent.GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        spawn.StartMove += LightBallMove;
    }
    
    
    private void LightBallMove()
    {
        anim.SetTrigger(attack1Hash);
        tween=transform.DOMove(player.position, 0.5f);
    }

    private void ResetMove()
    {
        transform.localPosition=originPos;
    }

    private void OnDisable()
    {
        spawn.StartMove -= LightBallMove;
        tween?.Kill();
        ResetMove();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out KTH_HealthSystem hs))
            {
                hs.TakeDamage(1);
            }
        }
    }
}
