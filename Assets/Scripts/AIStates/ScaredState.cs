using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredState : AIState
{
    public AIStateId GetId(){
        return AIStateId.Scared;
    }
    private float timer;
    public void Enter(AIAgent agent){
        timer = Random.Range(2f,3f);
    }
    public void Update(AIAgent agent){
        timer -= Time.deltaTime;
        if(timer < 0.0f) agent.stateMachine.ChangeState(AIStateId.Scared); 
        if(Vector3.Distance(agent.player.position, agent.transform.position) > 4f){
            agent.stateMachine.ChangeState(AIStateId.Idle);
        }
    }
    public void Exit(AIAgent agent){
        timer = 0;
    }
}
