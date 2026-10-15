using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

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
    [SerializeField] private float lowHealthVignette = 0.49f, vignetteSpeed = 1f;
    [SerializeField] private Image hpBar;
    
    public Action OnDeath;
    public Action OnPlayerDeath;
    public Action OnHealthChange;
    
    public bool IsInvincible { get; set; }
    public bool HealOnHit { get; set; }
    
    private Tween hpBarTween;
    
    private void Awake()
    {
        maxHealth = health;
        
        if(ht==HealthType.Boss)return;
        
        if (volume.profile.TryGet<Vignette>(out var Outvignette))
        {
            vignette= Outvignette;
        }
    }

    private void OnDisable()
    {
        hpBarTween.Kill();
    }

    private void Update()
    {
        CheckDamageEffect();
    }

    public void TakeDamage(int damage)
    {
        if(health<=0)return;
        if (IsInvincible) return;

        if (HealOnHit)
        {
            Heal(damage);
            return;
        }
        
        health=Mathf.Max(health-damage,0);
        
        OnHealthChange?.Invoke();
        
        UpdateHpBarAnimated();
        
        if (health <= 0&&ht == HealthType.Boss)
        {
            Debug.Log("Boss Dead");
            OnDeath?.Invoke();
        }
        else if (health <= 0 && ht == HealthType.Player)
        {
            Debug.Log("Player Dead");
            OnPlayerDeath?.Invoke();
        }
    }

    public void Heal(int heal)
    {
        if(health==0)return;
        
        UpdateHpBarAnimated();
        
        health=Mathf.Min(health+heal,maxHealth);
    }

    private void CheckDamageEffect()
    {
        if (ht != HealthType.Player || vignette == null) return;
        
        
        float target = health == 1 ? lowHealthVignette : 0f;
        vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, target, vignetteSpeed * Time.deltaTime);
    }

    private void UpdateHpBarAnimated()
    {
        float targetFill = Mathf.Clamp01((float)health / maxHealth);

        hpBarTween?.Kill();
        hpBarTween = hpBar
            .DOFillAmount(targetFill, 0.5f)
            .SetEase(Ease.OutQuad);
    }
}
