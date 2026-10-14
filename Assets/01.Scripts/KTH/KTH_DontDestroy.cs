using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_DontDestroy : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField]private SpriteRenderer backGround;
    private int i = 0;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
        backGround.sprite = backgrounds[i];
        i++;
        
        if (scene.name == "EndingScene")
        {
            Destroy(gameObject);
        }

        if (scene.name == "DeathScene")
        {
            Destroy(gameObject);
        }
    }
}
