using UnityEngine;

public class InteractObject : Interaction
{
    public Transform interactPoint;
    public float interactDistance;

    [SerializeField] protected bool hasInteracted;

    protected virtual void Start()
    {
        if (interactPoint == null)
        {
            interactPoint = transform;
        }
    }

    public override void OnInteract()
    {
        
    }

    public virtual void MarkInteractable(bool isCurrent)
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!hasInteracted)
        {
            InteractController controller = other.GetComponent<InteractController>();

            if (controller != null)
            {
                controller.AddObject(this);
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        InteractController controller = other.GetComponent<InteractController>();

        if (controller != null)
        {
            controller.RemoveObject(this);
        }
    }
}
