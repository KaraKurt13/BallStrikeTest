using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class EndGate : MonoBehaviour
    {
        public MeshRenderer Renderer;

        [SerializeField] private Material _activatedMaterial;

        public void Activate()
        {
            Renderer.material = _activatedMaterial;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerSphere"))
                Activate();
        }
    }
}