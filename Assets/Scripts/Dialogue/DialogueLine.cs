namespace BTF.Dialogue
{
    public sealed class DialogueLine
    {
        public string Speaker { get; }
        public string Text { get; }

        public DialogueLine(string speaker, string text)
        {
            Speaker = speaker;
            Text = text;
        }
    }
}
