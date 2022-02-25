using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareWhenObjectInteract : MonoBehaviour
{
    [SerializeField] Interactable interactable;
    List<Collider> collidersInsideTriggerZone = new List<Collider>();

    const float scareAmount = 20f;

    private void Start() {
        interactable.OnInteract.AddListener(() => {
            foreach (var item in collidersInsideTriggerZone) {
                item.GetComponent<Customer>().ScareMe(scareAmount);
            }
        });
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Customer" || other.gameObject.tag == "Staff") {
            collidersInsideTriggerZone.Add(other);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Customer" && collidersInsideTriggerZone.Contains(other)) {
            collidersInsideTriggerZone.Remove(other);
        }
    }
}
