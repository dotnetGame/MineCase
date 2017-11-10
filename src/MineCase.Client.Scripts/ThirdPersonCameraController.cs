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
    [RequireComponent(typeof(Camera))]
    public class ThirdPersonCameraController : SmartBehaviour, IHandle<CursorMoveMessage>
    {
        public float RotateFactor = 0.1f;
        public float Backward = 2;

        public IEventAggregator EventAggregator { get; set; }

        private Camera _camera;
        private Transform _player;
        private PlayerMovement _playerMovement;
        private float _height;

        private float _rotateX;
        private float _rotateY;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            var player = GameObject.FindGameObjectWithTag("Player");
            _player = player.transform;
            _playerMovement = player.GetComponent<PlayerMovement>();
            _height = (transform.position - _player.position).y;
            _rotateX = transform.localRotation.eulerAngles.x;
            _rotateY = transform.localRotation.eulerAngles.y;
            UpdateLookAt();

            EventAggregator.Subscribe(this);
        }

        private void LateUpdate()
        {
            transform.position = new Vector3(0, _height, 0) + _player.position + GetForward() * -Backward;
        }

        private Vector3 GetForward()
        {
            var forward = transform.forward;
            forward.y = 0;
            return forward.normalized;
        }

        void IHandle<CursorMoveMessage>.Handle(CursorMoveMessage message)
        {
            _rotateY += message.DeltaX * RotateFactor;
            _rotateX += -message.DeltaY * RotateFactor;
            transform.localRotation = Quaternion.Euler(_rotateX, _rotateY, 0);
            UpdateLookAt();
        }

        private void UpdateLookAt()
        {
            var lookAt = transform.forward * 50 + transform.position;
            _playerMovement.SetLookAtPosition(lookAt);
        }
    }
}
