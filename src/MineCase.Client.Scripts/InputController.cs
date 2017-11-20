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

        private float _mouseX;
        private float _mouseY;

        private void Start()
        {
            _mouseX = Input.mousePosition.x;
            _mouseY = Input.mousePosition.y;
        }

        private void FixedUpdate()
        {
            var horizontalMovement = Input.GetAxis("Horizontal");
            var verticalMovement = Input.GetAxis("Vertical");

            if (horizontalMovement != 0 || verticalMovement != 0)
                EventAggregator.PublishOnCurrentThread(new PositionMoveMessage { Horizontal = horizontalMovement, Vertical = verticalMovement });

            var mouseX = Input.mousePosition.x;
            var mouseY = Input.mousePosition.y;
            var deltaMouseX = mouseX - _mouseX;
            var deltaMouseY = mouseY - _mouseY;
            if (deltaMouseX != 0 || deltaMouseY != 0)
                EventAggregator.PublishOnCurrentThread(new CursorMoveMessage { DeltaX = deltaMouseX, DeltaY = deltaMouseY });
            _mouseX = mouseX;
            _mouseY = mouseY;
        }
    }
}
