using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] protected bool isInteractable = true;
    
    public virtual void Interact()
    {
        if (!isInteractable) return;
        OnInteract();
    }

    protected abstract void OnInteract();
} 