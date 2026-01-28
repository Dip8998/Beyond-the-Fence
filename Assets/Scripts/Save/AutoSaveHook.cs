using BTF.Game;
using BTF.UI;
using UnityEngine;

public sealed class AutoSaveHook : MonoBehaviour
{
    private GameContext context;
    private UIUnlockController uIUnlockController;

    public void Bind(GameContext context, UIUnlockController uIUnlockController)
    {
        this.context = context;
        this.uIUnlockController = uIUnlockController;
    }

    private void OnApplicationQuit()
    {
        if (context != null)
            SaveSystemService.SaveGame(context, uIUnlockController);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && context != null)
            SaveSystemService.SaveGame(context, uIUnlockController);
    }
}