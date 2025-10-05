using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseFSM : StateMachineBehaviour
{

    [Header(("Base config"))]
    public RobotAI NPC;
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject opponent;
    public float speed = 2.0f;
    public float rotSpeed = 1.0f;
    public float accuracy = 3.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.GetComponent<RobotAI>();
        opponent = NPC.GetPlayer();
        agent = NPC.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

}