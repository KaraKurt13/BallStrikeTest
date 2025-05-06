using Assets.Scripts.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Main
{
    public class ShootingController : MonoBehaviour
    {
        public InputController InputController;

        public GameManager GameManager;

        public PlayerSphere PlayerSphere;

        private bool _isCharging = false, _isActive = false;

        private const float _sizeStep = 0.01f;

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }

        private void Update()
        {
            if (!_isActive) return;

            if (InputController.IsReleased())
            {
                OnRelease();
                return;
            }
        }

        private void FixedUpdate()
        {
            if (!_isActive) return;

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
            _shotSphere.IsDestroyed.AddListener(OnSphereDestruction);
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

        private void OnSphereDestruction()
        {
            GameManager.CheckGameEndingCondiition();
        }
    }
}