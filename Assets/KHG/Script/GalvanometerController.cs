using UnityEngine;

public class GalvanometerController : MonoBehaviour
{
    [Header("코일 충돌 감지")]
    [SerializeField] private Collider2D coilCollider;

    [Header("검류계 애니메이터")]
    [SerializeField] private Animator animator;

    private void Update()
    {
        if (coilCollider == null || animator == null)
            return;

        Collider2D[] hits = new Collider2D[10];

        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();

        int count = coilCollider.Overlap(filter, hits);

        for (int i = 0; i < count; i++)
        {
            if (hits[i] != null && hits[i].CompareTag("Magenetic"))
            {
                animator.SetTrigger("Move");
                return;
            }
        }
    }
}