using BTF.Interfaces;
using System;
using System.Collections.Generic;

namespace BTF.StateMachine
{
    public class GenericStateMachine<T>
    {
        protected T Owner;
        protected IState<T> currentState;
        protected Dictionary<Enum, IState<T>> States = new();

        public GenericStateMachine(T owner) => Owner = owner;

        public void ChangeState(Enum newState)
        {
            currentState?.OnStateExit();
            currentState = States[newState];
            currentState?.OnStateEnter();
        }

        public void Update() => currentState?.Update();

        public void SetOwner()
        {
            foreach (var state in States.Values)
            {
                state.SetOwner(Owner);
            }
        }
    }
}
