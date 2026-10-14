using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private GroundCheck groundChecker;

    [SerializeField] private float moveSpeed, jumpPower, lowJumpMultiplier;
    [SerializeField] private float fallGravityIncrease, maxGravityScale;

    private Vector2 dir;
    private float defaultGravityScale;

    private Animator animator;
    
    [Header("Animation Components")]
    private readonly int idleHash = Animator.StringToHash("idle");
    private readonly int jumpHash = Animator.StringToHash("jump");
    private int walkHash = Animator.StringToHash("IsMove");

    private void Awake()
    {
        defaultGravityScale = rigid.gravityScale;
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        
        if (groundChecker.IsGround())
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame||Keyboard.current.wKey.wasPressedThisFrame)
            {
                Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        rigid.linearVelocityX = moveSpeed * dir.x;
        
        if (rigid.linearVelocity.y < 0)
        {
            rigid.gravityScale = Mathf.Min(rigid.gravityScale + fallGravityIncrease * Time.fixedDeltaTime, maxGravityScale);
            return;
        }

        rigid.gravityScale = defaultGravityScale;

        if (rigid.linearVelocity.y > 0 && !Input.GetKeyDown(KeyCode.Space)||!Input.GetKeyDown(KeyCode.W))
        {
            rigid.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }


    private void Jump()
    {
        
        rigid.AddForceY(jumpPower, ForceMode2D.Impulse);
    }

    private void OnMove(InputValue value)
    {
        dir =  value.Get<Vector2>();

        if (value.isPressed)
        {
            animator.SetBool(walkHash, true);
        }
        else
        {
            animator.SetBool(walkHash, false);
        }
        
    }
}
