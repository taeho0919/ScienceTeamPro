using System;
using UnityEngine;
using UnityEngine.UI;

public class InductionUI : MonoBehaviour
{
    [Header("유도전력 색깔")]
    [SerializeField] private Color baseColor;
    [SerializeField] private Color changeColor;
    
    [SerializeField] private CoilCheck coilCheck;
    
    [Header("유도전력 UI")]
    [SerializeField] private Image[] inductions;

    private int currentLevel;


    private void OnEnable()
    {
        coilCheck.OnChanged += Clear;
    }

    private void OnDisable()
    {
        coilCheck.OnChanged -= Clear;
    }
    
    


    public void Update()
    {
        ChangeColor(coilCheck._inducationLevel);
    }

    private void ChangeColor(int level)
    {
        currentLevel = level;
        if (level <= 0) return;
        for (int i = 0; i < level; i++)
        {
            inductions[i].color = changeColor;
        }
    }

    private void Clear()
    {
        foreach (var image  in inductions)
        {
            image.color = baseColor;
        }
    }
}
