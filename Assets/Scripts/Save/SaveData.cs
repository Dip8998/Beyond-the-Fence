using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // ---------- PLAYER ----------
    public Vector3 playerPosition;
    public int playerHP;
    public bool hasWeapon;
    public bool hasFenceKey;

    // ---------- INVENTORY ----------
    public int wood;
    public int berry;
    public int gear;

    // ---------- QUEST ----------
    public int currentQuestId;
    public List<bool> questTaskCompleted;
    public List<bool> questTaskVisible;

    // ---------- NPC ----------
    public int firstNeighborState;
    public int secondNeighborState;

    // ---------- WORLD ----------
    public bool isFenceUnlocked;

    // ---------- TUTORIAL ----------
    public bool foodTutorialShown;

    // ---------- UI ----------
    public bool berryUnlocked;
    public bool inventoryUnlocked;
    public bool questUnlocked;
}