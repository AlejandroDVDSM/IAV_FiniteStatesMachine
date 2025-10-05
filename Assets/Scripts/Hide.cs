using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : NPCBaseFSM
{

    private bool isHealing;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // NPC.StartHealing();
        isHealing = false;

    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // CleverHide();
        SeekHide();
    }
    
    private void CleverHide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGO = World.Instance.GetHidingSpots()[0];

        for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDir = World.Instance.GetHidingSpots()[i].transform.position - opponent.transform.position;
            hideDir.y = 0.0f;
            Vector3 hidePos = World.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * 100;
            

            if (Vector3.Distance(NPC.transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                chosenDir = hideDir;
                chosenGO = World.Instance.GetHidingSpots()[i];
                dist = Vector3.Distance(NPC.transform.position, hidePos);
            }
        }

        Collider hideCol = chosenGO.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float distance = 250.0f;
        hideCol.Raycast(backRay, out info, distance);
        Debug.DrawRay(chosenSpot, -chosenDir.normalized * distance, Color.yellow);

        agent.SetDestination(info.point + chosenDir.normalized);
    }
    
    void SeekHide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDir = World.Instance.GetHidingSpots()[i].transform.position - opponent.transform.position;
            Vector3 hidePos = World.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * 10;

            if (Vector3.Distance(NPC.transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                dist = Vector3.Distance(NPC.transform.position, hidePos);
            }
        }

        Debug.Log($"Distance to hiding spot: {Vector3.Distance(NPC.transform.position, chosenSpot)}" );
        if (!isHealing && Vector3.Distance(NPC.transform.position, chosenSpot) < 5.0f)
        {
            isHealing = true;
            NPC.StartHealing();
        }

        agent.SetDestination(chosenSpot);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
