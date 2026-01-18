using System;
using System.Collections.Generic;

namespace BTF.Quest
{
    public sealed class QuestController
    {
        public QuestModel CurrentQuest { get; private set; }

        public event Action<QuestModel> OnQuestChanged;

        private readonly Dictionary<QuestId, QuestModel> quests;

        public QuestController()
        {
            quests = BuildQuests();
            SetQuest(QuestId.HelpNeighbor);
        }

        private Dictionary<QuestId, QuestModel> BuildQuests()
        {
            return new()
            {
                {
                    QuestId.HelpNeighbor,
                    new QuestModel(
                        QuestId.HelpNeighbor,
                        "Help the Neighbor",
                        "Find out what happened to the missing jewellery."
                    )
                },
                {
                    QuestId.UnlockFence,
                    new QuestModel(
                        QuestId.UnlockFence,
                        "Unlock the Fence",
                        "Use the key to unlock the village fence."
                    )
                },
                {
                    QuestId.PrepareForSurvival,
                    new QuestModel(
                        QuestId.PrepareForSurvival,
                        "Prepare for Survival",
                        "Meet the old man and get a weapon."
                    )
                },
                {
                    QuestId.HelpVillager,
                    new QuestModel(
                        QuestId.HelpVillager,
                        "Help the Villager",
                        "Gather resources to help the villager.",
                        new List<QuestTask>
                        {
                            new("Collect wood"),
                            new("Find the gear"),
                            new("Build the bridge"),
                            new("Build the boat")
                        }
                    )
                },
                {
                    QuestId.SaveVillagersChild,
                    new QuestModel(
                        QuestId.SaveVillagersChild,
                        "Save the Villager’s Child",
                        "Defeat the boss and rescue the child."
                    )
                }
            };
        }

        public void Advance()
        {
            if (CurrentQuest.Id == QuestId.Completed)
                return;

            QuestId next = CurrentQuest.Id + 1;
            SetQuest(next);
        }

        public void CompleteTask(int index)
        {
            if (CurrentQuest.Tasks == null)
                return;

            var task = CurrentQuest.Tasks[index];
            task.Complete();

            if (CurrentQuest.IsCompleted)
                Advance();

            OnQuestChanged?.Invoke(CurrentQuest);
        }

        private void SetQuest(QuestId id)
        {
            if (!quests.TryGetValue(id, out var quest))
            {
                CurrentQuest = null;
                return;
            }

            CurrentQuest = quest;
            OnQuestChanged?.Invoke(CurrentQuest);
        }
    }
}
