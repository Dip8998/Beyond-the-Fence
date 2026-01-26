using BTF.Game;
using BTF.Quest;
using BTF.Villager;
using System.Collections.Generic;

namespace BTF.Dialogue
{
    public sealed class VillagerDialogue : IDialogueResolver
    {
        private readonly VillagerBoatQuestController villagerQuest;

        public VillagerDialogue(VillagerBoatQuestController villagerQuest)
        {
            this.villagerQuest = villagerQuest;
        }

        public IEnumerable<DialogueLine> Resolve(DialogueContext context)
        {
            var quest = context.Quest.CurrentQuest;

            if (GameProgress.IsFenceUnlocked && quest == null)
            {
                yield return new DialogueLine(
                    "Villager",
                    "Hey there. Everything okay?"
                );
                yield break;
            }

            if (!GameProgress.IsFenceUnlocked && quest == null)
            {
                yield return new DialogueLine(
                    "Villager",
                    "Please… stay inside the fence. It’s not safe out there."
                );
                yield break;
            }

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
                    if (villagerQuest.GetModel().IsBridgeBuilt)
                    {
                        yield return new DialogueLine(
                            "Villager",
                            "The bridge is ready."
                        );
                        yield return new DialogueLine(
                            "Villager",
                            "Go to the slime area and bring me the gear."
                        );
                    }
                    else
                    {
                        yield return new DialogueLine(
                            "Villager",
                            "The slime land blocks the way."
                        );
                        yield return new DialogueLine(
                            "Villager",
                            "Cut 10 wooden sticks. We need a bridge."
                        );
                    }
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