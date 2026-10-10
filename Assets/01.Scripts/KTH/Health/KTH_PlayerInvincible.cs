using System.Collections;
using UnityEngine;

public class KTH_PlayerInvincible : MonoBehaviour
{
    [SerializeField] private float invinTime = 0.5f;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField, Range(0f, 1f)] private float blinkAlpha = 0.3f;
    private KTH_HealthSystem hs;
    private SpriteRenderer sr;
    private float baseAlpha;

    private void Awake()
    {
        hs = GetComponent<KTH_HealthSystem>();
        sr = GetComponentInChildren<SpriteRenderer>();
        baseAlpha = sr.color.a;
    }

    private void OnEnable()
    {
        hs.OnHealthChange += StartInvincible;
    }

    private void OnDisable()
    {
        hs.OnHealthChange -= StartInvincible;
        hs.IsInvincible = false;
        SetAlpha(baseAlpha);
    }

    private void StartInvincible()
    {
        StopAllCoroutines();
        StartCoroutine(InvincibleRoutine());
    }

    private IEnumerator InvincibleRoutine()
    {
        hs.IsInvincible = true;

        float timer = 0f;
        bool isFaded = false;
        while (timer < invinTime)
        {
            isFaded = !isFaded;
            SetAlpha(isFaded ? blinkAlpha : baseAlpha);
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        SetAlpha(baseAlpha);
        hs.IsInvincible = false;
    }

    private void SetAlpha(float alpha)
    {
        Color color = sr.color;
        color.a = alpha;
        sr.color = color;
    }
}
