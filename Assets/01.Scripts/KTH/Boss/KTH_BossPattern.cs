using System;
using UnityEngine;

public abstract class KTH_BossPattern : MonoBehaviour
{
    public Action<KTH_BossPattern> OnPatternEnd;

    protected virtual void OnEnable()
    {
        StartPattern();
    }
    
    protected abstract void StartPattern();
    
    protected void Finish()
    {
        gameObject.SetActive(false);
        OnPatternEnd?.Invoke(this);
    }
}
