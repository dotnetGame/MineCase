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

        public CreatureAnimation(CreatureAction begin, CreatureAction end, int time)
        {
            _begin = begin;
            _end = end;
            _time = time;
            _curtime = 0;
            _step = (_end - _begin) / _time;
        }

        public abstract CreatureAction Step(int time);
    }

    public class CreatureAnimationLinear : CreatureAnimation
    {
        public CreatureAnimationLinear(CreatureAction begin, CreatureAction end, int time)
            : base(begin, end, time)
        {
        }

        public override CreatureAction Step(int time)
        {
            CreatureAction result = new CreatureAction();
            _curtime += time;
            result = _begin + _step * _curtime;
            return result;
        }
    }
}
