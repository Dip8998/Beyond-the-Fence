using UnityEngine;
using UnityEngine.SceneManagement;
using BTF.Player;
using BTF.Game;

namespace BTF.Scenes
{
    public sealed class InteriorSceneService
    {
        private readonly PlayerController player;
        private readonly PlayerView playerView;
        private GameContext context;
        private readonly PlayerCollisionService collisionService = new();

        private Scene? currentScene;
        private Vector3 returnPosition;

        public InteriorSceneService(
            PlayerController player,
            PlayerView playerView,
            GameContext context)
        {
            this.player = player;
            this.playerView = playerView;
            this.context = context;
        }

        public void Enter(string sceneName, Vector3 entrancePosition)
        {
            if (currentScene.HasValue) return;

            returnPosition = entrancePosition;
            player.Lock();
            collisionService.EnterInterior();

            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive)
                .completed += _ => OnSceneLoaded(sceneName);
        }

        private void OnSceneLoaded(string sceneName)
        {
            var scene = SceneManager.GetSceneByName(sceneName);
            currentScene = scene;

            foreach (var root in scene.GetRootGameObjects())
            {
                foreach (var binder in root.GetComponentsInChildren<ISceneBinder>(true))
                {
                    binder.Bind(context);
                }

                if (root.TryGetComponent<IInteriorScene>(out var interior))
                {
                    TeleportPlayer(interior.PlayerSpawn.position);
                }
            }

            player.Unlock();
        }

        public void Exit()
        {
            if (!currentScene.HasValue) return;

            player.Lock();

            SceneManager.UnloadSceneAsync(currentScene.Value)
                .completed += _ =>
                {
                    currentScene = null;
                    TeleportPlayer(returnPosition);
                    player.Unlock();
                };
            collisionService.ExitInterior();
        }

        public void SetContext(GameContext context)
        {
            this.context = context;
        }

        private void TeleportPlayer(Vector3 pos)
        {
            var rb = playerView.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            playerView.transform.position = pos;
        }
    }
}
