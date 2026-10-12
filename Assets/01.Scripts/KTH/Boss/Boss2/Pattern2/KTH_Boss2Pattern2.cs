using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class KTH_Boss2Pattern2 : MonoBehaviour
{
    [SerializeField] private Vector2 boxOffset = new Vector2(-37.7f, 3.64f);
    [SerializeField] private Vector2 boxSize = new Vector2(68.83f, 13.97f);
    [SerializeField] private float growTime = 1f;

    private ParticleSystem ps;
    private SplineAnimate splineAnimate;
    private Animator animator;
    private bool canHit;
    private float currentWidth;

    public readonly int IsAttack2Hash = Animator.StringToHash("IsAttack2");
    public readonly int IsAttackEndHash = Animator.StringToHash("IsAttack2End");

    private void Awake()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        splineAnimate = GetComponentInParent<SplineAnimate>();
        animator = GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        Fire();
    }

    private void OnDisable()
    {
        canHit = false;
        animator.SetTrigger(IsAttackEndHash);
        splineAnimate.Play();
        ps.Stop();
    }

    private void Fire()
    {
        StopAllCoroutines();
        animator.SetTrigger(IsAttack2Hash);
        ps.Play();
        splineAnimate.Pause();
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        canHit = true;
        currentWidth = 0f;
        while (currentWidth < boxSize.x)
        {
            currentWidth += boxSize.x / growTime * Time.deltaTime;
            yield return null;
        }
        currentWidth = boxSize.x;
    }

    private Vector2 GetCenter(float width)
    {
        float rightEdge = boxOffset.x + boxSize.x * 0.5f;
        return (Vector2)transform.position + new Vector2(rightEdge - width * 0.5f, boxOffset.y);
    }

    private void FixedUpdate()
    {
        if (!canHit) return;

        foreach (Collider2D hit in Physics2D.OverlapBoxAll(GetCenter(currentWidth),new Vector2(currentWidth, boxSize.y), 0f))
        {
            if (hit.CompareTag("Player") && hit.TryGetComponent(out KTH_HealthSystem hs))
            {
                hs.TakeDamage(1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float width = Application.isPlaying ? currentWidth : boxSize.x;
        Gizmos.DrawWireCube(GetCenter(width),new Vector2(width, boxSize.y));
    }
}
