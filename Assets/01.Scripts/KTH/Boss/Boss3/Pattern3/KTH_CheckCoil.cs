using System;
using System.Xml.Schema;
using UnityEngine;

public class KTH_CheckCoil : MonoBehaviour
{
    private Boss3PatternTarget b3t;
    private int j;
    
    private void Awake()
    {
        b3t = GetComponent<Boss3PatternTarget>();
    }

    private void OnDisable()
    {
        CheckCoil();
        
    }

    private void CheckCoil()
    {
        for (int i = 0; i < b3t.coils.Length; i++)
        {
            if (b3t.coils[i].activeSelf == false)
            {
                j++;
            }
        }
    }
}
