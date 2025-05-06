using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class PlayerSphere : MonoBehaviour
    {
        public Transform Transform;

        public LineRenderer Line;

        public float Size = 3;

        public void DecreaseSize(float step)
        {
            Size -= step;
            Transform.localScale = new Vector3(Size, Size, Size);
            Line.startWidth = Size;
            Line.endWidth = Size;
        }
    }
}