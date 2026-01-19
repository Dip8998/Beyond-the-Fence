using System.Collections.Generic;

namespace BTF.Dialogue
{
    public interface IDialogueResolver
    {
        IEnumerable<DialogueLine> Resolve(DialogueContext context);
    }
}
