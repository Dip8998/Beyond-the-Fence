namespace BTF.Core.StateMachine
{
    public interface IState<T>
    {
        void SetOwner(T Owner);
        void OnStateEnter();
        void Update();
        void OnStateExit();
    }
}
