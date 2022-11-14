using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class GenEnemyIdle : Node
{
    Transform transform;
    Vector3 initialPosition;
    Quaternion quaternion = new Quaternion(0, 0, 0, 0); 

    public GenEnemyIdle(Transform transform) { this.transform = transform; }

    void Start()
    {
        initialPosition = this.transform.position; 
    }

    public override NodeState Evaluate()
    {
        // every 3-5 seconds, do an idle animation 
        Debug.Log("Enemy entered Idle"); 

        if (transform.position != initialPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, GenEnemyBT.speed * Time.deltaTime);
            transform.LookAt(initialPosition); 
        }

        transform.rotation = quaternion;

        state = NodeState.RUNNING;
        return state; 
    }
}
