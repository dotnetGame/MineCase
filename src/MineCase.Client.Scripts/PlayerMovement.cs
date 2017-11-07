using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using UnityEngine;

namespace MineCase.Client
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class PlayerMovement : SmartBehaviour
    {
        public float Speed = 1.0f;

        private Rigidbody _rigidbody;
        private Animator _animator;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            var horizontalMovement = Input.GetAxis("Horizontal");
            var verticalMovement = Input.GetAxis("Vertical");

            var velocity = (transform.forward * verticalMovement + transform.right * horizontalMovement) * Speed;
            _animator.SetFloat("Speed", velocity.magnitude);
            _animator.SetFloat("PosX", horizontalMovement);
            _animator.SetFloat("PosY", verticalMovement);

            // velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;
        }
    }
}
