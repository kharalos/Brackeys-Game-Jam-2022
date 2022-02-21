using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : AIState
{
    private bool foundPlayer;
    public AIStateId GetId(){
        return AIStateId.Chase;
    }

    public void Enter(AIAgent agent){
        //agent.animator.SetTrigger("run");
        foundPlayer = false;
    }
    public void Update(AIAgent agent){
        if(foundPlayer) return;
        agent.navAgent.SetDestination(agent.targetLoc);
        if(Vector3.Distance(agent.player.position, agent.transform.position) < 2f){
            //agent.stateMachine.ChangeState(AIStateId.AntiGhostState);
            foundPlayer = true;
        }
        else if(Vector3.Distance(agent.targetLoc, agent.transform.position) < 2f){
            agent.stateMachine.ChangeState(AIStateId.Idle);
        }
        
    }
    public void Exit(AIAgent agent){
        //agent.animator.SetTrigger("idle");
    }
}
