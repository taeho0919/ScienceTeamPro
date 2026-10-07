using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class PlayerAttack : MonoBehaviour
{
    
    private Sequence _sequence;
    private Vector3 relativePos;

    private Vector3 originPos;

    private void Start()
    {
        originPos = transform.position;
        _sequence = DOTween.Sequence();
    }

    private void Attack()
    {
        _sequence.Kill();
        
        Vector3 attackDir = relativePos - transform.position;
        attackDir.Normalize();
        _sequence.Append(transform.DOMove(transform.position + attackDir * 3 , 0.3f));
        _sequence.AppendCallback(() => transform.DOMove(originPos, 0.3f));
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
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
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(lookPos);
        relativePos = worldPos - (Vector2)transform.position;
        
        float rotation = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation - 90);
    }
}
