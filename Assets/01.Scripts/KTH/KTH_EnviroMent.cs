using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_EnviroMent : MonoBehaviour
{
    [SerializeField]private SceneAsset upDateScene;
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
        if (scene.name == upDateScene.name)
        {
            Instantiate(map, transform.position, quaternion.identity);
        }
    }
}
