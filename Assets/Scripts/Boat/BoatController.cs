using BTF.Player;
using UnityEngine;

namespace BTF.Boat
{
    public class BoatController
    {
        private readonly BoatModel model;
        private readonly Transform boatTransform;
        private readonly Transform destination;
        private readonly float speed;

        private PlayerController player;

        public BoatController(
            BoatModel model,
            Transform boatTransform,
            Transform destination,
            float speed)
        {
            this.model = model;
            this.boatTransform = boatTransform;
            this.destination = destination;
            this.speed = speed;
        }

        public void Board(PlayerController player, Transform seatPoint)
        {
            if (model.State != BoatState.Idle) return;

            this.player = player;
            model.StartTravel();

            player.Lock();

            var rb = player.View.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;

            player.View.transform.SetParent(seatPoint);
            player.View.transform.localPosition = Vector3.zero;

            Debug.Log("Player boarded boat. Travelling...");
        }

        public void Tick()
        {
            if (model.State != BoatState.Travelling) return;

            boatTransform.position = Vector3.MoveTowards(
                boatTransform.position,
                destination.position,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(boatTransform.position, destination.position) < 0.05f)
            {
                Arrive();
            }
        }

        private void Arrive()
        {
            model.Arrive();

            player.View.transform.SetParent(null);

            var rb = player.View.GetComponent<Rigidbody2D>();
            rb.simulated = true;

            player.Unlock();

            Debug.Log("Boat arrived at Boss Island!");
        }
    }
}
