using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

enum HealthType
{
    Boss,
    Player
}

public class KTH_HealthSystem : MonoBehaviour
{
    [SerializeField]private int health;
    [SerializeField] private HealthType ht;
    [SerializeField]private Volume volume;
    private Vignette vignette;
    private int maxHealth;
    [SerializeField] private float lowHealthVignette = 0.49f, vignetteSpeed = 1f,InvinTime = 0.5f;
    
    public Action OnDeath;
    public Action<int,int> OnHealthChange;

    private bool isInvin=false;
    
    private void Awake()
    {
        maxHealth = health;
        
        if (volume.profile.TryGet<Vignette>(out var Outvignette))
        {
            vignette= Outvignette;
        }
    }

    private void Update()
    {
        CheckDamageEffect();
    }

    public void TakeDamage(int damage)
    {
        if(isInvin&&ht == HealthType.Player)return;
        
        health=Mathf.Max(health-damage,0);
        
        OnHealthChange?.Invoke(health,maxHealth);
        
        if (health <= 0)
        {
            OnDeath?.Invoke();
        }

        StartCoroutine(IsInvin());
    }

    public void Heal(int heal)
    {
        if(health==0)return;
        health=Mathf.Min(health+heal,maxHealth);
        OnHealthChange?.Invoke(health,maxHealth);
    }

    private void CheckDamageEffect()
    {
        if (ht != HealthType.Player || vignette == null) return;
        
        
        float target = health == 1 ? lowHealthVignette : 0f;
        vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, target, vignetteSpeed * Time.deltaTime);
    }

    private IEnumerator IsInvin()
    {
        isInvin = true;
        yield return new WaitForSeconds(InvinTime);
        isInvin=false;  
    }
}
