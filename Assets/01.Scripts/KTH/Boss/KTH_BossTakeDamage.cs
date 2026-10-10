using System.Collections;
using UnityEngine;

public class KTH_BossTakeDamage : MonoBehaviour
{
    [SerializeField] private Color color;
    private Color baseColor;
    private KTH_HealthSystem hs;
    private SpriteRenderer sr;
    private void Awake()
    {
        hs=GetComponent<KTH_HealthSystem>();
        sr=GetComponent<SpriteRenderer>();
        baseColor=sr.color;
    }

    private void OnEnable()
    {
        hs.OnHealthChange += ChangeColor;
    }

    private void OnDisable()
    {
        hs.OnHealthChange -= ChangeColor;
    }
    
    private void ChangeColor()
    {
        sr.color = color;
        StartCoroutine(ChangeBaseColor());
    }

    private IEnumerator ChangeBaseColor()
    {
        yield return new WaitForSeconds(0.1f);
        sr.color = baseColor;
    }
}
