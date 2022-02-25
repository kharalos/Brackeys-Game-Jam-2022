using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class ActorAnim : MonoBehaviour {
    [SerializeField] PlayerInteraction playerInteraction;
    [SerializeField] AnimancerComponent anim;
    [SerializeField] AnimationClip idle;
    [SerializeField] AnimationClip run;
    //[SerializeField] ClipTransition idle;
    //[SerializeField] ClipTransition run;
    [SerializeField] ClipTransition interact;
    [SerializeField] ClipTransition scare;

    bool isPlayingScareAnim = false;
    bool isPlayingInteractAnim = false;

    AnimancerState currentState;

    private void Start() {
        ScareAbility.OnScareAttack += () => {
            isPlayingScareAnim = true;
            anim.Play(scare).Events.OnEnd = () => {
                isPlayingScareAnim = false;
            };
        };
    }

    private void Update() {
        if (PlayerInput.InteractKeyDown && ScareAbility.InteractablesWithinPlayer.Count > 0) {
            isPlayingInteractAnim = true;
            anim.Play(interact).Events.OnEnd = () => {
                isPlayingInteractAnim = false;
            };
        }
        {
        //if (!isPlayingScareAnim && !isPlayingInteractAnim) {
        //    if (PlayerMovement.IsMoving)    anim.Play(run);
        //    else                            anim.Play(idle);
        //}

        //if (!isPlayingScareAnim && !isPlayingInteractAnim) {
        //    if (PlayerMovement.IsMoving) {
        //        state.Clip = run.Clip;
        //        state.Play();
        //    }
        //    else {
        //        anim.Play(idle);
        //    }
        //}

        //if (!isPlayingScareAnim && !isPlayingInteractAnim) {
        //    if (PlayerMovement.IsMoving) {
        //        if (currentState?.Clip == run.Clip) return;
        //        currentState = anim.Play(run);
        //    }
        //    else {
        //        if (currentState?.Clip == idle.Clip) return;
        //        anim.Play(idle);
        //    }
        //}
        }
        if (!isPlayingScareAnim && !isPlayingInteractAnim) {
            if (PlayerMovement.IsMoving)    anim.Play(run,0.25f, FadeMode.FixedSpeed);
            else                            anim.Play(idle,0.25f, FadeMode.FixedSpeed);
        }
    }
}