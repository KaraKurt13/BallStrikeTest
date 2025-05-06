using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ShotSphere : MonoBehaviour
    {
        public Transform Transform;

        public float Size, ImpactAreaSize;

        public SphereCollider Collider;

        public Rigidbody RigidBody;

        private const float _impactAreaModifier = 1.3f;

        [Header("Impact Area")]
        [SerializeField] private Transform _impactAreaTransform;
        [SerializeField] private MeshRenderer _impactAreaRenderer;

        public void IncreaseSize(float step)
        {
            Size += step;
            ImpactAreaSize = Size * _impactAreaModifier;

            Transform.localScale = new Vector3(Size, Size, Size);
            _impactAreaTransform.localScale = new Vector3(ImpactAreaSize, ImpactAreaSize, ImpactAreaSize);
        }

        public void Shoot()
        {
            Collider.enabled = true;
            _impactAreaRenderer.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
    }
}