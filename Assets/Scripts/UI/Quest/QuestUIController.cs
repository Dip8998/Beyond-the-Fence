using BTF.Quest;

namespace BTF.UI.Quest
{
    public sealed class QuestUIController
    {
        public QuestUIController(
            QuestController quest,
            QuestUIView view)
        {
            quest.OnQuestChanged += view.Render;
            view.Render(quest.CurrentQuest);
        }
    }
}
