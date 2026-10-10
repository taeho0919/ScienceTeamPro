using System.Collections;
using UnityEngine;

public class KTH_Base3 : MonoBehaviour
{
    [SerializeField] private float delay = 0.8f;
    [SerializeField] private float lifeTime = 0.3f;
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask hitLayer = ~0;
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

    private IEnumerator Strike()
    {
        yield return new WaitForSeconds(delay);
        isAttack = true;
        animator.SetBool(AttackHash, true);
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!isAttack) return;

        Vector2 top = new Vector2(transform.position.x, sr.bounds.max.y);
        RaycastHit2D hit = Physics2D.Raycast(top, Vector2.down, sr.bounds.size.y, hitLayer);
        if (hit.collider == null || !hit.collider.CompareTag("Player")) return;

        if (hit.collider.TryGetComponent(out KTH_HealthSystem hs))
        {
            hs.TakeDamage(damage);
            isAttack = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x, renderer.bounds.max.y, 0f), new Vector3(transform.position.x, renderer.bounds.min.y, 0f));
    }
}
