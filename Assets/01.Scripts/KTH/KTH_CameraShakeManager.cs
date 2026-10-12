using System;
using Unity.Cinemachine;
using UnityEngine;

public class KTH_CameraShakeManager : MonoBehaviour
{
    public static KTH_CameraShakeManager Instance { get; private set; }

    private CinemachineImpulseSource  impulseSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void Shake(float force = 1f)
    {
        impulseSource.GenerateImpulse(force);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Shake();
        }
    }
}
