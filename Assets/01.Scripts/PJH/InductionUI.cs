using System;
using UnityEngine;
using UnityEngine.UI;

public class InductionUI : MonoBehaviour
{
    [Header("유도전력 색깔")]
    [SerializeField] private Color baseColor;
    [SerializeField] private Color changeColor;
    
    [SerializeField] private CoilCheck[] coilChecks;
    
    [Header("유도전력 UI")]
    [SerializeField] private Image[] inductions;

    private int _currentLevel;


    private void Start()
    {
        Clear();
    }

    private void OnEnable()
    {
        foreach (var coil in coilChecks)
        {
            if (coil != null) coil.OnChanged += Clear;
        }
    }

    private void OnDisable()
    {
        foreach (var coil in coilChecks)
        {
            if (coil != null) coil.OnChanged -= Clear;
        }
    }




    public void Update()
    {
        int level = 0;
        foreach (var coil in coilChecks)
        {
            if (coil != null && coil.isActiveAndEnabled) level = Mathf.Max(level, coil.InducationLevel);
        }
        ChangeColor(level);
    }

    private void ChangeColor(int level)
    {
        _currentLevel = level;
        if (level <= 0) return;
        for (int i = 0; i < Mathf.Min(level, inductions.Length); i++)
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
