using UnityEngine;

public class InteractObjectTest : InteractObject
{
    private bool shouldRotate;

    public override void OnInteract()
    {
        print("Interacted with " + name);
        hasInteracted = true;
    }

    public override void MarkInteractable(bool isCurrent)
    {
        shouldRotate = isCurrent;
    }

    private void Update()
    {
        if (!hasInteracted)
        {
            if (shouldRotate)
            {
                transform.Rotate(Vector3.up, 45f * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
