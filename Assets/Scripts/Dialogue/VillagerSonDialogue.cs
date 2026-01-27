using System.Collections.Generic;
using BTF.Dialogue;
using BTF.Quest;

namespace BTF.Villager
{
    public sealed class VillagerSonDialogue : IDialogueResolver
    {
        public IEnumerable<DialogueLine> Resolve(DialogueContext context)
        {
            var quest = context.Quest.CurrentQuest;

            if (quest != null && quest.Id == QuestId.Completed)
            {
                yield return new DialogueLine(
                    "Villager's Son",
                    "You came back for me… I was so scared."
                );

                yield return new DialogueLine(
                    "Villager's Son",
                    "Thank you. I won’t forget this."
                );

                yield break;
            }

            yield return new DialogueLine(
                "Villager's Son",
                "I thought I would never leave this place."
            );

            yield return new DialogueLine(
                "Villager's Son",
                "Please… take me back to my father."
            );
        }
    }
}