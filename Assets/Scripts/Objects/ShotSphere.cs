using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ShotSphere : MonoBehaviour
    {
        public Transform Transform;

        public float Radius;

        public SphereCollider Collider;

        [Header("Impact Area")]
        [SerializeField] private Transform _impactAreaTransform;
        [SerializeField] private MeshRenderer _impactAreaRenderer;

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}