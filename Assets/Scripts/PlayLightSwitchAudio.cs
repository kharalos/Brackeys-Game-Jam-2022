using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LightSwitch),typeof(AudioSource))]
public class PlayLightSwitchAudio : MonoBehaviour
{

    [SerializeField] List<AudioClip> clipsSwitchOn;
    [SerializeField] List<AudioClip> clipsSwitchOff;

    LightSwitch lightSwitch;
    AudioSource source;
    private void Start() {
        source = GetComponent<AudioSource>();
        lightSwitch = GetComponent<LightSwitch>();

        lightSwitch.OnInteract.AddListener(() => {
            if (!lightSwitch.isOn) PlayRandomClip(clipsSwitchOff);
            else PlayRandomClip(clipsSwitchOn);
        });
    }

    public void PlayRandomClip(List<AudioClip> clips) {
        var clip = clips[Random.Range(0, clips.Count)];
        source.clip = clip;
        source.Play();
    }
}