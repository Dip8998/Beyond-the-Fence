using BTF.Player;

namespace BTF.UI.Health
{
    public sealed class PlayerHealthUIController
    {
        private readonly PlayerHealthUIModel model;
        private readonly PlayerHealthUIView view;
        private readonly PlayerController player;

        public PlayerHealthUIController(
            PlayerHealthUIModel model,
            PlayerHealthUIView view,
            PlayerController player)
        {
            this.model = model;
            this.view = view;
            this.player = player;

            Refresh();
            player.OnHealthChanged += Refresh;
        }

        private void Refresh()
        {
            model.Set(
                player.GetCurrentHP(),
                player.GetMaxHP()
            );

            view.SetFill(model.Normalized);
        }

        public void Dispose()
        {
            player.OnHealthChanged -= Refresh;
        }
    }
}
