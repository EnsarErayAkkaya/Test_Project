using Project.BaseClasses;
using Project.GameSystem;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Opponent
{
    public class OpponentMovement : BaseCharacterMovement
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float reachDistance;
        private NavMeshPath path;
        private int destinationIndex;
        private int pathIndex;
        private Vector3 lastCalculatedPosition;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        private void Start()
        {
            path = new NavMeshPath();
            agent.updatePosition = false;
            agent.updateRotation = false;
        }

        protected override void Update()
        {
            base.Update();
            if (canMove)
            {
                agent.nextPosition = transform.position; // set agent dest 
                //agent.CalculatePath(agent.destination, path);
                SetDestinationWhenReached();
                if(agent.path.corners.Length >= 2)
                {
                    for (int i = 0; i < agent.path.corners.Length-1; i++)
                    {
                        Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.red);
                    }
                }
            }
        }
        protected override void SetMoveVector()
        {
            Vector3 dir;
            if (agent.path.corners.Length == 0)
            {
                // means there is no path
                // save position
                if (Vector3.Distance(lastCalculatedPosition, transform.position) >= reachDistance)
                    dir = lastCalculatedPosition - transform.position;
                else
                    dir = Vector3.zero;
            }
            else
            {
                int cornerIndex = 0;

                if (agent.path.corners.Length > 1)
                    cornerIndex = 1;
            
                Vector3 firstDest = agent.path.corners[cornerIndex]; // get first point in path of ai
                dir = firstDest - transform.position; // direction to that point

                lastCalculatedPosition = transform.position;
            }
            dir.y = 0;
            moveVector = dir.normalized;
        }
        protected override void OnCharacterFellDown()
        {
            //fill later
        }
        private void SetDestinationWhenReached()
        {
            if(Vector3.Distance(transform.position, agent.destination) < reachDistance)
            {
                SetDestination();
            }
        }
        private void SetDestination()
        {
            //Reached find new path
            if(OpponentAIManager.instance.CanIGetDestination(pathIndex, destinationIndex))
            {
                agent.SetDestination(OpponentAIManager.instance.GetDestination(pathIndex, destinationIndex));
                agent.CalculatePath(agent.destination,path);
                agent.SetPath(path);
                destinationIndex++;
            }
        }
        private void SetPath()
        {
            pathIndex =  OpponentAIManager.instance.SetPathToClosest(transform.position);
        }
        public override void Resume()
        {
            destinationIndex = 0;
            SetPath();
            SetDestination();
            base.Resume();
        }
        public override void SetPoisiton(Vector3 pos)
        {
            agent.Warp(pos);
        }
    }
}