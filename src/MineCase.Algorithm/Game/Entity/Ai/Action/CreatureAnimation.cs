using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Algorithm.Game.Entity.Ai.Action
{
    public abstract class CreatureAnimation
    {
        protected CreatureAction _begin;
        protected CreatureAction _end;
        protected int _time;
        protected int _curtime;
        protected CreatureActionDistance _step;

        public CreatureAnimation(CreatureAction end, int time)
        {
            _end = end;
            _time = time;
            _curtime = 0;
            _step = (_end - _begin) / _time;
        }

        public abstract CreatureAction GetCreatureAction();

        public void SetBeginAction(CreatureAction action)
        {
            _begin = action;
        }

        public bool Step(int time)
        {
            _curtime += time;
            return _curtime < _time;
        }
    }

    public class CreatureAnimationLinear : CreatureAnimation
    {
        public CreatureAnimationLinear(CreatureAction end, int time)
            : base(end, time)
        {
        }

        public override CreatureAction GetCreatureAction()
        {
            CreatureAction result = new CreatureAction();
            result = _begin + _step * _curtime;
            return result;
        }
    }
}
