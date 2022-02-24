using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareAbility : MonoBehaviour {

    [SerializeField] AK.Wwise.Event myEvent;

    [SerializeField] float scareAmount = 30;

    List<Collider> collidersInsideTriggerZone = new List<Collider>();

    private void Update() {
        if (PlayerInput.Interact) {
            foreach (var item in collidersInsideTriggerZone) {
                Debug.Log($"Boo! {item.gameObject.name}");
                item.GetComponent<Customer>().ScareMe(scareAmount);
            }
        }
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
