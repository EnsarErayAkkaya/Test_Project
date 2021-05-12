using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.BaseClasses
{
    public class BaseCharacterCollision : MonoBehaviour
    {
        [SerializeField]
        private string obstacleTag;
        [SerializeField]
        private string finishPlatformTag;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag(obstacleTag))
            {
                OnCharacterCollidedObstacle();
            }
            if (collision.transform.CompareTag(finishPlatformTag))
            {
                OnCharacterReachedFinishPlatform();
            }
        }
        protected virtual void OnCharacterCollidedObstacle()
        {
            Debug.LogError("Error: OnCharacterCollidedObstacle not implemented!");
        }
        protected virtual void OnCharacterReachedFinishPlatform()
        {
            Debug.LogError("Error: OnCharacterReachedFinishPlatform not implemented!");
        }
    }
}
