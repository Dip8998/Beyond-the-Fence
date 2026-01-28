using BTF.Game;
using BTF.Guidance;
using BTF.Input;
using BTF.Tutorial;
using BTF.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestResetGame : MonoBehaviour
{
    private InputService inputService;
    private GameContext gameContext;

    public void Bind(InputService inputService, GameContext context)
    {
        this.inputService = inputService;
        gameContext = context;
    }

    void Update()
    {
        if (inputService == null)
            return;

        if (inputService.ConsumeReset())
        {
            SaveManager.DeleteSave();

            GameProgress.IsFenceUnlocked = false;
            TutorialFlags.FoodTutorialShown = false;

            gameContext.Inventory.ResetInventory();

            SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
            );
        }
    }
}