using System.Collections.Generic;
using BTF.FirstNB;
using BTF.Quest;

namespace BTF.Dialogue
{
    public sealed class FirstNeighborDialogue : IDialogueResolver
    {
        private readonly FirstNeighborController firstNeighbor;

        public FirstNeighborDialogue(FirstNeighborController firstNeighbor)
        {
            this.firstNeighbor = firstNeighbor;
        }

        public IEnumerable<DialogueLine> Resolve(DialogueContext context)
        {
            if (firstNeighbor.GetState() == FirstNeighborState.ConflictResolved)
            {
                yield return new DialogueLine(
                    "Neighbor",
                    "She confessed… I never wanted it to end like this."
                );

                yield return new DialogueLine(
                    "Neighbor",
                    "Please, take this key."
                );

                yield return new DialogueLine(
                    "Neighbor",
                    "Unlock the fence and be careful out there."
                );

                yield break;
            }

            var quest = context.Quest.CurrentQuest;

            if (quest.Id == QuestId.HelpNeighbor)
            {
                yield return new DialogueLine(
                    "Neighbor",
                    "My jewellery box is gone."
                );

                yield return new DialogueLine(
                    "Neighbor",
                    "Please… talk to the other neighbor for me."
                );

                yield break;
            }

            yield return new DialogueLine(
                "Neighbor",
                "Thank you… for choosing truth."
            );
        }
    }
}
