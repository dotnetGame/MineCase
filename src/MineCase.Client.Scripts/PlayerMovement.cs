using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Client.Messages;
using MineCase.Engine;
using MineCase.World;
using UnityEngine;

namespace MineCase.Client
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class PlayerMovement : SmartBehaviour, IHandleEvent<PositionMoveMessage>
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

        void IHandleEvent<PositionMoveMessage>.Handle(PositionMoveMessage message)
        {
            var velocity = (transform.forward * message.Vertical + transform.right * message.Horizontal) * Speed;
            _animator.SetFloat("Speed", velocity.magnitude);
            _animator.SetFloat("PosX", message.Horizontal);
            _animator.SetFloat("PosY", message.Vertical);

            // velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;
        }

        private ChunkWorldPos? _lastChunkWorldPos;

        private void FixedUpdate()
        {
            if (_lookAt is Vector3 lookAt)
            {
                var forward = lookAt - transform.position;
                forward.y = 0;
                _rigidbody.transform.rotation = Quaternion.LookRotation(forward);
            }

            var position = transform.position;
            var chunkWorldPos = new EntityWorldPos(position.x, position.y, position.z).ToChunkWorldPos();
            if (_lastChunkWorldPos != chunkWorldPos)
            {
                _lastChunkWorldPos = chunkWorldPos;
            }
        }

        private Vector3? _lookAt;

        public void SetLookAtPosition(Vector3 lookAt)
        {
            _lookAt = lookAt;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (_lookAt is Vector3 lookAt)
            {
                _animator.SetLookAtWeight(1);
                _animator.SetLookAtPosition(lookAt);
            }
        }
    }
}
