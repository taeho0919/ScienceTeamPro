using System;
using UnityEngine;

public class KTH_PlayerAnimationEnd : MonoBehaviour
{
    public Action OnEnd;
    
    public void EndAnimation()
    {
        gameObject.SetActive(false);
        OnEnd?.Invoke();
    }
}
