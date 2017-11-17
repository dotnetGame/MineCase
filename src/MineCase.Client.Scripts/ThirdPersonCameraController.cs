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
    public class ThirdPersonCameraController : SmartBehaviour, IHandleEvent<CursorMoveMessage>
    {
        public float RotateFactor = 0.1f;
        public float Distance;
        public Vector3 Forward;

        public IEventAggregator EventAggregator { get; set; }

        private Camera _camera;
        private Transform _player;
        private PlayerMovement _playerMovement;
        private Vector3 _angles;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            var player = GameObject.FindGameObjectWithTag("Player");
            _player = player.transform;
            _playerMovement = player.GetComponent<PlayerMovement>();

            EventAggregator.Subscribe(this);
        }

        private void LateUpdate()
        {
            transform.eulerAngles = _angles;
            transform.position = _player.transform.position - (transform.localRotation * Forward * Distance);
        }

        void IHandleEvent<CursorMoveMessage>.Handle(CursorMoveMessage message)
        {
            var angles = transform.localEulerAngles;
            angles.y += message.DeltaX * RotateFactor;
            angles.x += -message.DeltaY * RotateFactor;
            _angles = angles;

            UpdateLookAt();
        }

        private void UpdateLookAt()
        {
            var lookAt = Quaternion.Euler(_angles) * Forward * 50 + transform.position;
            _playerMovement.SetLookAtPosition(lookAt);
        }
    }
}
