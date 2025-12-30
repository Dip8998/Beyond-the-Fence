namespace BTF.Interfaces
{
    public interface IState<T>
    {
        void SetOwner(T owner);
        void OnStateEnter();
        void Update();
        void OnStateExit();
    }
}
