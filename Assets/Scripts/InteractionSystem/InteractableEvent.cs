using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class InteractableEvent : Interactable
{
    public GameObject prefab;
    public override string GetDescription()
    {
        //return "Hold to interact";
        return "";
        //return "Press <b>[E]</b> to turn interact";
    }

    public override void Interact()
    {
        OnInteract.Invoke();
    }
    public void EnableChildrenRB()
    {
        Rigidbody[] rbs = transform.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].isKinematic = false;
        }
    }
}
