using UnityEngine;

[System.Serializable]
public class PlayerGroundState : PlayerState
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accel;
    [SerializeField] private float decel;
    [SerializeField] private float fric;
    [SerializeField] private float turnAngle;

    private float moveSpeed;

    public override void StartState(PlayerController player)
    {
        player.VerticalSpeed = player.stickForce;
    }

    public override void UpdateState(PlayerController player)
    {
        // Get Input Direction
        Vector3 direction = Camera.main.transform.right * player.Move.x + Camera.main.transform.forward * player.Move.y;
        direction.y = 0f;
        direction = direction.normalized;

        // Accelleration based movement
        if (player.Move != Vector2.zero)
        {
            if (Vector3.Angle(direction, player.Direction) > turnAngle)
            {
                player.CurrentSpeed -= decel * Time.deltaTime;

                if (player.CurrentSpeed <= 0f)
                {
                    player.CurrentSpeed = 0f;
                    player.Direction = direction;
                }
            }
            else
            {
                if (player.CurrentSpeed < moveSpeed)
                {
                    player.CurrentSpeed += accel * Time.deltaTime;
                }
                else if (player.CurrentSpeed > moveSpeed + 0.2f)
                {
                    player.CurrentSpeed -= accel * Time.deltaTime;
                }
                else
                {
                    player.CurrentSpeed = moveSpeed;
                }

                player.Direction = direction;
            }
        }
        else
        {
            player.CurrentSpeed -= Mathf.Min(player.CurrentSpeed, fric * Time.deltaTime);
        }

        moveSpeed = player.Move.magnitude * maxSpeed;

        player.FaceDirection(player.Direction, player.turnSpeed);
    }

    public override void ChangeState(PlayerController player)
    {
        
    }

    public override void ExitState(PlayerController player)
    {
        
    }
}
