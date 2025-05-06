using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Main
{
    public class Engine : MonoBehaviour
    {
        public ShootingController ShootingController;

        public InputController InputController;

        void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            GenerateObstacles();
        }

        [Header("Obstacles Generation")]

        [SerializeField] private GameObject _obstaclePrefab;
        [SerializeField] private Transform _generationCenter;

        private const int _obstaclesCount = 70;
        private const float _generationRadius = 10f;

        private void GenerateObstacles()
        {
            for (int i = 0; i < _obstaclesCount; i++)
            {
                Vector2 randomPos = Random.insideUnitCircle * _generationRadius;
                Vector3 spawnPosition = new Vector3(
                    _generationCenter.position.x + randomPos.x,
                    0.5f,
                    _generationCenter.position.z + randomPos.y
                );

                Instantiate(_obstaclePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}