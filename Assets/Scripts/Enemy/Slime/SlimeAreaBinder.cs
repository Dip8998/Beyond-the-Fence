using BTF.Enemy;
using BTF.Game;
using BTF.Resource;
using BTF.Scenes;
using UnityEngine;

namespace BTF.Slime
{
    public sealed class SlimeAreaBinder : MonoBehaviour, ISceneBinder
    {
        [Header("Slime Mobs")]
        [SerializeField] private EnemyView[] slimeViews;

        [Header("Big Slime Boss")]
        [SerializeField] private EnemyView bossView;
        [SerializeField] private GearView gearView;

        public void Bind(GameContext context)
        {
            foreach (var slimeView in slimeViews)
            {
                BindEnemy(slimeView, context, 1.2f, 2.2f, 3);
            }

            if (bossView != null)
            {
                BindEnemy(bossView, context, 0f, 0f, 100);
            }

            if (gearView != null)
            {
                gearView.Bind(context.Inventory);
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
