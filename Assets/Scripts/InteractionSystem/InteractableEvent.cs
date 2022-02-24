using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class InteractableEvent : Interactable
{
    public override string GetDescription()
    {
        return "Press <b>[E]</b> to turn interact";
    }

    public override void Interact()
    {
        OnInteract.Invoke();
    }

}
