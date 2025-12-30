using BTF.Camera;
using BTF.Input;
using BTF.Interaction;
using BTF.Player;
using UnityEngine;

namespace BTF.Game
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private InputProvider inputProvider;
        [SerializeField] private CameraFollow2D cameraFollow;
        [SerializeField] private InteractionDetector interactionDetector;

        private PlayerModel playerModel;
        private PlayerController playerController;
        private InputService inputService;
        private InteractionSystem interactionSystem;

        private void Awake()
        {
            Debug.Assert(playerView != null);
            Debug.Assert(inputProvider != null);
            Debug.Assert(cameraFollow != null);

            playerModel = new PlayerModel(100, 5f);
            inputService = new InputService();
            inputProvider.Bind(inputService);

            playerController = new PlayerController(playerModel, inputService);
            playerView.Bind(playerController);
            cameraFollow.SetTarget(playerView.transform);

            interactionSystem = new InteractionSystem();    
            interactionDetector.Bind(interactionSystem);
        }

        private void Update()
        {
            if (inputService.ConsumeInput())
            {
                interactionSystem.TryInteract();
            }
        }
    }
}
