using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase
{
    public enum ClickAction
    {
        InvalidClick,
        LeftMouseClick,
        RightMouseClick,
        ShiftLeftMouseClick,
        ShiftRightMouseClick,
        LeftMouseDragBegin,
        LeftMouseAddSlot,
        LeftMouseDragEnd,
        RightMouseDragBegin,
        RightMouseAddSlot,
        RightMouseDragEnd,
        MiddleMouseDragBegin,
        MiddleMouseAddSlot,
        MiddleMouseDragEnd,
        DoubleClick,
        NumberKey1,
        NumberKey2,
        NumberKey3,
        NumberKey4,
        NumberKey5,
        NumberKey6,
        NumberKey7,
        NumberKey8,
        NumberKey9,
        MiddleClick,
        DropKey,
        CtrlDropKey,
        LeftClickNoting,
        RightClickNothing
    }
}
