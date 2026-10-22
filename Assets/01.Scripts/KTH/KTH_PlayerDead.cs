using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_PlayerDead : MonoBehaviour, ISerializationCallbackReceiver
{
#if UNITY_EDITOR
    [SerializeField] private UnityEditor.SceneAsset scene;
#endif
    [SerializeField, HideInInspector] private string sceneName;

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        sceneName = scene != null ? scene.name : string.Empty;
#endif
    }

    public void OnAfterDeserialize() { }
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
        KTH_SceneManager.Instance.LoadSceneWithFade(sceneName);
    }
}
