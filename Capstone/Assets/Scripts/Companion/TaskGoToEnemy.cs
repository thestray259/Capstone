using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class TaskGoToEnemy : Node
{
    private Transform transform; 

    public TaskGoToEnemy(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered TaskGoToEnemy");
        Transform target = (Transform)GetData("target"); 

        if (Vector3.Distance(transform.position, target.position) > 0.5f)
        {
            // change so that comp sees where player is and stays a distance away
            transform.position = Vector3.MoveTowards(transform.position, target.position, CompanionBT.speed * Time.deltaTime);
            transform.LookAt(target.position); 
        }

        state = NodeState.RUNNING;
        return state; 
    }
}
