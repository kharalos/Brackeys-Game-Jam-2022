using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayRandomAudio : MonoBehaviour
{
    [SerializeField] float pitchMin = 1;
    [SerializeField] float pitchMax = 1;
    [SerializeField] List<AudioClip> clips;

    AudioSource source;
    private void Start() {
        source = GetComponent<AudioSource>();
    }

    public void PlayRandomClip() {
        var clip = clips[Random.Range(0, clips.Count)];
        source.clip = clip;
        source.pitch = Random.Range(pitchMin, pitchMax);
        source.Play();
    }
}