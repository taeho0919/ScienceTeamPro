using System;
using UnityEngine;

public class KTH_PlayerDead : MonoBehaviour
{
    private KTH_HealthSystem hs;

    private void Awake()
    {
        hs = GetComponent<KTH_HealthSystem>();
    }

    private void OnEnable()
    {
        hs.OnDeath+= OnPlayerDeath;
    }

    private void OnDisable()
    {
        hs.OnDeath -= OnPlayerDeath;
    }
    
    private void OnPlayerDeath()
    {
        Destroy(gameObject);       
    }
}
