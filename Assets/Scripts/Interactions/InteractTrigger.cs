using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class InteractTrigger : Interaction
{
    public enum TriggerType
    {
        Enter,
        Constant,
        Exit
    }

    public TriggerType type;

    private void OnTriggerEnter(Collider other)
    {
        if (type == TriggerType.Enter)
        {
            OnInteract();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (type == TriggerType.Constant)
        {
            OnInteract();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (type == TriggerType.Exit)
        {
            OnInteract();
        }
    }
}
