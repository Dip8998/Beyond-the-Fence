using System.Collections.Generic;
using BTF.Quest;

namespace BTF.Dialogue
{
    public sealed class OldManDialogue : IDialogueResolver
    {
        public IEnumerable<DialogueLine> Resolve(DialogueContext context)
        {
            var quest = context.Quest.CurrentQuest;

            if (quest.Id != QuestId.PrepareForSurvival)
            {
                yield return new DialogueLine(
                    "Old Man",
                    "You’re still breathing. That’s good."
                );
                yield break;
            }

            yield return new DialogueLine(
                "Old Man",
                "Beyond the fence, the world does not forgive mistakes."
            );

            yield return new DialogueLine(
                "Old Man",
                "Take this blade. It won’t protect you — skill will."
            );

            yield return new DialogueLine(
                "Old Man",
                "Press K to strike. Hesitation will get you killed."
            );
        }
    }
}
