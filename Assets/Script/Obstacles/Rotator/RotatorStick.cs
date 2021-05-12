using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Obstacle
{
    public class RotatorStick : MonoBehaviour
    {
        [SerializeField]
        private RotatorObstacle rotator;
        [SerializeField]
        private Rigidbody rb;

        private float rotatingSpeed;
        private float degree;
        private float baseRotation;
        
        private void Start()
        {
            baseRotation = transform.rotation.eulerAngles.y;
            rotatingSpeed = rotator.RotatingSpeed;
        }
        private void FixedUpdate()
        {
            degree = (degree + rotatingSpeed * Time.fixedDeltaTime) % 360;

            rb.MoveRotation(Quaternion.Euler(0, baseRotation + degree, 0));    
        }
    }
}