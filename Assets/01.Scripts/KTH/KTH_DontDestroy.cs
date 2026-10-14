using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_DontDestroy : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField]private SpriteRenderer backGround;
    [SerializeField] private GameObject[] boss;
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

        if (scene.name == "Boss1Scene")
        {
            boss[0].SetActive(true);
            boss[2].SetActive(false);
            boss[1].SetActive(false);
        }
        if(scene.name == "Boss2Scene")
        {
            boss[1].SetActive(true);
            boss[0].SetActive(false);
            boss[2].SetActive(false);
        }
        if(scene.name == "Boss3Scene")
        {
            boss[2].SetActive(true);
            boss[1].SetActive(false);
            boss[0].SetActive(false);
        }
        
        if (scene.name == "EndingScene")
        {
            Destroy(gameObject);
            i = 0;
        }

        if (scene.name == "DeathScene")
        {
            Destroy(gameObject);
            i = 0;
        }
    }
}
