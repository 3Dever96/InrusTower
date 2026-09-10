using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public abstract class Interaction : MonoBehaviour
{
    public UnityEvent InteractEvent;

    public virtual void OnInteract()
    {
        InteractEvent?.Invoke();
    }
}
