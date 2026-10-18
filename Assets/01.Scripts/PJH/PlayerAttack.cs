using System;
using _01.Scripts.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class PlayerAttack : MonoBehaviour
{
    
    [SerializeField] private float wtmTime, endValue;
    [SerializeField] private SpriteRenderer magenticRenderer;
    
    private Sequence _sequence;
    private Vector3 relativePos;

    private Vector3 originPos;

    private bool _isAttacking;
    private bool _isReturning;
    private void Start()
    {
        originPos = transform.position;
    }

    private void Attack()
    {
        _isAttacking = true;
        _isReturning = false;
        _sequence = DOTween.Sequence().SetLink(gameObject);
        
        Vector3 attackDir = relativePos - transform.position;
        attackDir.Normalize();
        _sequence.Append(transform.DOScaleY(endValue, wtmTime));
        _sequence.AppendCallback(() => _isReturning = true);
        _sequence.Append(transform.DOScaleY(1f, wtmTime)).OnComplete(() =>
        {
            _isAttacking = false;
            _isReturning = false;
        });   
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


    public void HandleMagnetHit(Collider2D collision)
    {
   
        
        
        if (!_isAttacking || _isReturning) return;

        if (!collision.CompareTag("CoilTopDown"))
            return;
        
        _isReturning = true;
        _sequence?.Kill(false);
        _sequence = DOTween.Sequence().SetLink(gameObject);
        
        _sequence.Append(transform.DOScaleY(1f, wtmTime));

        _sequence.OnComplete(() =>
        {
            _isAttacking = false;
            _isReturning = false;
        });
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
