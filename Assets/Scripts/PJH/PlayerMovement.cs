using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private GroundCheck groundChecker;

    [SerializeField] private float moveSpeed, jumpPower;

    private Vector2 dir;


    private void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        
        Vector3 moveVector = new Vector3(hAxis, 0, 0);
        
        transform.Translate(moveVector * moveSpeed * Time.deltaTime);
    }


    private void FixedUpdate()
    {

        if (groundChecker.IsGround())
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Jump();
            }
        }
        
    }


    private void Jump()
    {
        rigid.AddForceY(jumpPower, ForceMode2D.Impulse);
    }
    
}
