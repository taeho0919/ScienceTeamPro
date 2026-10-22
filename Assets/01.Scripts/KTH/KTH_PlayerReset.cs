using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_PlayerReset : MonoBehaviour, ISerializationCallbackReceiver
{
#if UNITY_EDITOR
    [SerializeField]private UnityEditor.SceneAsset resetScene;
#endif
    [SerializeField, HideInInspector] private string resetSceneName;

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        resetSceneName = resetScene != null ? resetScene.name : string.Empty;
#endif
    }

    public void OnAfterDeserialize() { }
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
        if (scene.name == resetSceneName)
        {
            healthSystem.Heal(maxHealth);
            transform.position = origin;
        }
    }
}
