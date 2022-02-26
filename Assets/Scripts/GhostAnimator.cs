using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GhostAnimator : MonoBehaviour
{
    Animator animator;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", ((transform.position - pos) / Time.deltaTime).magnitude, 0.3f, 2f * Time.deltaTime);
        pos = transform.position;
    }

    public void TriggerScare(){
        animator.SetTrigger("scare");
    }
    public void TriggerInteract(){
        animator.SetTrigger("interact");
    }
}
