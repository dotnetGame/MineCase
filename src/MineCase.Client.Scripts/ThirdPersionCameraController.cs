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
    public class ThirdPersionCameraController : SmartBehaviour, IHandle<CursorMoveMessage>
    {
        public float RotateFactor = 0.1f;
        public float Backward = 2;

        public IEventAggregator EventAggregator { get; set; }

        private Camera _camera;
        private Transform _player;
        private PlayerMovement _playerAnim;
        private float _height;
        private Quaternion _cameraRotation;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            var player = GameObject.FindGameObjectWithTag("Player");
            _player = player.transform;
            _playerAnim = player.GetComponent<PlayerMovement>();
            _height = (transform.position - _player.position).y;
            _cameraRotation = transform.localRotation;
            UpdateLookAt();

            EventAggregator.Subscribe(this);
        }

        private void LateUpdate()
        {
            transform.position = new Vector3(0, _height, 0) + _player.position + _player.forward.normalized * -Backward;
        }

        void IHandle<CursorMoveMessage>.Handle(CursorMoveMessage message)
        {
            var y = message.DeltaX * RotateFactor;
            var x = -message.DeltaY * RotateFactor;
            _cameraRotation *= Quaternion.Euler(x, y, 0);
            transform.localRotation = _cameraRotation;
            UpdateLookAt();
        }

        private void UpdateLookAt()
        {
            var lookAt = transform.forward * 50 + transform.position;
            _playerAnim.SetLookAtPosition(lookAt);
            transform.localRotation = Quaternion.LookRotation(lookAt - transform.position);
        }
    }
}
