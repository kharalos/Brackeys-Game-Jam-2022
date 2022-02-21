using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInteraction : MonoBehaviour
{

    public float interactionDistance;
    public bool enableRaycast;
    public TextMeshProUGUI interactionText;
    public GameObject interactionClickGO;
    public GameObject interactionHoldGO; // the ui parent to disable when not interacting
    public Image interactionHoldProgress; // the progress bar for hold interaction type

    Camera cam;
    public bool successfulHit = false;
    Interactable i;
    KeyCode key = KeyCode.E;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        /*        if (enableRaycast)
                {
                    Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
                    RaycastHit hit;



                    if (Physics.Raycast(ray, out hit, interactionDistance))
                    {
                        Interactable interactable = hit.collider.GetComponent<Interactable>();
                        successfulHit = UpdateUI(successfulHit, interactable);
                    }
                }*/

        if (successfulHit && Input.GetKey(key))
            HandleInteraction(i);
    }

    public void UpdateUI(bool _successfulHit, Interactable interactable)
    {
        successfulHit = _successfulHit;
        if (_successfulHit)
        {
            i = interactable;
            interactionText.text = interactable.GetDescription();
            //successfulHit = true;
            //Debug.Log("E");
            interactionClickGO.SetActive(interactable.interactionType == Interactable.InteractionType.Click);
            interactionHoldGO.SetActive(interactable.interactionType == Interactable.InteractionType.Hold);
        }
        else if (!_successfulHit)
        {
            ResetInteraction();
        }
    }
    //dasa

    private void ResetInteraction()
    {
        interactionText.text = "";
        interactionHoldGO.SetActive(false);
        interactionClickGO.SetActive(false);
    }

    void HandleInteraction(Interactable interactable)
    {

        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.Click:
                // interaction type is click and we clicked the button -> interact
                if (Input.GetKeyDown(key))
                {
                    interactable.Interact();

                }
                break;
            case Interactable.InteractionType.Hold:
                if (Input.GetKey(key))
                {
                    // we are holding the key, increase the timer until we reach 1f
                    interactable.IncreaseHoldTime();
                    if (interactable.GetHoldTime() > 1f)
                    {
                        successfulHit = false;
                        interactable.Interact();
                        interactable.ResetHoldTime();
                        ResetInteraction();
                    }

                }
                else
                {
                    interactable.ResetHoldTime();
                }
                interactionHoldProgress.fillAmount = interactable.GetHoldTime();
                break;

            // helpful error for us in the future
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
    }
}