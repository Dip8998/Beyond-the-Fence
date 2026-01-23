namespace BTF.Discovery
{
    public sealed class DiscoveryController
    {
        private readonly DiscoveryModel model;
        private readonly DiscoveryUIView view;

        public DiscoveryController(
            DiscoveryModel model,
            DiscoveryUIView view)
        {
            this.model = model;
            this.view = view;
        }

        public void TryDiscover(DiscoverableItem item)
        {
            if (model.IsDiscovered(item))
                return;

            model.MarkDiscovered(item);
            view.Show($"New Item: {item}");
        }

        public void Notify(string message)
        {
            view.Show(message);
        }
    }
}
