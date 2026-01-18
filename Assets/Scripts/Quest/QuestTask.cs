namespace BTF.Quest
{
    public sealed class QuestTask
    {
        public string Text { get; }
        public bool Completed { get; private set; }

        public QuestTask(string text)
        {
            Text = text;
        }

        public void Complete() => Completed = true;
    }
}
