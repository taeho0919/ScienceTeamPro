using System;
using System.Collections;
using UnityEngine;

public class CoilSpawn : MonoBehaviour
{
    [SerializeField] private GameObject coilPrefab;
    [SerializeField] private Animator _animator;

    public readonly int coilAmount = Animator.StringToHash("IsAttack3");

    private void Awake()
    {
        if (_animator == null)
        {
            _animator = transform.parent.GetComponentInChildren<Animator>();
        }
    }

    public IEnumerator Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(coilPrefab, transform.position, Quaternion.identity);
            _animator.SetTrigger(coilAmount);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
