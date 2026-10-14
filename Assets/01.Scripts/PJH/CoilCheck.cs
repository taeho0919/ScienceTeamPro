using System;
using System.Collections;
using UnityEngine;

public class CoilCheck : MonoBehaviour
{ 
    [SerializeField] private KTH_HealthSystem kth_healthSystem;
    [SerializeField]private ParticleSystem ps;
    private bool _canAttack = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Magenetic") && _canAttack)
        {
            kth_healthSystem.TakeDamage(1);
            ps.Play();
            StartCoroutine(WaitCanAttack());
        }
            
            
    }

    private IEnumerator WaitCanAttack()
    {
        _canAttack = false;
        yield return new WaitForSeconds(0.4f);
        _canAttack = true;
    }
}
