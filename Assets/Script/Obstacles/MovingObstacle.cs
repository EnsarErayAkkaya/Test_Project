using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.Obstacle
{
    public class MovingObstacle : MonoBehaviour
    {
        [SerializeField] private Transform firstPoint;
        [SerializeField] private Transform secondPoint;
        [SerializeField] private float speed;
        [SerializeField] private float changeGoalDistance;

        private Vector3 firstPos;
        private Vector3 secondPos;
        private Vector3 goal;
        private void Start()
        {
            firstPos = firstPoint.position;
            secondPos = secondPoint.position;
            goal = secondPos;
        }
        private void Update()
        {
            Move();
        }
        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, goal, Time.deltaTime * speed); // move towards goal

            // change pos when reached
            if (Vector3.Distance(transform.position, firstPos) < changeGoalDistance)
                goal = secondPos;
            else if (Vector3.Distance(transform.position, secondPos) < changeGoalDistance)
                goal = firstPos;
        }
    }
}