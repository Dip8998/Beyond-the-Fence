using BTF.Player;

namespace BTF.FirstNB
{
    public class FirstNeighborController
    {
        private readonly FirstNeighborModel model;

        public FirstNeighborController(FirstNeighborModel model)
        {
            this.model = model;
        }

        public FirstNeighborState GetState() => model.State;

        public void OnPlayerInteract()
        {
            if (model.State == FirstNeighborState.Idle)
            {
                model.AskForHelp();
            }
        }

        public void ResolveConflict()
        {
            model?.ResolveConflict();
        }

        public void GiveKey(PlayerController player)
        {
            if (model.State != FirstNeighborState.ConflictResolved)
                return;

            player.ReceiveFenceKey();
        }
    }
}
