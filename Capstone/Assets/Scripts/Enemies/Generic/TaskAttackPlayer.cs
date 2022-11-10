using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttackPlayer : Node
{
    private Transform lastTarget;

    private float attackTime = 1f;
    private float attackCounter = 0;

    public TaskAttackPlayer(Transform transform) { }

    public override NodeState Evaluate()
    {
        Debug.Log("Enemy entered TaskAttackPlayer");
        Transform target = (Transform)GetData("target");
        if (target != lastTarget)
        {
            lastTarget = target;
        }

        Collider[] colliders = Physics.OverlapSphere(target.position, GenEnemyBT.attackRange);

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            foreach (Collider collider in colliders)
            {
                //if (collider.gameObject == component.gameObject) continue; // was breaking it for some reason 

                if (collider.CompareTag("Player"))
                {
                    if (collider.gameObject.TryGetComponent<Player>(out Player player))
                    {
                        player.gameObject.GetComponent<Health>().health -= GenEnemyBT.damage;

                        if (player.gameObject.GetComponent<Health>().health <= 0) ClearData("target");
                    }
                }
            }

            attackCounter = 0f;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
