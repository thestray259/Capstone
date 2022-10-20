using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class TaskAttackEnemy : Node
{
    private Transform lastTarget;
    // private EnemyManager enemyManager; 

    private float attackTime = 1f;
    private float attackCounter = 0; 

    public TaskAttackEnemy(Transform transform) { }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered TaskAttackEnemy"); 
        Transform target = (Transform)GetData("target"); 
        if (target != lastTarget)
        {
            // enemyManager = target.GetComponent<EnemyManager>(); 
            lastTarget = target; 
        }

        attackCounter += Time.deltaTime; 
        if (attackCounter >= attackTime)
        {
            // bool isEnemyDead = enemyManager.TakeHit(); 
/*            if (isEnemyDead)
            {
                ClearData("target"); 
            }
            else */
                attackCounter = 0f; 
        }

        state = NodeState.RUNNING;
        return state; 
    }
}
