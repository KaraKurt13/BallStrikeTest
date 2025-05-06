using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class Obstacle : MonoBehaviour
    {
        public CapsuleCollider Collider;

        public MeshRenderer Renderer;

        [SerializeField] private Material _disabledObstacleMaterial;

        public void Disable()
        {
            Renderer.material = _disabledObstacleMaterial;
            Collider.enabled = false;
        }
    }
}