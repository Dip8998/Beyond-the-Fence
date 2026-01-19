using System.Collections.Generic;
using BTF.Quest;

namespace BTF.Dialogue
{
    public sealed class VillagerDialogue : IDialogueResolver
    {
        public IEnumerable<DialogueLine> Resolve(DialogueContext context)
        {
            var quest = context.Quest.CurrentQuest;

            if (quest.Id != QuestId.HelpVillager)
            {
                yield return new DialogueLine(
                    "Villager",
                    "Thank you again… my son is safe now."
                );
                yield break;
            }

            var task = quest.GetCurrentTask();
            if (task == null) yield break;

            switch (task.Text)
            {
                case "Talk to the villager":
                    yield return new DialogueLine(
                        "Villager",
                        "My son was taken by a monster across the sea."
                    );
                    yield return new DialogueLine(
                        "Villager",
                        "I need a boat… but I don’t have the materials."
                    );
                    break;

                case "Collect wood":
                    yield return new DialogueLine(
                        "Villager",
                        "Bring me wood. I’ll begin the work."
                    );
                    break;

                case "Build the bridge":
                    yield return new DialogueLine(
                        "Villager",
                        "The slime land is blocked. We need a bridge."
                    );
                    break;

                case "Find the gear":
                    yield return new DialogueLine(
                        "Villager",
                        "The slime boss carries a vital gear."
                    );
                    break;

                case "Build the boat":
                    yield return new DialogueLine(
                        "Villager",
                        "That’s everything. I’ll finish the boat."
                    );
                    break;
            }
        }
    }
}
