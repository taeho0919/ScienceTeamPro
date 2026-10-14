using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded {get; private set;}
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask groundLayer;
    



    public bool IsGround()
    {
        isGrounded = Physics2D.OverlapBox(transform.position, boxSize, 0f, groundLayer);
        return isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
