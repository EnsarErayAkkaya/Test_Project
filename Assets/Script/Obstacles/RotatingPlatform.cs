using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Obstacle
{
    public class RotatingPlatform : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        private Rigidbody rb;
        private float degree;
        private void FixedUpdate()
        {
            degree += speed * Time.fixedDeltaTime;
            degree %= 360;
            rb.MoveRotation(Quaternion.Euler(0, 0, degree));
        }
    }
}