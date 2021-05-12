using Project.BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.Opponent
{
    public class OpponentCollision : BaseCharacterCollision
    {
        [SerializeField] private BaseCharacterMovement movement;

        protected override void OnCharacterCollidedObstacle()
        {
            // restart
            movement.Stop();
        }
        protected override void OnCharacterReachedFinishPlatform()
        {
            Debug.Log("Opponent reached finish");
            movement.Stop();
        }
    }
}