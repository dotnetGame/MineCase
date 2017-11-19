using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Engine.Messages;
using MineCase.World;
using UnityEngine;

namespace MineCase.Client.Game.Entities.Components
{
    /// <summary>
    /// 玩家移动和动作
    /// </summary>
    public class PlayerMotorComponent : Component<PlayerEntity>,
        IHandle<Update>, IHandle<LookAt>, IHandle<PositionMove>, IHandle<OnAnimatorIK>
    {
        public static readonly DependencyProperty<float> SpeedProperty =
            DependencyProperty.Register(nameof(Speed), typeof(PlayerMotorComponent), new PropertyMetadata<float>(0.3f));

        /// <summary>
        /// 移动速度
        /// </summary>
        public float Speed
        {
            get => AttachedObject.GetValue(SpeedProperty);
            set => AttachedObject.SetLocalValue(SpeedProperty, value);
        }

        private Rigidbody _rigidbody;
        private Animator _animator;

        private Vector3? _lookAt;
        private ChunkWorldPos? _lastChunkWorldPos;

        public PlayerMotorComponent(string name = "playerMotor")
            : base(name)
        {
        }

        protected override void OnAttached()
        {
            _rigidbody = AttachedObject.GetUnityComponent<Rigidbody>();
            _animator = AttachedObject.GetUnityComponent<Animator>();
        }

        void IHandle<Update>.Handle(Update message)
        {
            if (_lookAt is Vector3 lookAt)
            {
                var forward = lookAt - AttachedObject.transform.position;
                forward.y = 0;
                _rigidbody.transform.rotation = Quaternion.LookRotation(forward);
            }

            var position = AttachedObject.transform.position;
            var chunkWorldPos = new EntityWorldPos(position.x, position.y, position.z).ToChunkWorldPos();
            if (_lastChunkWorldPos != chunkWorldPos)
            {
                _lastChunkWorldPos = chunkWorldPos;
            }
        }

        void IHandle<LookAt>.Handle(LookAt message)
        {
            _lookAt = message.Position;
        }

        void IHandle<PositionMove>.Handle(PositionMove message)
        {
            var velocity = (AttachedObject.transform.forward * message.Vertical + AttachedObject.transform.right * message.Horizontal) * Speed;
            _animator.SetFloat("Speed", velocity.magnitude);
            _animator.SetFloat("PosX", message.Horizontal);
            _animator.SetFloat("PosY", message.Vertical);

            // velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;
        }

        void IHandle<OnAnimatorIK>.Handle(OnAnimatorIK message)
        {
            if (_lookAt is Vector3 lookAt)
            {
                _animator.SetLookAtWeight(1);
                _animator.SetLookAtPosition(lookAt);
            }
        }
    }
}
