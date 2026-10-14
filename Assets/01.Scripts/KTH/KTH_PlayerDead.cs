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
        hs.OnDeath+= OnPlayerDeath;
    }

    private void OnDisable()
    {
        hs.OnDeath -= OnPlayerDeath;
        OnDeath();
    }
    
    private void OnPlayerDeath()
    {
        gameObject.SetActive(false);       
    }

    private void OnDeath()
    {
        SceneManager.LoadScene(scene.name);
    }
}
