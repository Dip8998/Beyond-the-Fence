using UnityEngine;
using BTF.Scenes;
using BTF.Game;
using BTF.Enemy;

namespace BTF.Slime
{
    public sealed class SlimeAreaBinder : MonoBehaviour, ISceneBinder
    {
        [Header("Slime Mobs")]
        [SerializeField] private EnemyView[] slimeViews;

        [Header("Big Slime Boss")]
        [SerializeField] private EnemyView bossView;

        public void Bind(GameContext context)
        {
            foreach (var slimeView in slimeViews)
            {
                BindEnemy(
                    slimeView,
                    context,
                    moveSpeed: 1.2f,
                    chaseSpeed: 2.2f,
                    hp: 3
                );
            }

            if (bossView != null)
            {
                BindEnemy(
                    bossView,
                    context,
                    moveSpeed: 0f,   
                    chaseSpeed: 0f, 
                    hp: 25          
                );
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
