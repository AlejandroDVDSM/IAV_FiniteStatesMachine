using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : NPCBaseFSM
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Seek method
        agent.SetDestination(opponent.transform.position);
        
        // Pursue method
        // Pursue();
    }

    private void Pursue()
    {
        Vector3 targetDir = opponent.transform.position - NPC.transform.position;

        float lookAhead = targetDir.magnitude * this.speed / agent.speed;
        Debug.DrawRay(opponent.transform.position, opponent.transform.forward * lookAhead,Color.red);

        Debug.DrawRay(NPC.transform.position, opponent.transform.position + opponent.transform.forward * lookAhead - NPC.transform.position,Color.green);
        agent.SetDestination(opponent.transform.position + opponent.transform.forward * lookAhead);
    }
}