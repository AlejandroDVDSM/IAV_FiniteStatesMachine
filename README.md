# IAV_FiniteStatesMachine

A basic project to learn about AI and states machines. This project features a robot that is always patrolling. If the 
robot make visual contact with the player its state will change from "Patrol" to "Chase" and when it is close enough to
the player it will start attacking. If the robot health is reduced to a certain point it will try to hide behind a block
to start healing.

![project_iav_finitestatesmachine](https://github.com/user-attachments/assets/ea833066-a25c-4fcc-ac17-7e96a425852b)

<img width="1377" height="726" alt="Captura de pantalla 2025-10-05 162032" src="https://github.com/user-attachments/assets/f27da6db-d5c0-4560-8277-68d2878880ec" />

The robot has the `RobotAI` script attached to it with the logic to fire the player or to know if it is making
visual contact with the player.

```csharp
    void Update()
    {
        anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position) + _stoppingDistance);
        Debug.Log($"Distance to player: {Vector3.Distance(transform.position, player.transform.position)}");
        anim.SetBool("isChasing", CanSeeTarget());
    }
    
    bool CanSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = player.transform.position - this.transform.position;
        if (Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            if (raycastInfo.transform.gameObject.CompareTag("Player") && Vector3.Distance(transform.position, raycastInfo.transform.position) < 40)
                return true;
        }
        
        return false;
    }
```

Besides `RobotAI`, this project have a `StateMachineBehaviour` named `NPCBaseFSM` with the simple logic of storing a reference to the
robot and the player.

```csharp
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
```
Each state present on the robot animator have its own `StateMachineBehaviour` that inherits from `NPCBaseFSM` to override
its functions, such as `OnStateEnter`, `OnStateUpdate` and `OnStateExit`.

```csharp
public class Attack : NPCBaseFSM
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        NPC.StartFiring();
        agent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC.transform.LookAt(opponent.transform.position);

        if (NPC.GetHP() <= 25)
            animator.SetBool("isHealing", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC.StopFiring();
        agent.isStopped = false;
    }
}
```

<img width="1106" height="909" alt="Captura de pantalla 2025-10-05 162858" src="https://github.com/user-attachments/assets/f6c9d080-73de-4078-8efe-43a90065a418" />
