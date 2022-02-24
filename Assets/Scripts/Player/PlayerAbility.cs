using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] Collider scareCollider;

    //void Update() {
    //    if (PlayerInput.Interact) {
    //        StartCoroutine(Scare());
    //    }
    //}

    //IEnumerator Scare() {
    //    scareCollider.enabled = true;
    //    yield return null;
    //    scareCollider.enabled = false;
    //}
}
