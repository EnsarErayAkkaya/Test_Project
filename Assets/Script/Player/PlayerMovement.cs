using Project.BaseClasses;
using Project.GameSystem;
using UnityEngine;

namespace Project.Player
{
    public class PlayerMovement : BaseCharacterMovement
    {
        [SerializeField] private SwerveInput swerveInput;

        protected override void SetMoveVector()
        {
            moveVector = new Vector3(swerveInput.swerveVector.x, 0, swerveInput.swerveVector.y);
        }
        protected override void OnCharacterFellDown()
        {
            GameManager.instance.Restart();
        }
    }
}