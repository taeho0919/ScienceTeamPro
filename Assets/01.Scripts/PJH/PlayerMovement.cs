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

    private bool isgrounded;
    public Vector2 dir;
    private float defaultGravityScale;

    private Animator animator;
    
    [Header("Animation Components")]
    private int walkHash = Animator.StringToHash("IsMove");
    private int isGroundHash = Animator.StringToHash("IsGround");
    private int y_velocityHash = Animator.StringToHash("YVelocity");

    private void Awake()
    {
        defaultGravityScale = rigid.gravityScale;
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        
        isgrounded = groundChecker.IsGround();

        
        animator.SetFloat(y_velocityHash, rigid.linearVelocity.y);
        animator.SetBool(isGroundHash, isgrounded);
        
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

        if (MathF.Abs(dir.x) > 0.1f)
        {
            animator.SetBool(walkHash, true);
        }

        else
        {
            animator.SetBool(walkHash, false);
        }
    }

    
}
