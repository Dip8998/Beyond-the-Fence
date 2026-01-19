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
            // 🔒 Once confessed, NEVER fall back
            if (secondNeighbor.GetState() == SecondNeighborState.Confessed)
            {
                yield return new DialogueLine(
                    "Neighbor",
                    "I’m sorry. I’ve already confessed."
                );
                yield break;
            }

            // Only gate BEFORE confession
            bool asked =
                firstNeighbor.GetState() == FirstNeighborState.AskedForHelp;

            if (!asked)
            {
                yield return new DialogueLine(
                    "Neighbor",
                    "I don’t want to talk."
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
                        "Alright… I took it. I was angry."
                    );
                    break;
            }
        }
    }
}
