using System.Collections.Generic;

namespace BTF.Dialogue
{
    public sealed class DialogueRunner
    {
        private readonly DialogueContext context;

        public DialogueRunner(DialogueContext context)
        {
            this.context = context;
        }

        public IEnumerable<DialogueLine> Run(IDialogueResolver resolver)
        {
            return resolver.Resolve(context);
        }
    }
}
