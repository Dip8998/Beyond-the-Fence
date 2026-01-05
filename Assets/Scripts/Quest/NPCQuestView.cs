using BTF.Interfaces;
using UnityEngine;

namespace BTF.Quest
{
    public class NPCQuestView : MonoBehaviour, IInteractable
    {
        [SerializeField] private string npcName = "Villager";

        private NPCQuestController controller;

        public void Bind(NPCQuestController controller)
        {
            this.controller = controller;
            controller.OnQuestStarted += OnQuestStarted;
            controller.OnQuestCompleted += OnQuestCompleted;
        }

        public void Interact()
        {
            if (controller.IsQuestNotStarted())
            {
                controller.StartQuest();
            }
            else
            {
               bool completed = controller.TryCompleteQuest();

                if (!completed)
                {
                    Debug.Log($"{npcName}: You still need more wood.");
                }
            }
        }

        private void OnQuestStarted()
        {
            Debug.Log($"{npcName}: Please collect 10 wood for me.");
        }

        private void OnQuestCompleted()
        {
            Debug.Log($"{npcName}: Thank you! The fence is now unlocked.");
        }
    }
}
