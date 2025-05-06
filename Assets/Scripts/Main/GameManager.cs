using Assets.Scripts.Objects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Main
{
    public class GameManager : MonoBehaviour
    {
        public ShootingController ShootingController;

        public PlayerSphere PlayerSphere;

        public Transform EndingPoint;

        public void Activate()
        {
            ShootingController.Activate();
        }

        public void CheckGameEndingCondiition()
        {

            if (IsPlayerLost())
            {
                Debug.Log("Player lost!");
                ShootingController.Deactivate();
                return;
            }

            if (IsPlayerWon())
            {
                PlayerSphere.Push(EndingPoint.position);
                ShootingController.Deactivate();
                Debug.Log("Player won!");
            }
        }

        [SerializeField] private float _criticalSize = 0.3f;

        private bool IsPlayerLost()
        {
            if (PlayerSphere.Size <= _criticalSize)
                return true;

            return false;
        }

        private bool IsPlayerWon()
        {
            var direction = (EndingPoint.position - PlayerSphere.Transform.position).normalized;
            var distance = Vector3.Distance(EndingPoint.position, PlayerSphere.Transform.position); 
            var mask = LayerMask.GetMask("Obstacle");
            var hits = Physics.SphereCastAll(PlayerSphere.Transform.position, PlayerSphere.Size / 2f, direction, distance, mask);
            Debug.Log(hits.Count());

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Obstacle"))
                {
                    Debug.DrawLine(PlayerSphere.Transform.position, hit.point, Color.red, 2f);
                    return false;
                }
            }

            return true;
        }
    }
}