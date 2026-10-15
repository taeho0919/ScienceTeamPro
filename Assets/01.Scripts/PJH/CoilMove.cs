using System;
using _01.Scripts.Core;
using _01.Scripts.Core.Logger;
using UnityEngine;

namespace _01.Scripts.PJH
{
    public class CoilMove : MonoBehaviour
    {
        [Header("Coil Setting")] 
        [SerializeField] private float maxY;
        [SerializeField] private float minY;
        [SerializeField] private float moveDuration;
        [SerializeField] private float speed;
    
        private float _direction = 1f;  


        public void Update()
        {
            transform.position += new Vector3(0, _direction * speed * Time.deltaTime, 0f);

            if (Mathf.Approximately(transform.position.y, minY) || Mathf.Approximately(transform.position.y, maxY))
                _direction *= -1f;
        }
    }
}
