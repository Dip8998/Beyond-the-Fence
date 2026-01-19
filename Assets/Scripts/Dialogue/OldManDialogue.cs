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
                    "Stay sharp out there."
                );
                yield break;
            }

            yield return new DialogueLine(
                "Old Man",
                "Beyond the fence, nothing is forgiving."
            );

            yield return new DialogueLine(
                "Old Man",
                "Take this blade. You’ll need it."
            );
        }
    }
}
