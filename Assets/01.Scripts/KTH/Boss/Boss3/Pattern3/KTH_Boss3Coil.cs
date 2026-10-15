using System;
using System.Collections.Generic;
using UnityEngine;

public class KTH_Boss3Coil : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.TryGetComponent(out KTH_HealthSystem hs))
            {
                hs.TakeDamage(1);
            }
        }
    }
}
