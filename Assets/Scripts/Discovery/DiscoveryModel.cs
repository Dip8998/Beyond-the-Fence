using System.Collections.Generic;

namespace BTF.Discovery
{
    public sealed class DiscoveryModel
    {
        private readonly HashSet<DiscoverableItem> discovered = new();

        public bool IsDiscovered(DiscoverableItem item)
            => discovered.Contains(item);

        public void MarkDiscovered(DiscoverableItem item)
            => discovered.Add(item);
    }
}
