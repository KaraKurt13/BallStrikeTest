using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ShotSphere : MonoBehaviour
    {
        public Transform Transform;

        public float Size;

        public SphereCollider Collider;

        public Rigidbody RigidBody;

        private const float _impactAreaModifier = 2f;

        [Header("Impact Area")]
        [SerializeField] private Transform _impactAreaTransform;
        [SerializeField] private MeshRenderer _impactAreaRenderer;

        public void IncreaseSize(float step)
        {
            Size += step;
            Transform.localScale = new Vector3(Size, Size, Size);
        }

        public void Shoot(Vector3 target)
        {
            var direction = (target - transform.position).normalized;
            var force = direction * 50f;
            Collider.radius = Size;
            Collider.enabled = true;
            _impactAreaRenderer.enabled = false;
            RigidBody.useGravity = true;
            RigidBody.AddForce(force, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            ImpactArea();
            Destroy(gameObject);
        }

        private void ImpactArea()
        {
            var impactRadius = Size * _impactAreaModifier;
            var center = transform.position;
            var hits = Physics.OverlapSphere(center, impactRadius);

            foreach (var collider in hits)
            {
                if (collider.TryGetComponent<Obstacle>(out var obstacle))
                {
                    obstacle.Disable();
                }
            }

        }
    }
}