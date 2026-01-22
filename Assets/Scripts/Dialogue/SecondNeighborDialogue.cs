using System.Collections.Generic;
using BTF.FirstNB;
using BTF.SeconNB;

namespace BTF.Dialogue
{
    public sealed class SecondNeighborDialogue : IDialogueResolver
    {
        private readonly FirstNeighborController firstNeighbor;
        private readonly SecondNeighborController secondNeighbor;

        public SecondNeighborDialogue(
            FirstNeighborController first,
            SecondNeighborController second)
        {
            firstNeighbor = first;
            secondNeighbor = second;
        }

        public IEnumerable<DialogueLine> Resolve(DialogueContext context)
        {
            if (secondNeighbor.GetState() == SecondNeighborState.Confessed)
            {
                yield return new DialogueLine(
                    "Neighbor",
                    "I’ve already confessed. I can’t undo it."
                );
                yield break;
            }

            bool asked =
                firstNeighbor.GetState() == FirstNeighborState.AskedForHelp;

            if (!asked)
            {
                yield return new DialogueLine(
                    "Neighbor",
                    "I don’t want to talk. Leave me alone."
                );
                yield break;
            }

            switch (secondNeighbor.GetState())
            {
                case SecondNeighborState.Idle:
                    yield return new DialogueLine(
                        "Neighbor",
                        "I… I don’t know anything."
                    );
                    break;

                case SecondNeighborState.Lying:
                    yield return new DialogueLine(
                        "Neighbor",
                        "Alright… I took it."
                    );
                    yield return new DialogueLine(
                        "Neighbor",
                        "I was angry. I wanted her to feel ignored too."
                    );
                    break;
            }
        }
    }
}
