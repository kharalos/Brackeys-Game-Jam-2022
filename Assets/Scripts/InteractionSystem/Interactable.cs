using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    // minigame is for custom types, for example connect wires to interact with doors
    public enum InteractionType
    {
        Click,
        Hold
    }

    float holdTime;

    public InteractionType interactionType;

    public UnityEvent OnInteract;

    public Transform CanvasPlacement;

    public abstract string GetDescription();
    public abstract void Interact();

    public void IncreaseHoldTime() => holdTime += Time.deltaTime;
    public void ResetHoldTime() => holdTime = 0f;

    public float GetHoldTime() => holdTime;

    public void OnTriggerEnter(Collider other)
    {
        PlayerInteraction player = other.GetComponent<PlayerInteraction>();
        if (player)
        {
            if (ScareAbility.InteractablesWithinPlayer.Contains(this) == false)
                ScareAbility.InteractablesWithinPlayer.Add(this);

            player.UpdateUI(true, this);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        PlayerInteraction player = other.GetComponent<PlayerInteraction>();
        if (player)
        {
            if (ScareAbility.InteractablesWithinPlayer.Contains(this) == true)
                ScareAbility.InteractablesWithinPlayer.Remove(this);

            player.UpdateUI(false, this);
        }
    }
}