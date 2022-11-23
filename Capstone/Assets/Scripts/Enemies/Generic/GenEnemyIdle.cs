using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class GenEnemyIdle : Node
{
    Transform transform;
    Vector3 initialPosition;

    private Animator animator; 

    public GenEnemyIdle(Transform transform) { this.transform = transform; animator = transform.GetComponent<Animator>(); }

    public override NodeState Evaluate()
    {
        // every 3-5 seconds, do an idle animation 
        Debug.Log("Enemy entered Idle");
        animator.SetBool("walking", false); 

        if (initialPosition == new Vector3(0, 0, 0)) initialPosition = transform.position;

        if (transform.position != initialPosition)
        {
            animator.SetBool("walking", true);
            Debug.Log("Enemy moving towards Initial Pos"); 
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, GenEnemyBT.speed * Time.deltaTime);
            transform.LookAt(initialPosition);            
        }

        state = NodeState.RUNNING;
        return state; 
    }
}
