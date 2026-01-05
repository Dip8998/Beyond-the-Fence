using BTF.Inventory;
using BTF.Player;
using System;

namespace BTF.Quest
{
    public class NPCQuestController
    {
        private readonly NPCQuestModel model;
        private readonly InventoryController inventory;

        public event Action OnQuestStarted;
        public event Action OnQuestCompleted;

        public NPCQuestController(NPCQuestModel model, InventoryController inventory)
        {
            this.model = model;
            this.inventory = inventory;
        }

        public void StartQuest()
        {
            if (model.CurrentState != QuestState.NotStarted)
                return;
            model.StartQuest();
            OnQuestStarted?.Invoke();
        }

        public bool TryCompleteQuest()
        {
            if(model.CurrentState != QuestState.InProgress)
                return false;

            if(inventory.GetWoodCount() < model.RequiredWood)
                return false;

            inventory.ConsumeWood(model.RequiredWood);
            model.CompleteQuest();
            OnQuestCompleted?.Invoke();

            return true;
        }

        public bool IsQuestNotStarted()
        {
            return model.CurrentState == QuestState.NotStarted;
        }
    }
}
