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
    public class InputController : SmartBehaviour
    {
        public IEventAggregator EventAggregator { get; set; }

        private bool _isEnableCursorLock = true;
        private bool _isCursorLocked = false;

        private void Start()
        {
            _isCursorLocked = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void UpdateMouseLock()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
                _isCursorLocked = false;
            else if (Input.GetMouseButtonUp(0))
                _isCursorLocked = true;

            if (_isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!_isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        private void FixedUpdate()
        {
            if (_isEnableCursorLock)
                UpdateMouseLock();

            if (Cursor.lockState == CursorLockMode.Locked)
            {
                var horizontalMovement = Input.GetAxis("Horizontal");
                var verticalMovement = Input.GetAxis("Vertical");

                if (horizontalMovement != 0 || verticalMovement != 0)
                    EventAggregator.PublishOnCurrentThread(new PositionMoveMessage { Horizontal = horizontalMovement, Vertical = verticalMovement });

                var mouseX = Input.GetAxis("Mouse X");
                var mouseY = Input.GetAxis("Mouse Y");
                if (mouseX != 0 || mouseY != 0)
                    EventAggregator.PublishOnCurrentThread(new CursorMoveMessage { DeltaX = mouseX, DeltaY = mouseY });
            }
        }
    }
}
