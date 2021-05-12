using UnityEngine;

namespace Project.Player
{
    public class SwerveInput : MonoBehaviour
    {
        private Vector2 startingPoint;

        public Vector2 swerveVector;
        private void Update()
        {
            if(Input.GetMouseButton(0))
            {
                swerveVector = -(startingPoint - (Vector2)Input.mousePosition).normalized;
            }
            if (Input.GetMouseButtonDown(0))
            {
                startingPoint = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                swerveVector = Vector2.zero;
            }

        }
    }
}