using BTF.FirstNB;
using BTF.Game;
using BTF.Interfaces;
using UnityEngine;

namespace BTF.SeconNB
{
    public class SecondNeighborView : MonoBehaviour, IInteractable
    {
        private SecondNeighborController controller;
        private FirstNeighborController firstNeighbor;
        private GameContext gameContext;

        public void Bind(SecondNeighborController controller, FirstNeighborController firstNeighborController, GameContext gameContext)
        {
            this.controller = controller;
            firstNeighbor = firstNeighborController;
            this.gameContext = gameContext;
        }

        public void Interact()
        {
            bool asked = firstNeighbor.GetState() == FirstNeighborState.AskedForHelp;

            controller.OnPlayerInteract(asked);

            switch (controller.GetState())
            {
                case SecondNeighborState.Idle:
                    Debug.Log("I don’t know anything. Go away.");
                    break;

                case SecondNeighborState.Lying:
                    Debug.Log("Okay… I took the jewelry box.");
                    break;

                case SecondNeighborState.Confessed:
                    Debug.Log("I’m sorry. I only wanted her to realize her mistake.");
                    firstNeighbor.ResolveConflict();
                    gameContext.Quest.Advance();
                    break;
            }
        }
    }

}