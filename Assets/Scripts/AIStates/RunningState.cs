using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : AIState
{
    public AIStateId GetId(){
        return AIStateId.Running;
    }
    public void Enter(AIAgent agent){
        //agent.animator.SetTrigger("run");
        agent.navAgent.SetDestination(agent.targetLoc);
    }
    public void Update(AIAgent agent){

    }
    public void Exit(AIAgent agent){
    }
}
