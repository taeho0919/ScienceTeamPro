using UnityEngine;

public class KTH_BaseAttack3 : MonoBehaviour
{
    [SerializeField] private GameObject lightning;
    [SerializeField] private int minCount = 3;
    [SerializeField] private int maxCount = 5;
    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;

    private void OnEnable()
    {
        int count = Random.Range(minCount, maxCount + 1);
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = lightning.transform.position;
            pos.x = Random.Range(minX, maxX);
            Instantiate(lightning, pos, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 pos = transform.position;
        Gizmos.DrawLine(new Vector3(minX, pos.y, 0f), new Vector3(maxX, pos.y, 0f));
    }
}
