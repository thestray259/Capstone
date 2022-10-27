using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class GenEnemyIdle : Node
{
    public GenEnemyIdle() { }

    public override NodeState Evaluate()
    {
        // every 3-5 seconds, do an idle animation 
        Debug.Log("Enemy entered Idle"); 

        state = NodeState.RUNNING;
        return state; 
    }
}
