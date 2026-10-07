using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class PlayerAttack : MonoBehaviour
{
    
    [SerializeField] private float wtmTime, endValue;
    
    private Sequence _sequence;
    private Vector3 relativePos;

    private Vector3 originPos;

    private bool _isAttacking;

    private void Start()
    {
        originPos = transform.position;
    }

    private void Attack()
    {
        _isAttacking = true;
        _sequence = DOTween.Sequence();
        
        Vector3 attackDir = relativePos - transform.position;
        attackDir.Normalize();
        _sequence.Append(transform.DOScaleY(endValue, wtmTime));
        _sequence.Append(transform.DOScaleY(1f, wtmTime)).OnComplete(() => _isAttacking = false); 
        
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!_isAttacking)
                Attack();
        }
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        if(context.performed) HandleLook(context.ReadValue<Vector2>());
        else if(context.canceled) HandleLook(Vector2.zero);
    }



    public void HandleLook(Vector2 lookPos)
    {
        if(_isAttacking) return;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(lookPos);
        relativePos = worldPos - (Vector2)transform.position;
        
        float rotation = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation - 90);
    }
}
