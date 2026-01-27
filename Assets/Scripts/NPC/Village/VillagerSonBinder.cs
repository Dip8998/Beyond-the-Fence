using BTF.Game;
using BTF.Scenes;
using BTF.Villager;
using UnityEngine;

public sealed class VillagerSonBinder : MonoBehaviour, ISceneBinder
{
    [SerializeField] private VillagerSonView sonView;

    public void Bind(GameContext context)
    {
        sonView.Bind(context);
    }
}