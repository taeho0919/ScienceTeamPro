using System.Collections;
using UnityEngine;

public class KTH_CapyCoil : MonoBehaviour
{
    [SerializeField]private ParticleSystem ps;
    [SerializeField] private GameObject coil;
    private bool _canAttack = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Magenetic") && _canAttack)
        {
            
            StartCoroutine(WaitCanAttack());
            
        }
            
            
    }

    private IEnumerator WaitCanAttack()
    {
        ps.Play();
        yield return new WaitForSeconds(0.4f);
        coil.gameObject.SetActive(false);
    }
}
