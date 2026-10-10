using System;
using System.Collections;
using UnityEngine;

public class KTH_Base2 : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField]private int damage;
    [SerializeField]private float lifeTime;
    private Rigidbody2D rb;
    private KTH_HealthSystem hs;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(LifeTime());
    }

    private void FixedUpdate()
    {
        rb.linearVelocity=Vector2.left * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out KTH_HealthSystem hs))
            {
                hs.TakeDamage(damage);
            }
        }
    }


    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
