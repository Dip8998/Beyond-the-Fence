namespace BTF.Villager
{
    public class VillagerBoatQuestModel
    {
        public int RequiredBoatWood { get; }
        public int RequiredBridgeWood { get; }
        public int RequiredGear { get; }

        public bool HasRealizedGear { get; private set; }
        public bool IsBridgeBuilt { get; private set; }

        public VillagerBoatQuestState State { get; private set; }

        public VillagerBoatQuestModel(
            int boatWood,
            int bridgeWood,
            int gear)
        {
            RequiredBoatWood = boatWood;
            RequiredBridgeWood = bridgeWood;
            RequiredGear = gear;
            State = VillagerBoatQuestState.Inactive;
        }

        public void Activate() => State = VillagerBoatQuestState.Active;
        public void StartQuest() => State = VillagerBoatQuestState.InProgress;
        public void RealizeGear() => HasRealizedGear = true;
        public void MarkBridgeBuilt() => IsBridgeBuilt = true;
        public void BoatReady() => State = VillagerBoatQuestState.BoatReady;
        public void Complete() => State = VillagerBoatQuestState.Completed;
    }
}
