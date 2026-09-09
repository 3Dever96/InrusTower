using UnityEngine;

[System.Serializable]
public class PlayerAirState : PlayerState
{
    public override void StartState(PlayerController player)
    {
        
    }

    public override void UpdateState(PlayerController player)
    {
        if (!player.Jump || Physics.CheckSphere(player.transform.position + Vector3.up * 1.6f, player.Controller.radius - 0.01f, LayerMask.GetMask("Solid")))
        {
            player.VerticalSpeed = Mathf.Min(0f, player.VerticalSpeed);
        }

        player.VerticalSpeed += player.gravity * Time.deltaTime;
    }

    public override void ChangeState(PlayerController player)
    {
        if (player.VerticalSpeed <= 0f && Physics.CheckSphere(player.transform.position + Vector3.up * 0.4f, player.Controller.radius - 0.01f, LayerMask.GetMask("Solid")))
        {
            player.SetState(player.GroundState);
        }
    }

    public override void ExitState(PlayerController player)
    {
        
    }
}
