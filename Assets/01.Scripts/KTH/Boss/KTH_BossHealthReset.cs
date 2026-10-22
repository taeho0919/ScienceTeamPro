using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_BossHealthReset : MonoBehaviour, ISerializationCallbackReceiver
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

    private void Awake()
    {
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
        if (healthSystem != null && scene.name == resetSceneName)
        {
            healthSystem.Heal(maxHealth);
        }
    }
}
