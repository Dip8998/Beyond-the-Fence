namespace BTF.FirstNB
{
    public class FirstNeighborModel
    {
        public FirstNeighborState State { get; set; }

        public FirstNeighborModel()
        {
            State = FirstNeighborState.Idle;
        }

        public void AskForHelp() => State = FirstNeighborState.AskedForHelp;
        public void ResolveConflict() => State = FirstNeighborState.ConflictResolved;
    }
}
