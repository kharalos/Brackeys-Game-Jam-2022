using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredState : AIState
{
    public AIStateId GetId(){
        return AIStateId.Scared;
    }
    private float timer = 6f;
    private float frightenedTime = 0;
    private bool reachedPlace = false;
    public void Enter(AIAgent agent){
        frightenedTime++;

        reachedPlace = false;
        
        agent.navAgent.speed = 5f;
        if(frightenedTime > 1){
            agent.navAgent.SetDestination(agent.FindExit());
        }
        else{
            agent.navAgent.SetDestination(agent.targetLoc);
        }
    }
    public void Update(AIAgent agent){
        if (!agent.navAgent.pathPending)
        {
            if (agent.navAgent.remainingDistance <= agent.navAgent.stoppingDistance)
            {
                if (agent.navAgent.velocity.sqrMagnitude < 1f)
                {
                    if(!reachedPlace){
                        reachedPlace = true;
                        agent.animator.SetTrigger("scared");
                    }
                    else{
                        timer -= Time.deltaTime;
                    }
                    if(timer < 0)
                    {
                        agent.stateMachine.ChangeState(AIStateId.Idle);
                        agent.GetComponent<Customer>().ResetFright();
                    }
                }
            }
        }
    }
    public void Exit(AIAgent agent){
        agent.animator.SetTrigger("idle");
    }
}
