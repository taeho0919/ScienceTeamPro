using System;
using UnityEngine;

public class KTH_Test : MonoBehaviour
{
    [SerializeField]private KTH_HealthSystem  healthSystem;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            healthSystem.TakeDamage(1);
        }
    }
}
