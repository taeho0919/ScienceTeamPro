using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_EnviroMent : MonoBehaviour, ISerializationCallbackReceiver
{
#if UNITY_EDITOR
    [SerializeField]private UnityEditor.SceneAsset upDateScene;
#endif
    [SerializeField, HideInInspector] private string upDateSceneName;

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        upDateSceneName = upDateScene != null ? upDateScene.name : string.Empty;
#endif
    }

    public void OnAfterDeserialize() { }
    [SerializeField] private GameObject map;
    
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
        if (scene.name == upDateSceneName)
        {
            Instantiate(map, transform.position, quaternion.identity);
        }
    }
}
