using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Obstacle
{
    public class RotatorObstacle : MonoBehaviour
    {
        [SerializeField]
        private Transform rotatorBase;
        [SerializeField]
        private float rotatingSpeed;
        
        private Vector3 rotateVector;

        public float RotatingSpeed => rotatingSpeed;
        private void Start()
        {
            rotateVector = new Vector3(0, rotatingSpeed, 0);
        }
        private void Update()
        {
            rotatorBase.Rotate(rotateVector * Time.deltaTime);
        }
    }
}