using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Movement movement; 

    void Start()
    {
        
    }

    void Update()
    {
        animator.SetFloat("speed", movement.velocity.magnitude);
        Debug.Log("Enemy Speed: " + movement.velocity.magnitude); 
    }
}
