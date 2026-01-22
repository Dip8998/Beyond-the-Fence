using BTF.Player;
using System.Collections.Generic;

namespace BTF.Dialogue
{
    public sealed class DialogueController
    {
        private readonly DialogueUIView view;
        private readonly PlayerController player;

        private IEnumerator<DialogueLine> enumerator;
        private System.Action onFinish;

        public bool IsPlaying { get; private set; }

        public DialogueController(
            DialogueUIView view,
            PlayerController player)
        {
            this.view = view;
            this.player = player;
        }

        public void StartDialogue(
            IEnumerable<DialogueLine> dialogue,
            System.Action onFinish = null)
        {
            if (IsPlaying)
                return;

            IsPlaying = true;
            this.onFinish = onFinish;

            player.LockMovement(); 

            enumerator = dialogue.GetEnumerator();
            ShowNext();
        }

        private void ShowNext()
        {
            if (!enumerator.MoveNext())
            {
                EndDialogue();
                return;
            }

            var line = enumerator.Current;
            view.Show(line.Speaker, line.Text, ShowNext);
        }

        private void EndDialogue()
        {
            IsPlaying = false;
            view.Hide();

            player.UnlockMovement(); 
            onFinish?.Invoke();
        }
    }
}
