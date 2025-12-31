using UnityEngine;

namespace BTF.Enemy
{
	[RequireComponent(typeof(Collider2D))]
	public class EnemyDetection : MonoBehaviour
	{
		private Transform player;
		private EnemyController controller;

        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        public void Bind(Transform player, EnemyController controller)
		{
			this.player = player;
			this.controller = controller;
		}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.transform == player)
			{
				controller?.OnPlayerDetected(player);
			}
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.transform == player)
			{
				controller.OnPlayerLost();
			}
        }
    }
}
