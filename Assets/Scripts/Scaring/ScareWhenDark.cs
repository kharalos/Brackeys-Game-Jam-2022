using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareWhenDark : MonoBehaviour
{

    List<Collider> collidersInsideTriggerZone = new List<Collider>();
    [SerializeField] LightSwitch lightSwitch;

    const float scareRate = 10;
    private void Start() {
        lightSwitch.OnInteract.AddListener(() => {
            if (lightSwitch.isOn == false) {
                foreach (var item in collidersInsideTriggerZone) {
                    Customer customer = item.GetComponent<Customer>();
                    if (customer != null) StartCoroutine(ScareCustomerWhileDark());
                    IEnumerator ScareCustomerWhileDark() {
                        while (lightSwitch.isOn == false) {
                            customer.ScareMe(scareRate * Time.deltaTime);
                            yield return null;
                        }
                    }
                }
            }
        });
    }



    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Customer") {
            collidersInsideTriggerZone.Add(other);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Customer" && collidersInsideTriggerZone.Contains(other)) {
            collidersInsideTriggerZone.Remove(other);
        }
    }
}
