using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttackPlayer : Node
{
    private Transform lastTarget;
    // private EnemyManager enemyManager; // this stuff but make it for player instead

    private float attackTime = 1f;
    private float attackCounter = 0;

    public TaskAttackPlayer(Transform transform) { }

    public override NodeState Evaluate()
    {
        Debug.Log("Enemy entered TaskAttackPlayer");
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
