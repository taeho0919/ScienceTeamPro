using System.Collections;
using UnityEngine;

public class KTH_BaseAttack2 : MonoBehaviour
{
    [SerializeField] private GameObject attack2;
    [SerializeField] private float spawnInterval = 0.3f;

    private void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        int count = Random.Range(1, 3);
        for (int i = 0; i < count; i++)
        {
            Instantiate(attack2, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
