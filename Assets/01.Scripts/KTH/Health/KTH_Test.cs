using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_Test : MonoBehaviour
{
    [SerializeField]private KTH_HealthSystem  healthSystem;
    [SerializeField] private SceneAsset s;
    
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

        if (Input.GetKeyDown(KeyCode.F) && s != null)
        {
            SceneManager.LoadScene(s.name);
        }
    }
}
