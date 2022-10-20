using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree; 

public class Follow : Node
{
    private Transform transform;
    private Transform playerTransform;

    public Follow(Transform transform, Transform playerTransform)
    {
        this.transform = transform;
        this.playerTransform = playerTransform;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered Follow");
        if (Vector3.Distance(transform.position, playerTransform.position) > 3.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, CompanionBT.speed * Time.deltaTime);
            transform.LookAt(playerTransform.position); 
        }

        state = NodeState.RUNNING;
        return state; 
    }
}
