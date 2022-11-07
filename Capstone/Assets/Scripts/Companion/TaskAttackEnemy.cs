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

    private Component component; 

    public TaskAttackEnemy(Transform transform) { }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered TaskAttackEnemy"); 
        Transform target = (Transform)GetData("target");

        Collider[] colliders = Physics.OverlapSphere(lastTarget.position, CompanionBT.attackRange);

        if (target != lastTarget)
        {
            // enemyManager = target.GetComponent<EnemyManager>(); 
            lastTarget = target; 
        }

        attackCounter += Time.deltaTime; 
        if (attackCounter >= attackTime)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject == component.gameObject) continue;

                //if (tagName == "" || collider.CompareTag(tagName))
                {
                    if (collider.gameObject.TryGetComponent<GenEnemyBT>(out GenEnemyBT genEnemyBT))
                    {
                        genEnemyBT.gameObject.GetComponent<Health>().health -= CompanionBT.damage;
                    }
                }
            }

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
