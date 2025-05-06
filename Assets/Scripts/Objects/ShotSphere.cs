using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Objects
{
    public class ShotSphere : MonoBehaviour
    {
        public Transform Transform;

        public float Size;

        public SphereCollider Collider;

        public Rigidbody Rigidbody;

        public UnityEvent IsDestroyed = new();

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
            Rigidbody.useGravity = true;
            Rigidbody.AddForce(force, ForceMode.Impulse);
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

        private void OnTriggerEnter(Collider other)
        {
            ImpactArea();
            IsDestroyed?.Invoke();
            IsDestroyed.RemoveAllListeners();
            Destroy(gameObject);
        }

        private void OnBecameInvisible()
        {
            IsDestroyed?.Invoke();
            IsDestroyed.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}