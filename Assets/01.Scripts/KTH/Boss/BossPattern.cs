using System.Collections;
using UnityEngine;

public class BossPattern : KTH_BossPattern
{
    [SerializeField] private float duration = 2f;

    protected override void StartPattern()
    {
        StartCoroutine(PatternRoutine());
    }
    private IEnumerator PatternRoutine()
    {
        yield return new WaitForSeconds(duration);
        
        Finish();
    }
}
