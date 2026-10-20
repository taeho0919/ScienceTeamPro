using System.Collections;
using UnityEngine;

public class KTH_BaseAttack1 : MonoBehaviour
{
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask hitLayer = ~0;
    [SerializeField]private Transform playerTransform;
    [SerializeField] private KTH_BaseSoundSO soundSO;
    private Animator animator;
    private SpriteRenderer sr;
    private bool isAttack = false;
    private readonly int AttackHash = Animator.StringToHash("IsAttack");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        FindPlayer();
        PlaySound();
        StartCoroutine(BaseAttack());
    }

    private void OnDisable()
    {
        isAttack = false;
        animator.SetBool(AttackHash, false);
    }

    private IEnumerator BaseAttack()
    {
        yield return new WaitForSeconds(delay);
        isAttack = true;
        animator.SetBool(AttackHash, true);
    }

    private void Update()
    {
        if (!isAttack) return;

        Collider2D hit = Physics2D.OverlapBox(sr.bounds.center, sr.bounds.size, 0f, hitLayer);
        if (hit == null || !hit.CompareTag("Player")) return;

        if (hit.TryGetComponent(out KTH_HealthSystem hs))
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
        Gizmos.DrawWireCube(renderer.bounds.center, renderer.bounds.size);
    }

    private void PlaySound()
    {
        if (soundSO == null) return;
        soundSO.Play(soundSO.baseAttack1);
    }

    private void FindPlayer()
    {
        Vector3 pos = transform.position;
        pos.y = playerTransform.position.y;
        transform.position = pos;
    }
}
