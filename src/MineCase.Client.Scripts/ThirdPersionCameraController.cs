using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MineCase.Client
{
    [RequireComponent(typeof(Camera))]
    public class ThirdPersionCameraController : MonoBehaviour
    {
        private Camera _camera;
        private Transform _player;
        private Vector3 _offset;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _offset = transform.position - _player.position;
        }

        private void LateUpdate()
        {
            transform.position = _offset + _player.position;
        }
    }
}
