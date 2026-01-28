using BTF.FirstNB;
using BTF.Game;
using BTF.Quest;
using BTF.SeconNB;
using BTF.Tutorial;
using BTF.UI;

public static class SaveSystemService
{
    public static void SaveGame(
        GameContext context,
        UIUnlockController uiUnlock)
    {
        SaveData save = new SaveData();

        // ---------- PLAYER ----------
        save.playerPosition =
            context.Player.View.transform.position;
        save.playerHP = context.Player.GetCurrentHP();
        save.hasWeapon = context.Player.HasWeapon;
        save.hasFenceKey = context.Player.HasFenceKey();

        // ---------- INVENTORY ----------
        save.wood = context.Inventory.GetWoodCount();
        save.berry = context.Inventory.GetBerryCount();
        save.gear = context.Inventory.GetGearCount();

        // ---------- QUEST ----------
        var quest = context.Quest.CurrentQuest;
        save.currentQuestId = (int)quest.Id;

        if (quest.Tasks != null)
        {
            save.questTaskCompleted = new();
            save.questTaskVisible = new();

            foreach (var t in quest.Tasks)
            {
                save.questTaskCompleted.Add(t.Completed);
                save.questTaskVisible.Add(t.Visible);
            }
        }

        // ---------- NPC ----------
        save.firstNeighborState =
            (int)context.FirstNeighbor.GetState();
        save.secondNeighborState =
            (int)context.SecondNeighbor.GetState();

        // ---------- WORLD ----------
        save.isFenceUnlocked =
            GameProgress.IsFenceUnlocked;

        // ---------- TUTORIAL ----------
        save.foodTutorialShown =
            TutorialFlags.FoodTutorialShown;

        // ---------- UI ----------
        save.berryUnlocked = uiUnlock.IsBerryUnlocked;
        save.inventoryUnlocked = uiUnlock.IsInventoryUnlocked;
        save.questUnlocked = uiUnlock.IsQuestUnlocked;

        SaveManager.Save(save);
    }

    public static void LoadGame(
        GameContext context,
        UIUnlockController uiUnlock)
    {
        SaveLoadContext.IsLoading = true;


        SaveData save = SaveManager.Load();
        if (save == null)
        {
            SaveLoadContext.IsLoading = false;
            return;
        }

        // ---------- PLAYER ----------
        context.Player.View.transform.position =
            save.playerPosition;

        context.Player.HealToFull();
        context.Player.TakeDamage(
            context.Player.GetMaxHP() - save.playerHP
        );

        if (save.hasWeapon)
            context.Player.ReceiveWeapon();

        if (save.hasFenceKey)
            context.Player.ReceiveFenceKey();

        // ---------- INVENTORY (SILENT) ----------
        context.Inventory.IsLoading = true;

        context.Inventory.AddWood(save.wood);
        context.Inventory.AddBerry(save.berry);
        context.Inventory.AddGear(save.gear);

        context.Inventory.IsLoading = false;

        // ---------- QUEST ----------
        context.Quest.SetQuest(
            (QuestId)save.currentQuestId
        );

        var quest = context.Quest.CurrentQuest;
        if (quest.Tasks != null)
        {
            for (int i = 0; i < quest.Tasks.Count; i++)
            {
                if (save.questTaskVisible[i])
                    quest.Tasks[i].Reveal();

                if (save.questTaskCompleted[i])
                    quest.Tasks[i].Complete();
            }
        }

        context.Quest.NotifyQuestChanged();

        // ---------- NPC ----------
        context.FirstNeighbor.SetState(
            (FirstNeighborState)save.firstNeighborState
        );

        context.SecondNeighbor.SetState(
            (SecondNeighborState)save.secondNeighborState
        );

        // ---------- WORLD ----------
        GameProgress.IsFenceUnlocked =
            save.isFenceUnlocked;

        // ---------- TUTORIAL ----------
        TutorialFlags.FoodTutorialShown =
            save.foodTutorialShown;

        // ---------- UI ----------
        uiUnlock.SetUnlockedState(
            save.berryUnlocked,
            save.inventoryUnlocked,
            save.questUnlocked
        );


        SaveLoadContext.IsLoading = false;
        UnityEngine.Debug.Log("[SAVE] World restored");
    }
}