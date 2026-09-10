using UnityEngine;

public class InteractTriggerTest : InteractTrigger
{
    public override void OnInteract()
    {
        print("Player is at position: " + transform.position);
    }
}
