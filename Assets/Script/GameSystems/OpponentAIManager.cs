using UnityEngine;

namespace Project.GameSystem
{
    public class OpponentAIManager : MonoBehaviour
    {
        public Path[] AIPaths;

        [System.Serializable]
        public struct Path
        {
            public Transform[] destinations;
        }

        public static OpponentAIManager instance;

        private void Awake()
        {
            instance = this;
        }

        public bool CanIGetDestination(int pathIndex, int destIndex)
        {
            return destIndex < AIPaths[pathIndex].destinations.Length;
        }

        public Vector3 GetDestination(int pathIndex, int destIndex) 
        {
            return AIPaths[pathIndex].destinations[destIndex].position; 
        }
        public int SetPathToClosest(Vector3 agentPos)
        {
            int index = 0;
            float smallest = int.MaxValue;
            float dist = 0;
            for (int i = 0; i < AIPaths.Length; i++)
            {
                dist = Vector3.Distance(agentPos, AIPaths[i].destinations[0].position);
                if ( dist < smallest)
                {
                    smallest = dist;
                    index = i;
                }
            }
            return index;
        }
    }
}
