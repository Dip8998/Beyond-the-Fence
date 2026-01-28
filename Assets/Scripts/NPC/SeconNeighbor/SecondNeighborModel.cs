namespace BTF.SeconNB
{
    public class SecondNeighborModel
    {
        public SecondNeighborState State { get; set; } = SecondNeighborState.Idle;

        public void Lie() => State = SecondNeighborState.Lying;
        public void Confess() => State = SecondNeighborState.Confessed;
    }

}