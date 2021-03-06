using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareAbility : MonoBehaviour {

    public static List<Interactable> InteractablesWithinPlayer = new List<Interactable>();
    public static event Action OnScareAttack;

    [SerializeField] float scareAmount = 30;
    [SerializeField] float scareCooldown = 1;

    [SerializeField] UIBar manaBar;
    [SerializeField] float manaCost_scare = 10;
    public float manaCost_interact = 10;
    [SerializeField] float maxMana = 100;
    [SerializeField] float startingMana = 20;
    [SerializeField] float manaRegen = 10;

    [SerializeField] List<AudioClip> scareSounds;
    [SerializeField] float scareSound_pitchMin = 1;
    [SerializeField] float scareSound_pitchMax = 1;
    [SerializeField] AudioSource audioSource_scareSounds;
    [SerializeField] ParticleSystem scareParticles;
    [SerializeField] List<AudioClip> giggleSounds;
    [SerializeField] AudioSource audioSource_giggleSounds;
    [SerializeField] float giggleSound_pitchMin = .9f;
    [SerializeField] float giggleSound_pitchMax = 1.1f;

    public float CurrentMana { get; private set; }
    public float CurrentScareCooldown { get; private set; }

    List<Collider> customersInsideTriggerZone = new List<Collider>();
    List<Collider> staffInsideTriggerZone = new List<Collider>();


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

        GameManager.Instance.OnScoreIncrease += () => {
            audioSource_giggleSounds.clip = giggleSounds[UnityEngine.Random.Range(0, giggleSounds.Count)];
            audioSource_giggleSounds.pitch = UnityEngine.Random.Range(giggleSound_pitchMin, giggleSound_pitchMax);
            audioSource_giggleSounds.Play();
        };

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

                //deal with customers.
                foreach (var item in customersInsideTriggerZone) {
                    //Debug.Log($"Boo! {item.gameObject.name}");
                    item.GetComponent<Customer>().ScareMe(scareAmount);
                }
                //deal with staff
                foreach (var item in staffInsideTriggerZone) {
                    item.GetComponent<Staff>().ScareMe();
                }
                CurrentMana -= manaCost_scare;
                CurrentScareCooldown += scareCooldown;

                OnScareAttack?.Invoke();
                FindObjectOfType<GhostAnimator>().TriggerScare();
                //scare audio
                audioSource_scareSounds.clip = scareSounds[UnityEngine.Random.Range(0, scareSounds.Count)];
                audioSource_scareSounds.pitch = UnityEngine.Random.Range(scareSound_pitchMin, scareSound_pitchMax);
                audioSource_scareSounds.Play();
                //scare fx
                scareParticles.Play();
            }
        }


        //handle mana regen & display
        CurrentMana += manaRegen * Time.deltaTime;
        if (CurrentMana > maxMana) CurrentMana = maxMana;
        manaBar.UpdateBarPercentFill(CurrentMana / maxMana);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Customer") {
            customersInsideTriggerZone.Add(other);
        }
        if (other.gameObject.tag == "Staff") {
            staffInsideTriggerZone.Add(other);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Customer" && customersInsideTriggerZone.Contains(other)) {
            customersInsideTriggerZone.Remove(other);
        }
        if (other.gameObject.tag == "Staff" && staffInsideTriggerZone.Contains(other)) {
            staffInsideTriggerZone.Remove(other);
        }
    }
}
