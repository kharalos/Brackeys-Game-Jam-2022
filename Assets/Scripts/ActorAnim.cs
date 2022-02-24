using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class ActorAnim : MonoBehaviour {
    [SerializeField] AnimationClip clip;
    [SerializeField] AnimancerComponent animancer;

    private void Start() {
        animancer.Play(clip);
    }
}
