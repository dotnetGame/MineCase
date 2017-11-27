using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Persistence;
using MineCase.Server.Persistence.Components;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.User
{
    [PersistTableName("nonAuthenticatedUser")]
    [Reentrant]
    internal class NonAuthenticatedUserGrain : PersistableDependencyObject, INonAuthenticatedUser
    {
        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override async Task InitializePreLoadComponent()
        {
            await SetComponent(new StateComponent<StateHolder>());
        }

        public Task<Guid> GetUUID() => Task.FromResult(State.UUID);

        public async Task<IUser> GetUser()
        {
            var user = GrainFactory.GetGrain<IUser>(State.UUID);
            await user.SetName(this.GetPrimaryKeyString());
            await WriteStateAsync();
            return user;
        }

        internal class StateHolder
        {
            public Guid UUID { get; set; }

            public StateHolder()
            {
            }

            public StateHolder(InitializeStateMark mark)
            {
                UUID = Guid.NewGuid();
            }
        }
    }
}
