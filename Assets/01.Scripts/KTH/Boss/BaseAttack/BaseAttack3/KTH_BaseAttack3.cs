using UnityEngine;

public class KTH_BaseAttack3 : MonoBehaviour
{
    [SerializeField] private KTH_Base3 lightning;
    [SerializeField] private int minCount = 3;
    [SerializeField] private int maxCount = 5;
    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 5f;
    [SerializeField] private KTH_BaseSoundSO soundSO;

    private void OnEnable()
    {
        PlaySound();
        int count = Random.Range(minCount, maxCount + 1);
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(Random.Range(minX, maxX), minY, 0f);
            KTH_Base3 bolt = Instantiate(lightning, pos, Quaternion.identity);
            bolt.FitHeight(minY, maxY);
        }
    }

    private void PlaySound()
    {
        if (soundSO == null) return;
        soundSO.Play(soundSO.baseAttack3);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = new Vector3((minX + maxX) * 0.5f, (minY + maxY) * 0.5f, 0f);
        Vector3 size = new Vector3(maxX - minX, maxY - minY, 0f);
        Gizmos.DrawWireCube(center, size);
    }
}
