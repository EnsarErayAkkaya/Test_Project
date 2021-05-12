using System.Collections;
using UnityEngine;

namespace Project.Obstacle
{
    public class HalfDonutObstacle : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody stick;
        [SerializeField]
        private float moveInterval;
        [SerializeField]
        private float returnDurtaion;
        [SerializeField]
        private float speed;
        [SerializeField]
        private Transform target;
        [SerializeField]
        private float reachedDistance;

        private WaitForSeconds waitToMove;
        private WaitForSeconds waitToReturn;
        private float deltaTime;

        private void Start()
        {
            waitToMove = new WaitForSeconds(moveInterval);
            waitToReturn = new WaitForSeconds(returnDurtaion);
            StartCoroutine(LoopStick());
        }

        private void OnEnable()
        {
            StartCoroutine(LoopStick());
        }
        IEnumerator LoopStick()
        {
            while(true)
            {
                yield return waitToMove;
                yield return MoveForward();
                yield return waitToReturn;
                yield return ReturnBack();
            }
        }
        private IEnumerator MoveForward()
        {
            deltaTime = 0;
            while (Vector3.Distance(stick.position, target.position) > reachedDistance)
            {
                deltaTime += Time.deltaTime;
                stick.MovePosition(stick.position - stick.transform.right * Time.fixedDeltaTime * speed);
                yield return new WaitForFixedUpdate();
            }
        }
        private IEnumerator ReturnBack()
        {
            deltaTime = 0;
            while (Vector3.Distance(stick.position, transform.position) > reachedDistance)
            {
                deltaTime += Time.deltaTime;
                stick.MovePosition(stick.position + stick.transform.right * Time.fixedDeltaTime * speed);
                yield return null;
            }
        }
    }
}
