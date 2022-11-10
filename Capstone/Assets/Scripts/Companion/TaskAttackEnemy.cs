using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class TaskAttackEnemy : Node
{
    private Transform lastTarget;

    private float attackTime = 1f;
    private float attackCounter = 0;

    private Component component; 

    public TaskAttackEnemy(Transform transform) { }

    public override NodeState Evaluate()
    {
        Debug.Log("Companion entered TaskAttackEnemy"); 
        Transform target = (Transform)GetData("target");

        Collider[] colliders = Physics.OverlapSphere(target.position, CompanionBT.attackRange); // used to be lastTarget.position but that was breaking it for some reason

        if (target != lastTarget)
        {
            lastTarget = target; 
        }

        attackCounter += Time.deltaTime; 
        if (attackCounter >= attackTime)
        {
            foreach (Collider collider in colliders)
            {
                //if (collider.gameObject == component.gameObject) continue; // was breaking it for some reason 

                if (collider.CompareTag("Enemy"))
                {
                    if (collider.gameObject.TryGetComponent<GenEnemyBT>(out GenEnemyBT genEnemyBT))
                    {
                        genEnemyBT.gameObject.GetComponent<Health>().health -= CompanionBT.damage;

                        if (genEnemyBT.gameObject.GetComponent<Health>().health <= 0) ClearData("target"); 
                    }
                }
            }

            attackCounter = 0f; 
        }

        state = NodeState.RUNNING;
        return state; 
    }
}
