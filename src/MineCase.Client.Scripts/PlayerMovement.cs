using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Client.Messages;
using MineCase.Engine;
using UnityEngine;

namespace MineCase.Client
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class PlayerMovement : SmartBehaviour, IHandle<PositionMoveMessage>
    {
        public float Speed = 0.3f;

        public IEventAggregator EventAggregator { get; set; }

        private Rigidbody _rigidbody;
        private Animator _animator;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            EventAggregator.Subscribe(this);
        }

        void IHandle<PositionMoveMessage>.Handle(PositionMoveMessage message)
        {
            var velocity = (transform.forward * message.Vertical + transform.right * message.Horizontal) * Speed;
            _animator.SetFloat("Speed", velocity.magnitude);
            _animator.SetFloat("PosX", message.Horizontal);
            _animator.SetFloat("PosY", message.Vertical);

            // velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;
        }

        private void FixedUpdate()
        {
            var forward = _lookAt - transform.position;
            forward.y = 0;
            _rigidbody.transform.rotation = Quaternion.LookRotation(forward);
        }

        private Vector3 _lookAt;

        public void SetLookAtPosition(Vector3 lookAt)
        {
            _lookAt = lookAt;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            _animator.SetLookAtWeight(1);
            _animator.SetLookAtPosition(_lookAt);
        }
    }
}
