using System;
using UnityEngine;

public class KTH_Bakery : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 originalPos;
    
    private void  Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPos = transform.position;
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = -10f;
    }

    private void OnDisable()
    {
        transform.position = originalPos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.TryGetComponent(out KTH_HealthSystem hs))
            {
                hs.TakeDamage(1);
                gameObject.SetActive(false);
            }
        }
    }
}
