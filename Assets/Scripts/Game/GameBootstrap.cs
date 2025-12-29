using UnityEngine;
using BTF.Core.Services;
using BTF.Player;
using BTF.Camera;

namespace Game
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [Header("Scene References")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private CameraFollow2D cameraFollow;

        private InputService inputService;

        private void Awake()
        {
            inputService = new InputService();
            inputService.Enable();

            var playerModel = new PlayerModel();
            var playerController = new PlayerController(playerModel, inputService);

            playerView.Bind(playerController);

            cameraFollow.SetTarget(playerView.transform);
        }

        private void OnDisable()
        {
            inputService.Disable();
        }
    }
}
