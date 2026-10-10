using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class KTH_BossBase : MonoBehaviour
{
    [SerializeField] private List<KTH_BossPattern> patterns = new List<KTH_BossPattern>();
    [SerializeField] private float patternDelay = 1f;
    [SerializeField] private bool preventRepeat = true;

    private KTH_BossPattern curPattern;
    private KTH_BossPattern lastPattern;

    private void Awake()
    {
        foreach (var pattern in patterns)
        {
            pattern.gameObject.SetActive(false);
            pattern.OnPatternEnd += HandlePatternEnd;
        }
    }

    private void Start()
    {
        StartCoroutine(NextPatternRoutine());
    }

    private void HandlePatternEnd(KTH_BossPattern pattern)
    {
        if (pattern != curPattern) return;
        curPattern = null;
        StartCoroutine(NextPatternRoutine());
    }

    private IEnumerator NextPatternRoutine()
    {
        yield return new WaitForSeconds(patternDelay);
        ChangePattern();
    }

    public void ChangePattern()
    {
        if (patterns.Count == 0) return;

        KTH_BossPattern next;
        do
        {
            next = patterns[Random.Range(0, patterns.Count)];
        } while (preventRepeat && patterns.Count > 1 && next == lastPattern);

        lastPattern = next;
        curPattern = next;
        curPattern.gameObject.SetActive(true);
    }

    public void StopPatterns()
    {
        StopAllCoroutines();
        if (curPattern != null) curPattern.gameObject.SetActive(false);
        curPattern = null;
    }
}
