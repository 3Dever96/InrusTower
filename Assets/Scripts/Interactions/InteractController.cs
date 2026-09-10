using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractController : MonoBehaviour
{
    private PlayerInput input;

    public InteractObject currentObject;
    public InteractObject lastObject;
    public List<InteractObject> objects = new List<InteractObject>();

    private void Start()
    {
        input = GetComponentInParent<PlayerInput>();

        input.onActionTriggered += OnAction;
    }

    private void OnEnable()
    {
        if (input != null)
        {
            input.onActionTriggered += OnAction;
        }
    }

    private void OnDisable()
    {
        input.onActionTriggered -= OnAction;
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.action.name == "Interact")
        {
            if (context.performed)
            {
                if (currentObject != null)
                {
                    Vector2 myPosition = new Vector2(transform.position.x, transform.position.z);
                    Vector2 objectPosition = new Vector2(currentObject.interactPoint.position.x, currentObject.interactPoint.position.z);

                    if (Vector2.Distance(myPosition, objectPosition) < currentObject.interactDistance)
                    {
                        currentObject.OnInteract();
                        objects.Remove(currentObject);
                        currentObject = null;
                    }
                }
            }
        }
    }

    private void Update()
    {
        objects.Sort(SortObjects);

        currentObject = objects.Count > 0 ? objects[0] : null;

        if (currentObject != lastObject)
        {
            if (currentObject != null)
            {
                currentObject.MarkInteractable(true);
            }

            if (lastObject != null)
            {
                lastObject.MarkInteractable(false);
            }

            lastObject = currentObject;
        }
    }

    public void AddObject(InteractObject newObject)
    {
        if (!objects.Contains(newObject))
        {
            objects.Add(newObject);
            objects.Sort(SortObjects);
        }
    }

    public void RemoveObject(InteractObject lostObject)
    {
        if (objects.Contains(lostObject))
        {
            objects.Remove(lostObject);
        }
    }

    private int SortObjects(InteractObject a, InteractObject b)
    {
        Vector2 myPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 aPosition = new Vector2(a.interactPoint.position.x, a.interactPoint.position.z);
        Vector2 bPosition = new Vector2(b.interactPoint.position.x, b.interactPoint.position.z);

        float distanceA = Vector2.Distance(myPosition, aPosition);
        float distanceB = Vector2.Distance(myPosition, bPosition);

        if (distanceA > distanceB)
        {
            return 1;
        }
        else if (distanceA < distanceB)
        {
            return -1;
        }

        return 0;
    }
}
