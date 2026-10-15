using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KTH_DontDestroy : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField]private SpriteRenderer backGround;
    [SerializeField] private GameObject[] boss;
    private int i = 0;
    private static KTH_DontDestroy instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            gameObject.SetActive(false);
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
        if (scene.name == "EndingScene" || scene.name == "DeathScene")
        {
            Destroy(gameObject);
            return;
        }

        if (i < backgrounds.Length)
        {
            backGround.sprite = backgrounds[i];
            i++;
        }

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
    }
}
