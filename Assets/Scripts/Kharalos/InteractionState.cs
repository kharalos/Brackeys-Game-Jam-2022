using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionState : AIState 
{
    private string animationKey;
    private float interactionTimer;
    private Quaternion targetRot;
    private Vector3 targetPos;
    private bool interacting;
    public AIStateId GetId(){
        return AIStateId.Interaction;
    }
    public void Enter(AIAgent agent){
        //agent.animator.SetTrigger("run");
        SetData(agent.interactionPoints[0]);
        agent.navAgent.SetDestination(targetPos);
    }
    private void SetData(Transform interPoint){
        AIInteractionData data = interPoint.GetComponent<AIInteractionPoint>().GetData();
        animationKey = data.interactionAnimationTrigger;
        interactionTimer = data.interactionTime;
        targetRot = interPoint.rotation;
        targetPos = interPoint.position;
    }
    public void Update(AIAgent agent){

        if (!agent.navAgent.pathPending)
        {
            if (agent.navAgent.remainingDistance <= agent.navAgent.stoppingDistance)
            {
                if (!agent.navAgent.hasPath || agent.navAgent.velocity.sqrMagnitude == 0f)
                {
                    agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, targetRot, Time.deltaTime * 5);
                    if(!interacting){
                        interacting = true;
                        //agent.animator.SetTrigger(animationKey);
                        Debug.Log(animationKey);
                        //interaction
                    }
                    else
                    {
                        interactionTimer -= Time.deltaTime;
                        if(interactionTimer < 0.0f){

                        }
                    }
                }
            }
        }
    }
    public void Exit(AIAgent agent){
    }
}
