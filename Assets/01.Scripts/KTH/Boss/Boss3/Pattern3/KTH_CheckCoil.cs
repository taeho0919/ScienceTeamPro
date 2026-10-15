using System.Collections;
using UnityEngine;

public class KTH_CheckCoil : MonoBehaviour
{
    [SerializeField] private CoilSpawn spawner;
    private Boss3PatternTarget b3t;
    private Animator animtor;
    
    
    private void Awake()
    {
        b3t = GetComponent<Boss3PatternTarget>();
        animtor = GetComponentInParent<Animator>();
    }

    public void Check()
    {
        int remaining = CountRemaining();
        if (remaining <= 0) return;

        DisableCoils();
        spawner.gameObject.SetActive(true);
        spawner.StartCoroutine(spawner.Spawn(remaining));
    }

    private void DisableCoils()
    {
        foreach (GameObject coil in b3t.coils)
        {
            coil.SetActive(false);
        }
    }

    private int CountRemaining()
    {
        int count = 0;
        foreach (GameObject coil in b3t.coils)
        {
            if (!coil.activeSelf) continue;

            KTH_CapyCoil capy = coil.GetComponentInChildren<KTH_CapyCoil>();
            if (capy != null && capy.IsHit) continue;

            count++;
        }
        return count;    
    }
    
    
}
