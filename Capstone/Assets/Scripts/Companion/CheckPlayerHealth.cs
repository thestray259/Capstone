using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class CheckPlayerHealth : Node
{
    private Transform transform;
    private UnityEngine.GameObject playerObject;

    public CheckPlayerHealth(Transform transform, UnityEngine.GameObject playerObject)
    {
        this.transform = transform;
        this.playerObject = playerObject; 
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered CheckPlayerHealth"); 
        // check if player health is below a certain point/percentage 
        // if yes, return success 
        // else return failure 

        if (playerObject.GetComponent<Health>().health <= 50)
        {
            Debug.Log("CheckPlayerHealth success"); 
            state = NodeState.SUCCESS;
            return state; 
        }
        else
        {
            Debug.Log("CheckPlayerHealth failure"); 
            state = NodeState.FAILURE;
            return state;
        }
    }
}
