using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareAbility : MonoBehaviour {

    public static List<Interactable> InteractablesWithinPlayer = new List<Interactable>();

    [SerializeField] AK.Wwise.Event myEvent;

    [SerializeField] float scareAmount = 30;
    [SerializeField] float scareCooldown = 1;

    [SerializeField] UIBar manaBar;
    [SerializeField] float manaCost_scare = 10;
    public float manaCost_interact = 10;
    [SerializeField] float maxMana = 100;
    [SerializeField] float startingMana = 20;
    [SerializeField] float manaRegen = 10;
    
    public float CurrentMana { get; private set; }
    public float CurrentScareCooldown { get; private set; }

    List<Collider> collidersInsideTriggerZone = new List<Collider>();

    /// <summary>
    /// if there is enough mana, it will spend the amount and return true.
    /// </summary>
    public bool TrySpendMana(float amount) {
        if (CurrentMana >= amount) {
            CurrentMana -= amount;
            return true;
        }
        else return false;
    }

    private void Start() {
        CurrentMana = startingMana;
        CurrentScareCooldown = 0;
    }

    private void Update() {
        //Handle scare ability
        {
            //handle cooldown
            if (CurrentScareCooldown > 0) 
                CurrentScareCooldown = Mathf.Max(CurrentScareCooldown - Time.deltaTime, 0);

            if (PlayerInput.InteractKeyDown
                && CurrentMana >= manaCost_scare
                && CurrentScareCooldown <= 0
                && InteractablesWithinPlayer.Count == 0) {

                foreach (var item in collidersInsideTriggerZone) {
                    Debug.Log($"Boo! {item.gameObject.name}");
                    item.GetComponent<Customer>().ScareMe(scareAmount);
                }
                CurrentMana -= manaCost_scare;
                CurrentScareCooldown += scareCooldown;
            }
        }


        //handle mana regen & display
        CurrentMana += manaRegen * Time.deltaTime;
        if (CurrentMana > maxMana) CurrentMana = maxMana;
        manaBar.UpdateBarPercentFill(CurrentMana / maxMana);
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
