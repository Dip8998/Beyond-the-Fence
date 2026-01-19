using System.Collections.Generic;

namespace BTF.Dialogue
{
    public sealed class DialogueController
    {
        private readonly DialogueUIView view;
        private IEnumerator<DialogueLine> enumerator;
        private System.Action onFinished;

        public bool IsPlaying { get; private set; }

        public DialogueController(DialogueUIView view)
        {
            this.view = view;
        }

        public void StartDialogue(
            IEnumerable<DialogueLine> dialogue,
            System.Action onFinished = null)
        {
            if (IsPlaying) return;

            this.onFinished = onFinished;
            enumerator = dialogue.GetEnumerator();
            IsPlaying = true;
            ShowNext();
        }

        private void ShowNext()
        {
            if (!enumerator.MoveNext())
            {
                IsPlaying = false;
                view.Hide();
                onFinished?.Invoke();
                return;
            }

            var line = enumerator.Current;
            view.Show(line.Speaker, line.Text, ShowNext);
        }
    }

}
