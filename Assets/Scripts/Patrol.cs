using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : NPCBaseFSM
{
    [Header("Patrol config")]
    public float wanderRadius = 2f;
    public float wanderDistance = 8f;
    public float wanderJitter = 0.2f;
    
    private Vector3 wanderTarget = Vector3.zero;
    

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Wander();
    }

    private void Wander()
    {
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
            0,
            Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;
    
        Debug.DrawRay(NPC.transform.position, NPC.gameObject.transform.TransformVector(wanderTarget),Color.red);
        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);

        Debug.DrawRay(NPC.transform.position+NPC.gameObject.transform.TransformVector(wanderTarget), NPC.gameObject.transform.TransformVector(new Vector3(0, 0, wanderDistance)),Color.green);
    
        Vector3 targetWorld = NPC.transform.position + NPC.gameObject.transform.TransformVector(targetLocal);
        
        agent.SetDestination(targetWorld);
    }
}