using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class CheckForPlayer : Node
{
    private Transform transform;
    private static int _playerLayerMask = 1 << 7;

    public CheckForPlayer(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        Debug.Log("Enemy entered CheckForPlayer");
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, GenEnemyBT.fovRange, _playerLayerMask);

            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
