namespace BTF.Boat
{
    public enum BoatState
    {
        Idle,
        Travelling,
        Arrived
    }

    public class BoatModel
    {
        public BoatState State { get; private set; } = BoatState.Idle;

        public void StartTravel() => State = BoatState.Travelling;
        public void Arrive() => State = BoatState.Arrived;
    }
}
