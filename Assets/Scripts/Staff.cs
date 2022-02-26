using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] List<AudioClip> scareClips;
    [SerializeField] float pitchMin = 0.9f;
    [SerializeField] float pitchMax = 1.1f;

    public void ScareMe() {
        PlayRandomAudio(scareClips);
    }

    public void PlayRandomAudio(List<AudioClip> clips) {
        source.clip = clips[Random.Range(0, clips.Count)];
        source.pitch = Random.Range(pitchMin, pitchMax);
        source.Play();
    }
}
