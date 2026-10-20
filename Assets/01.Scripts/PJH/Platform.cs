using System;
using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Collider2D playerCollider;
    
    private PlayerMovement _onPlayer;
    private Collider2D _collider;
    

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }


    private void Update()
    {
        if (_onPlayer == null) return;

        if (_onPlayer.dir.y < -0.1f)
        {
            Physics2D.IgnoreCollision(_collider, playerCollider, true);
            StartCoroutine(ColliderCoroutine(playerCollider));
        }
    }
    
    private IEnumerator ColliderCoroutine(Collider2D player)
    {
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(_collider, player, false);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement player))
        {
            _onPlayer = player;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement player))
        {
            _onPlayer = null;
        }
    }
}
