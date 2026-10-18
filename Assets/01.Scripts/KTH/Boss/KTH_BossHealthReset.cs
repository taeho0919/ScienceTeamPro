using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_BossHealthReset : MonoBehaviour
{
    [SerializeField]private SceneAsset resetScene;
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
        if (scene.name == resetScene.name)
        {
            healthSystem.Heal(maxHealth);
        }
    }
}
