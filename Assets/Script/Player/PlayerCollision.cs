using Project.BaseClasses;
using Project.GameSystem;
using Project.Player;
using UnityEngine;

public class PlayerCollision : BaseCharacterCollision
{
    protected override void OnCharacterCollidedObstacle()
    {
        GameManager.instance.OnPlayerFailed();
    }
    protected override void OnCharacterReachedFinishPlatform()
    {
        GameManager.instance.FinishRace();
    }
}
