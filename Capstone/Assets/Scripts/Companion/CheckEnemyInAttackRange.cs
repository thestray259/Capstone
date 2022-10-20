using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class CheckEnemyInAttackRange : Node
{
    private Transform transform; 

    public CheckEnemyInAttackRange(Transform transform) { this.transform = transform; }

    public override NodeState Evaluate()
    {
        return base.Evaluate();
    }
}
