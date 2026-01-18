using System.Collections.Generic;

namespace BTF.Quest
{
    public sealed class QuestModel
    {
        public QuestId Id { get; }
        public string Title { get; }
        public string Description { get; }

        public readonly List<QuestTask> Tasks;

        public QuestModel(
            QuestId id,
            string title,
            string description,
            List<QuestTask> tasks = null)
        {
            Id = id;
            Title = title;
            Description = description;
            Tasks = tasks;
        }

        public bool IsCompleted =>
            Tasks == null || Tasks.TrueForAll(t => t.Completed);

        public void RevealTask(int index)
        {
            if (Tasks == null || index < 0 || index >= Tasks.Count)
                return;

            Tasks[index].Reveal();
        }

        public void CompleteTask(int index)
        {
            if (Tasks == null || index < 0 || index >= Tasks.Count)
                return;

            Tasks[index].Complete();

            if (index + 1 < Tasks.Count)
                Tasks[index + 1].Reveal();
        }

    }
}
