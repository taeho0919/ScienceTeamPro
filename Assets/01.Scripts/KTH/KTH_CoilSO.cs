using UnityEngine;

[CreateAssetMenu(fileName = "CoilSO", menuName = "KTH/CoilSO")]
public class KTH_CoilSO : ScriptableObject
{
    [Header("데미지")]
    public int baseDamage;
    public int bonusDamage;

    [Header("유도 단계")]
    public int maxLevel;
    public float decayDelay = 4.5f;
    public float decayInterval = 1.5f;

    [Header("공격")]
    public float attackCooldown = 0.4f;
}
