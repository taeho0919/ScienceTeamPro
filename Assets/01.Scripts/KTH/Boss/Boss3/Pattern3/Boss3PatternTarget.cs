using System;
using System.Collections;
using UnityEngine;

public class Boss3PatternTarget : MonoBehaviour
{
    [SerializeField] private Transform player;
    public  GameObject[] coils;
    [SerializeField]private float spawnInterval;
    [SerializeField] private float checkDelay = 3f;
    private KTH_CheckCoil checkCoil;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        checkCoil = GetComponent<KTH_CheckCoil>();
    }

    private void OnEnable()
    {
        StartCoroutine(Find());
    }

    void Update()
    {
        transform.position = player.position;

    }

    private IEnumerator Find()
    {
        foreach (GameObject coil in coils)
        {
            coil.SetActive(false);
        }

        for (int i = 0; i < coils.Length; i++)
        {
            coils[i].SetActive(true);
            yield return new WaitForSeconds(spawnInterval);
        }

        yield return new WaitForSeconds(checkDelay);
        checkCoil.Check();
    }
}
