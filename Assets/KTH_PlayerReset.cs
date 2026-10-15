using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_PlayerReset : MonoBehaviour
{
    [SerializeField]private SceneAsset resetScene;
    [SerializeField]private int maxHealth;
    private KTH_HealthSystem  healthSystem;
    private Vector3 origin;

    private void Awake()
    {
        origin = transform.position;
        healthSystem = GetComponent<KTH_HealthSystem>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoadScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoadScene;   
    }

    private void OnLoadScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == resetScene.name)
        {
            healthSystem.Heal(maxHealth);
            transform.position = origin;
        }
    }
}
