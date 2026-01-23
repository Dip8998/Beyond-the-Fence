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
                    "Please… if you find anyone outside the fence… tell them to be careful."
                );
                yield break;
            }

            var task = quest.GetCurrentTask();
            if (task == null)
                yield break;

            switch (task.Text)
            {
                case "Talk to the villager":
                    yield return new DialogueLine(
                        "Villager",
                        "Please… my son was taken across the sea."
                    );
                    yield return new DialogueLine(
                        "Villager",
                        "I need a boat… but I have nothing."
                    );
                    break;

                case "Collect wood":
                    yield return new DialogueLine(
                        "Villager",
                        "Bring me 10 wood. I can start building."
                    );
                    break;

                case "Build the bridge":
                    yield return new DialogueLine(
                        "Villager",
                        "The slime land blocks the way."
                    );
                    yield return new DialogueLine(
                        "Villager",
                        "Cut 10 wooden sticks. We need a bridge."
                    );
                    break;

                case "Find the gear":
                    yield return new DialogueLine(
                        "Villager",
                        "The slime boss carries a gear."
                    );
                    yield return new DialogueLine(
                        "Villager",
                        "Without it, the boat will break apart."
                    );
                    break;

                case "Build the boat":
                    yield return new DialogueLine(
                        "Villager",
                        "That’s everything… I’ll finish the boat now."
                    );
                    break;
            }
        }
    }
}
