using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class KTH_LightBallSpawn : MonoBehaviour
{
    [SerializeField]private GameObject[] lightBall;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private KTH_BaseSoundSO soundSO;
    private SplineAnimate splineAnim;
    public Action StartMove;

    private void Awake()
    {
        splineAnim = GetComponentInParent<SplineAnimate>();
        GetComponentInParent<Rigidbody2D>().freezeRotation = true;
    }

    private void OnEnable()
    {
        splineAnim.Pause();
        PlaySound();
        StartCoroutine(Spawn());
    }

    private void PlaySound()
    {
        if (soundSO == null) return;
        soundSO.Play(soundSO.boss1Pattern1);
    }

    private void OnDisable()
    {
        foreach (var ball in lightBall)
        {
            ball.SetActive(false);
        }
        splineAnim.Play();
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < lightBall.Length; i++)
        {
            lightBall[i].SetActive(true);
            yield return new WaitForSeconds(spawnInterval);
        }
        
        StartMove?.Invoke();
    }
}
