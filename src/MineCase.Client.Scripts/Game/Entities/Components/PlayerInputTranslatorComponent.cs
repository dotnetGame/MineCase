using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MineCase.Client.Messages;
using MineCase.Engine;
using MineCase.Engine.Messages;
using UnityEngine;

namespace MineCase.Client.Game.Entities.Components
{
    /// <summary>
    /// 玩家输入翻译组件
    /// </summary>
    public class PlayerInputTranslatorComponent : Component<PlayerEntity>, IHandle<Update>
    {
        private bool _isEnableCursorLock = true;
        private bool _isCursorLocked = false;

        private IEventAggregator _eventAggregator;

        public PlayerInputTranslatorComponent(string name = "playerInputTranslator")
            : base(name)
        {
        }

        protected override void OnAttached()
        {
            _eventAggregator = ServiceProvider.Resolve<IEventAggregator>();
        }

        void IHandle<Update>.Handle(Update message)
        {
            if (_isEnableCursorLock)
                UpdateMouseLock();

            if (Cursor.lockState == CursorLockMode.Locked)
            {
                var horizontalMovement = Input.GetAxis("Horizontal");
                var verticalMovement = Input.GetAxis("Vertical");

                if (horizontalMovement != 0 || verticalMovement != 0)
                    AttachedObject.Tell(new PositionMove { Horizontal = horizontalMovement, Vertical = verticalMovement });

                var mouseX = Input.GetAxis("Mouse X");
                var mouseY = Input.GetAxis("Mouse Y");
                if (mouseX != 0 || mouseY != 0)
                    _eventAggregator.PublishOnCurrentThread(new CursorMoveMessage { DeltaX = mouseX, DeltaY = mouseY });
            }
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
    }
}
