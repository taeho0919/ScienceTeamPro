using UnityEngine;

public class GalvanometerController : MonoBehaviour
{
    [Header("검류계 애니메이터")]
    [SerializeField] private Animator animator;

    private Collider2D coilCollider;
    private bool isAnimating;

    private void Update()
    {
        if (coilCollider == null)
        {
            FindCoil();
            return;
        }

        Collider2D[] hits = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();

        int count = coilCollider.Overlap(filter, hits);
        bool touchingWeapon = false;

        for (int i = 0; i < count; i++)
        {
            if (hits[i] != null && hits[i].CompareTag("Magenetic"))
            {
                touchingWeapon = true;

                if (!isAnimating && animator != null)
                {
                    animator.SetTrigger("Move");
                    isAnimating = true;
                }

                break;
            }
        }

        if (!touchingWeapon)
        {
            isAnimating = false;
        }
    }

    private void FindCoil()
    {
        CoilCheck coil = FindFirstObjectByType<CoilCheck>();

        if (coil == null)
            return;

        coilCollider = coil.GetComponent<Collider2D>();

        if (coilCollider == null)
            coilCollider = coil.GetComponentInChildren<Collider2D>();
    }
}