using BTF.Quest;

namespace BTF.UI.Quest
{
    public sealed class QuestUIController
    {
        private readonly QuestController questController;
        private readonly QuestUIView view;

        public QuestUIController(
            QuestController questController,
            QuestUIView view)
        {
            this.questController = questController;
            this.view = view;

            questController.OnQuestChanged += OnQuestChanged;

            view.Render(questController.CurrentQuest);
        }

        private void OnQuestChanged(QuestModel quest)
        {
            view.Render(quest);
        }
    }
}
