using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareAbility : MonoBehaviour {

    [SerializeField] AK.Wwise.Event myEvent;

    [SerializeField] float scareAmount = 30;
    [SerializeField] float scareCooldown = 1;

    [SerializeField] UIBar manaBar;
    [SerializeField] float manaCost_scare = 10;
    [SerializeField] float maxMana = 100;
    [SerializeField] float startingMana = 20;
    [SerializeField] float manaRegen = 10;
    
    float currentMana;
    float currentScareCooldown = 0;

    List<Collider> collidersInsideTriggerZone = new List<Collider>();

    private void Start() {
        currentMana = startingMana;
    }

    private void Update() {
        //Handle scare ability
        {
            //handle cooldown
            if (currentScareCooldown > 0) 
                currentScareCooldown = Mathf.Max(currentScareCooldown - Time.deltaTime, 0);

            if (PlayerInput.Interact && currentMana >= manaCost_scare && currentScareCooldown <= 0) {
                foreach (var item in collidersInsideTriggerZone) {
                    Debug.Log($"Boo! {item.gameObject.name}");
                    item.GetComponent<Customer>().ScareMe(scareAmount);
                }
                currentMana -= manaCost_scare;
                currentScareCooldown += scareCooldown;
            }
        }


        //handle mana regen & display
        currentMana += manaRegen * Time.deltaTime;
        if (currentMana > maxMana) currentMana = maxMana;
        manaBar.UpdateBarPercentFill(currentMana / maxMana);
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
