using System;
using System.Collections.Generic;

namespace BTF.Core.StateMachine
{
    public abstract class GenericStateMachine<T>
    {
        protected T Owner;
        protected IState<T> CurrentState;
        protected Dictionary<Enum, IState<T>> States = new();

        protected GenericStateMachine(T owner) => Owner = owner;

        public void Update() => CurrentState?.Update();

        public void ChangeStates(Enum newState)
        {
            CurrentState?.OnStateExit();
            CurrentState = States[newState];
            CurrentState?.OnStateEnter();
        }

        protected void SetOwnerToStates()
        {
            foreach (var state in States.Values)
            {
                state.SetOwner(Owner);  
            }
        }
    }
}
