using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_Test : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField]private KTH_HealthSystem  healthSystem;
#if UNITY_EDITOR
    [SerializeField] private UnityEditor.SceneAsset s;
#endif
    [SerializeField, HideInInspector] private string sceneName;

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        sceneName = s != null ? s.name : string.Empty;
#endif
    }

    public void OnAfterDeserialize() { }
    
    private static KTH_Test instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && healthSystem != null)
        {
            healthSystem.TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.F) && !string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
