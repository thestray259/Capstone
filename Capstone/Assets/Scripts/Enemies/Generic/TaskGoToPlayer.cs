using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToPlayer : Node
{
    Transform transform; 

    public TaskGoToPlayer(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        Debug.Log("Enemy entered TaskGoToPlayer");
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(transform.position, target.position) > 0.5f && Vector3.Distance(transform.position, target.position) < 5.0f) // quick fix, need to change later bc doesn't go back to idle 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, GenEnemyBT.speed * Time.deltaTime);
            transform.LookAt(target.position);
        }

        state = NodeState.RUNNING;
        return state;
    }
}
