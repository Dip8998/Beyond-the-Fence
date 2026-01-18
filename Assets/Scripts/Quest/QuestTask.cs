namespace BTF.Quest
{
    public sealed class QuestTask
    {
        public string Text { get; }
        public bool Completed { get; private set; }
        public bool Visible { get; private set; }

        public QuestTask(string text, bool visible = false)
        {
            Text = text;
            Visible = visible;
        }

        public void Complete() => Completed = true;

        public void Reveal() => Visible = true;
    }
}
