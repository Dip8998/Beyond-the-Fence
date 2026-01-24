using BTF.FirstNB;
using BTF.Game;
using BTF.Quest;
using BTF.Scenes;
using UnityEngine;

namespace BTF.NPC
{
    public sealed class FirstNeighborInteriorBinder : MonoBehaviour, ISceneBinder
    {
        [SerializeField] private FirstNeighborView view;
        [SerializeField] private NPCQuestIcon questIcon;

        public void Bind(GameContext context)
        {
            view.Bind(context.FirstNeighbor, context.Player, context);

            if (context.Quest.CurrentQuest.Id == QuestId.HelpNeighbor)
                questIcon.Show();
            else
                questIcon.Hide();
        }
    }
}
