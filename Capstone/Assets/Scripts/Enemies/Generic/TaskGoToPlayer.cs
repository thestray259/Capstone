using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToPlayer : Node
{
    Transform transform;
    public float timer = 0.0f; 

    public TaskGoToPlayer(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        Debug.Log("Enemy entered TaskGoToPlayer");
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(transform.position, target.position) > 0.5f && timer < 3.0f) // quick fix, need to change later bc doesn't go back to idle 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, GenEnemyBT.speed * Time.deltaTime);
            transform.LookAt(target.position);

            if (Vector3.Distance(transform.position, target.position) < 5.0f) timer = 0;
            if (Vector3.Distance(transform.position, target.position) > 5.0f)
            {
                timer += Time.deltaTime;
                Debug.Log("Timer: " + timer); 
            }            
        }

        // if player is too far away, start timer 
        // stop and reset timer if sees player 
        // when timer gets to x value, return failure and exit 

        if (timer >= 3.0f)
        {
            //CheckForPlayer checkForPlayer = new CheckForPlayer(transform);
            //checkForPlayer.colliders = null; 
            state = NodeState.FAILURE;
            return state; 
        }
        else
        {
            state = NodeState.RUNNING;
            return state;
        }
    }
}
