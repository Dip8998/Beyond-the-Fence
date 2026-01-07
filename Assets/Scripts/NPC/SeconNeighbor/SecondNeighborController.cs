namespace BTF.SeconNB
{
    public class SecondNeighborController
    {
        private readonly SecondNeighborModel model;

        public SecondNeighborController(SecondNeighborModel model)
        {
            this.model = model;
        }

        public SecondNeighborState GetState() => model.State;

        public void OnPlayerInteract(bool firstNeighborAsked)
        {
            if (!firstNeighborAsked) return;

            if (model.State == SecondNeighborState.Idle)
                model.Lie();
            else if (model.State == SecondNeighborState.Lying)
                model.Confess();
        }
    }
}
