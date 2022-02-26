using System.Collections;
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
    public GameObject CanvasGO;
    Camera cam;
    public bool successfulHit = false;
    Interactable i;
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

        if (successfulHit)
            HandleInteraction(i);
    }

    public void UpdateUI(bool _successfulHit, Interactable interactable)
    {
        successfulHit = _successfulHit;
        if (_successfulHit)
        {
            i = interactable;
            CanvasGO.transform.position = i.CanvasPlacement.position;
            interactionText.text = interactable.GetDescription();
            //successfulHit = true;
            //Debug.Log("E");
            interactionClickGO.SetActive(interactable.interactionType == Interactable.InteractionType.Click);
            interactionHoldGO.SetActive(interactable.interactionType == Interactable.InteractionType.Hold);
            
            interactionHoldGO.SetActive(true);
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
                if (PlayerInput.InteractKeyDown)
                {

                    interactable.Interact();
                    //UpdateUI(true, interactable);
                    UpdateUI(false, interactable);

                    FindObjectOfType<GhostAnimator>().TriggerInteract();
                    if (ScareAbility.InteractablesWithinPlayer.Contains(interactable)) {
                        ScareAbility.InteractablesWithinPlayer.Remove(interactable);
                    }


                    ///Respawn Interactables. Disabled cuz bugs.
                    //StartCoroutine(RespawnAfterDelay());
                    //IEnumerator RespawnAfterDelay() {
                    //    GameObject go = ((InteractableEvent)interactable).prefab;
                    //    var i = interactable.transform;
                    //    Vector3 pos = i.position;
                    //    Quaternion rot = i.rotation;
                    //    yield return new WaitForSeconds(2);
                    //
                    //    Destroy(interactable.gameObject);
                    //    var newGo = GameObject.Instantiate(go,pos,rot);
                    //    newGo.SetActive(true);
                    //    newGo.GetComponent<SphereCollider>().enabled = true;
                    //}
                }
                break;
            case Interactable.InteractionType.Hold:
                if (PlayerInput.InteractKey)
                {
                    // we are holding the key, increase the timer until we reach 1f
                    interactable.IncreaseHoldTime();
                    if (interactable.GetHoldTime() > 1f)
                    {
                        if (ScareAbility.InteractablesWithinPlayer.Contains(interactable) == true)
                            ScareAbility.InteractablesWithinPlayer.Remove(interactable);

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