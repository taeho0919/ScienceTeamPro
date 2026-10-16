using System;
using System.Collections;
using _01.Scripts.Core;
using UnityEngine;
using UnityEngine.Serialization;

public class CoilCheck : MonoBehaviour
{ 
    [SerializeField] private KTH_HealthSystem kth_healthSystem;
    
    [FormerlySerializedAs("coilData")]
    [Header("코일 설정")]
    [SerializeField] private KTH_CoilSO kthCoilData;

    [Header("플레이어 체력 컴포넌트")]
    [SerializeField] private KTH_HealthSystem playerHealth;

    [Header("파티클")]
    [SerializeField] private ParticleSystem ps;

    public int InducationLevel { get; private set; } = 0;
    private float _decayTimer;
    
    
    private bool _canAttack = true;

    public event Action OnChanged;

    private void Reset()
    {
        FindParticle();
    }

    private void OnValidate()
    {
        if (ps == null) FindParticle();
    }

    private void FindParticle()
    {
        ps = GetComponentInChildren<ParticleSystem>(true);
        if (ps == null) ps = GetComponentInParent<ParticleSystem>(true);
    }

    private void Awake()
    {
        if (ps == null) FindParticle();
        if (playerHealth == null) playerHealth = GetComponentInParent<KTH_HealthSystem>();
    }

    private void OnEnable()
    {
        if (playerHealth != null) playerHealth.OnHealthChange += MinusLevel;
    }

    private void OnDisable()
    {
        if (playerHealth != null) playerHealth.OnHealthChange -= MinusLevel;
        InducationLevel = 0;
        _canAttack = true;
        OnChanged?.Invoke();
    }

    private void Update()
    {
        Logging.Log(InducationLevel);
        if (InducationLevel <= 0) return;
        
        _decayTimer -= Time.deltaTime;

        if (_decayTimer <= 0f)
        {
            InducationLevel--;
            OnChanged?.Invoke();
            _decayTimer = kthCoilData.decayInterval;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Magenetic") && _canAttack)
        {
            InducationLevel = Mathf.Min(InducationLevel + 1, kthCoilData.maxLevel);
            _decayTimer = kthCoilData.decayDelay;
            
            int damage = kthCoilData.baseDamage + kthCoilData.bonusDamage * InducationLevel;
            
            kth_healthSystem.TakeDamage(damage);
            if (ps != null) ps.Play();
            Logging.Log(damage);
            StartCoroutine(WaitCanAttack());
        }
            
            
    }

    private void MinusLevel()
    {
        if (InducationLevel <= 0) return;
        InducationLevel--;
        OnChanged?.Invoke();
    }

    private IEnumerator WaitCanAttack()
    {
        _canAttack = false;
        yield return new WaitForSeconds(kthCoilData.attackCooldown);
        _canAttack = true;
    }
}
