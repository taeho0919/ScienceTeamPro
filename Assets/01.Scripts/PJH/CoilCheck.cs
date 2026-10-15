using System;
using System.Collections;
using _01.Scripts.Core;
using UnityEngine;

public class CoilCheck : MonoBehaviour
{ 
    [SerializeField] private KTH_HealthSystem kth_healthSystem;
    
    [Header("데미지")]
    [SerializeField] private int baseDamage;
    [SerializeField] private int bonusDamage;
    
    [Header("유도 단계")]
    [SerializeField] private int maxLevel;
    [SerializeField] private float decayDelay = 4.5f;
    [SerializeField] private float decayInterval = 1.5f;

    private int _inducationLevel = 0;
    private float _decayTimer;
    
    
    
    
    private bool _canAttack = true;


    private void Update()
    {
        if (_inducationLevel <= 0) return;
        
        _decayTimer -= Time.deltaTime;

        if (_decayTimer <= 0f)
        {
            _inducationLevel--;
            _decayTimer = decayInterval;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Magenetic") && _canAttack)
        {
            _inducationLevel = Mathf.Min(_inducationLevel + 1, maxLevel);
            _decayTimer = decayDelay;
            
            int damage = baseDamage + bonusDamage * _inducationLevel;
            
            kth_healthSystem.TakeDamage(damage);
            Logging.Log(damage);
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
