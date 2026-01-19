using System.Collections.Generic;
using BTF.Quest;

namespace BTF.Dialogue
{
    public sealed class FirstNeighborDialogue : IDialogueResolver
    {
        public IEnumerable<DialogueLine> Resolve(DialogueContext context)
        {
            var quest = context.Quest.CurrentQuest;

            if (quest.Id != QuestId.HelpNeighbor)
            {
                yield return new DialogueLine(
                    "Neighbor",
                    "Thank you again for helping me."
                );
                yield break;
            }

            yield return new DialogueLine(
                "Neighbor",
                "Please… someone took my jewellery box."
            );

            yield return new DialogueLine(
                "Neighbor",
                "Can you talk to the other neighbor for me?"
            );
        }
    }
}
