namespace BTF.Villager
{
    public class VillagerBoatQuestModel
    {
        public int RequiredWood { get; }
        public int RequiredGear { get; }

        public VillagerBoatQuestState State { get; private set; }

        public VillagerBoatQuestModel(int wood, int gear)
        {
            RequiredWood = wood;
            RequiredGear = gear;
            State = VillagerBoatQuestState.Inactive;
        }

        public void Activate() => State = VillagerBoatQuestState.Active;
        public void StartQuest() => State = VillagerBoatQuestState.InProgress;
        public void BoatReady() => State = VillagerBoatQuestState.BoatReady;
        public void Complete() => State = VillagerBoatQuestState.Completed;
    }
}
