using System.Collections.Generic;
using BTF.Dialogue;

namespace BTF.Tutorial
{
    public sealed class FoodTutorialDialogue : IDialogueResolver
    {
        public IEnumerable<DialogueLine> Resolve(DialogueContext context)
        {
            yield return new DialogueLine(
                "Tip",
                "Food fills up your life."
            );

            yield return new DialogueLine(
                "Tip",
                "Don't forget to use it in combat."
            );
        }
    }
}