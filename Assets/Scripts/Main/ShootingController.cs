using Assets.Scripts.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Main
{
    public class ShootingController : MonoBehaviour
    {
        public InputController InputController;

        public PlayerSphere PlayerSphere;

        private bool _isCharging = false;

        private const float _sizeStep = 0.01f;

        private void Update()
        {
            if (InputController.IsReleased())
            {
                OnRelease();
                return;
            }
        }

        private void FixedUpdate()
        {
            if (InputController.IsHolding())
            {
                OnHold();
            }
        }

        private void OnHold()
        {
            if (!_isCharging)
            {
                StartCharging();
            }
            else
            {
                IncreaseShootSize();
            }
        }

        private void OnRelease()
        {
            if (_isCharging)
            {
                EndCharging();
                InvokeShoot();
            }
        }

        [SerializeField] private GameObject _shotSpherePrefab;
        [SerializeField] private Transform _shotSphereSpawnPoint;
        [SerializeField] private Transform _targetPoint;

        private ShotSphere _shotSphere;

        private void StartCharging()
        {
            _isCharging = true;
            _shotSphere = Instantiate(_shotSpherePrefab, _shotSphereSpawnPoint.position, Quaternion.identity).GetComponent<ShotSphere>();
        }

        private void EndCharging()
        {
            _isCharging = false;
        }

        private void IncreaseShootSize()
        {
            PlayerSphere.DecreaseSize(_sizeStep);
            _shotSphere.IncreaseSize(_sizeStep);
        }

        private void InvokeShoot()
        {
            _shotSphere.Shoot(_targetPoint.position);
            _shotSphere = null;
        }
    }
}