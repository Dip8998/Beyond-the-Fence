using BTF.Camera;
using BTF.Input;
using BTF.Player;
using UnityEngine;

namespace BTF.Game
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private InputProvider inputProvider;
        [SerializeField] private CameraFollow2D cameraFollow;

        private void Awake()
        {
            Debug.Assert(playerView != null);
            Debug.Assert(inputProvider != null);
            Debug.Assert(cameraFollow != null);

            var playerModel = new PlayerModel(100, 5f);
            var inputService = new InputService();
            inputProvider.Bind(inputService);

            var playerController = new PlayerController(playerModel, inputService);
            playerView.Bind(playerController);
            cameraFollow.SetTarget(playerView.transform);
        }
    }
}
