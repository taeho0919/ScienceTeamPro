using System;
using _01.Scripts.Core;
using UnityEngine;

public class MagnetHeadCollision : MonoBehaviour
{
    [SerializeField] private PlayerAttack magenetAttack;

    private void OnTriggerEnter2D(Collider2D other)
    {
        magenetAttack.HandleMagnetHit(other);
    }
}
