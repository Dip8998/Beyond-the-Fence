using BTF.Quest;
using BTF.Game;

namespace BTF.Dialogue
{
    public sealed class DialogueContext
    {
        public QuestController Quest { get; }
        public GameContext Game { get; }

        public DialogueContext(
            QuestController quest,
            GameContext game)
        {
            Quest = quest;
            Game = game;
        }
    }
}
