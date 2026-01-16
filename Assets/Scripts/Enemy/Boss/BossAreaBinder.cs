using BTF.Enemy;
using BTF.Game;
using BTF.Resource;
using BTF.Scenes;
using UnityEngine;

namespace BTF.Boss
{
    public sealed class BossAreaBinder : MonoBehaviour, ISceneBinder
    {
        [Header("Slime Mobs")]
        [SerializeField] private EnemyView[] bossEnemies;

        [Header("Big Slime Boss")]
        [SerializeField] private EnemyView bossView;

        public void Bind(GameContext context)
        {
            foreach (var slimeView in bossEnemies)
            {
                BindEnemy(slimeView, context, 1.2f, 2.2f, 3);
            }

            if (bossView != null)
            {
                BindEnemy(bossView, context, 0f, 0f, 100);
            }
        }

        private void BindEnemy(
            EnemyView view,
            GameContext context,
            float moveSpeed,
            float chaseSpeed,
            int hp)
        {
            var model = new EnemyModel(moveSpeed, chaseSpeed, hp);
            var controller = new EnemyController(model);

            view.Bind(controller);

            var detection = view.GetComponentInChildren<EnemyDetection>();
            detection.Bind(context.Player.View.transform, controller);
        }
    }
}
