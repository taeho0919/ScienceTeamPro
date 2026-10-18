using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_PlayerDead : MonoBehaviour
{
    [SerializeField] private SceneAsset scene;
    private KTH_HealthSystem hs;
    
    private void Awake()
    {
        hs = GetComponent<KTH_HealthSystem>();
    }

    private void OnEnable()
    {
        hs.OnPlayerDeath+= OnPlayerDeath;
    }

    private void OnDisable()
    {
        hs.OnPlayerDeath -= OnPlayerDeath;
    }
    
    private void OnPlayerDeath()
    {
        gameObject.SetActive(false);       
        OnDeath();
    }

    private void OnDeath()
    {
        Debug.Log("Player Dead");
        KTH_SceneManager.Instance.LoadSceneWithFade(scene.name);
    }
}
