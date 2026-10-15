using System.Collections;
using UnityEngine;

public class KTH_Base3 : MonoBehaviour
{
    [SerializeField] private float delay = 0.8f;
    [SerializeField] private float lifeTime = 0.3f;
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask hitLayer = ~0;
    [SerializeField, Range(0f, 1f)] private float hitWidthRatio = 0.5f;
    private Animator animator;
    private SpriteRenderer sr;
    private bool isAttack = false;
    private readonly int AttackHash = Animator.StringToHash("IsAttack");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(Strike());
    }

    public void FitHeight(float bottom, float top)
    {
        float height = top - bottom;
        if (height <= 0f) return;

        Vector3 scale = transform.localScale;
        scale.y *= height / sr.bounds.size.y;
        transform.localScale = scale;

        Vector3 pos = transform.position;
        pos.y += bottom - sr.bounds.min.y;
        transform.position = pos;
    }

    private IEnumerator Strike()
    {
        yield return new WaitForSeconds(delay);
        isAttack = true;
        animator.SetBool(AttackHash, true);
        CheckHit();
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void Update()
    {
        CheckHit();
    }

    private void CheckHit()
    {
        if (!isAttack) return;

        Bounds b = sr.bounds;
        Vector2 size = new Vector2(b.size.x * hitWidthRatio, b.size.y);
        Collider2D[] hits = Physics2D.OverlapBoxAll(b.center, size, 0f, hitLayer);
        foreach (Collider2D col in hits)
        {
            if (!col.CompareTag("Player")) continue;
            if (!col.TryGetComponent(out KTH_HealthSystem hs)) continue;
            if (hs.IsInvincible) return;

            hs.TakeDamage(damage);
            isAttack = false;
            return;
        }
    }

    private void OnDrawGizmosSelected()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer == null) return;

        Bounds b = renderer.bounds;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(b.center, new Vector3(b.size.x * hitWidthRatio, b.size.y, 0f));
    }
}
