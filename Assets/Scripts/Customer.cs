using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{

    [SerializeField] UIBar uiBar;
    [SerializeField] float maxFright = 100;
    [SerializeField] float frightDecayRate = 3;
    
    [SerializeField] Image fillBarImage;
    [Space]
    [SerializeField] AudioSource source;
    [SerializeField] List<AudioClip> scareClips;
    [SerializeField] List<AudioClip> frightClips;
    [SerializeField] float pitchMin = 0.9f;
    [SerializeField] float pitchMax = 1.1f;

    private AgentCustomer agent;

    float currentFright = 0;
    bool isPermanentlyScared = false;

    private void Start() {
        uiBar.UpdateBarPercentFill(currentFright/maxFright);
        agent = GetComponent<AgentCustomer>();
    }

    // Update is called once per frame
    void Update() {

        //fright decay & update UI.
        if (isPermanentlyScared == false) {
            currentFright -= frightDecayRate * Time.deltaTime;
            if (currentFright < 0) currentFright = 0;
        }
        uiBar.UpdateBarPercentFill(currentFright / maxFright);
    }

    public void ScareMe(float amount) {
        if (isPermanentlyScared) return;
        //Debug.Log($"I got scared by {amount} amount");
        currentFright += amount;
        if (currentFright >= maxFright) {
            PermanentlyScared();
            GameManager.Instance.IncreaseTimer();
            GameManager.Instance.IncreaseScore();
            //popuptext
            var v = transform.position;
            v.y += 5;
            GameManager.Instance.SpawnPopupText(v,$"+{GameManager.Instance.timerIncreaseAmount} Happiness");
            //Audio big scare
            PlayRandomAudio(frightClips);
        }
        else {
            //audio small scare
            PlayRandomAudio(scareClips);
        }
    }

    public void PlayRandomAudio(List<AudioClip> clips) {
        source.clip = clips[Random.Range(0, clips.Count)];
        source.pitch = Random.Range(pitchMin, pitchMax);
        source.Play();
    }

    public void PermanentlyScared() {
        //Debug.Log("Screw this, I'm outta here!");
        isPermanentlyScared = true;
        fillBarImage.color = Color.red;
        agent.Scared();
    }

    public void ResetFright()
    {
        currentFright = 0;
        isPermanentlyScared = false;
    }
}
