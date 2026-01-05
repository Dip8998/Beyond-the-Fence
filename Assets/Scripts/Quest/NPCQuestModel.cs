using UnityEngine;

namespace BTF.Quest
{
	public class NPCQuestModel 
	{
		public int RequiredWood { get; set; }
		public QuestState CurrentState { get; set; }
		public bool IsRewardGiven;

		public NPCQuestModel(int requiredWood)
		{
			RequiredWood = requiredWood;
			CurrentState = QuestState.NotStarted;
			IsRewardGiven = false;
		}

		public void StartQuest() => CurrentState = QuestState.InProgress;

		public void CompleteQuest() => CurrentState = QuestState.Completed;
	}
}
