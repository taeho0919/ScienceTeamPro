using System;
using System.Collections;
using UnityEngine;

public class Boss3PatternTarget : MonoBehaviour
{
    [SerializeField] private Transform player;
    public  GameObject[] coils;
    [SerializeField]private float spawnInterval; 
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        StartCoroutine(Find());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        
    }

    private IEnumerator Find()
    {
        for (int i = 0; i < coils.Length; i++)
        {
            coils[i].SetActive(true);
            yield return new WaitForSeconds(spawnInterval);
        }

    }
}
