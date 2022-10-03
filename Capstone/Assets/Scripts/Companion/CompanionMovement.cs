using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

[RequireComponent(typeof(NavMeshAgent))]
public class CompanionMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] Player2 player;
    [SerializeField] Companion2 companion;
    [Header("Idle Configs")]
    [SerializeField] [Range(0, 10f)] float rotationSpeed = 2f;
    [Header("Follow Configs")]
    [SerializeField] float followRadius = 2f;

    private Coroutine MovementCoroutine; 
    private Coroutine StateChangeCoroutine;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //player.OnStateChange += HandleStateChange; 
    }

    private void HandleStateChange(PlayerState OldState, PlayerState NewState)
    {
        if (StateChangeCoroutine != null) StopCoroutine(StateChangeCoroutine);

        switch (NewState)
        {
            case PlayerState.Initial:
                break;
            case PlayerState.Idle:
                StateChangeCoroutine = StartCoroutine((string)HandleIdlePlayer()); 
                break;
            case PlayerState.Moving:
                HandleMovingPlayer(); 
                break;
            default:
                break;
        }
    }

    private IEnumerable HandleIdlePlayer()
    {
        switch (companion.State)
        {
            case CompanionState.Initial:
                break;
            case CompanionState.Idle:
                if (MovementCoroutine != null) StopCoroutine(MovementCoroutine); 
                break;
            case CompanionState.Follow:
                yield return null; 
                yield return null;
                yield return new WaitUntil(() => companion.State == CompanionState.Idle);
                goto case CompanionState.Idle; 
            default:
                break;
        }
    }

    private void HandleMovingPlayer()
    {
        companion.ChangeState(CompanionState.Follow);
        if (MovementCoroutine != null) StopCoroutine(MovementCoroutine); 

        if (!agent.enabled)
        {
            agent.enabled = true;
            agent.Warp(transform.position); 
        }
        MovementCoroutine = StartCoroutine((string)FollowPlayer()); 
    }

    private IEnumerator RotateAroundPlayer()
    {
        WaitForFixedUpdate Wait = new WaitForFixedUpdate(); 
        while (true)
        {
            transform.RotateAround(player.transform.position, Vector3.up, rotationSpeed);
            yield return Wait; 
        }
    }

    private IEnumerable FollowPlayer()
    {
        yield return null;

        NavMeshAgent playerAgent = player.GetComponentInChildren<NavMeshAgent>();
        Vector3 playerDest = playerAgent.destination;
        Vector3 positionOffset = followRadius * new Vector3(
            Mathf.Cos(2 * Mathf.PI * Random.value),
            0,
            Mathf.Sin(2 * Mathf.PI * Random.value)
            ).normalized;

        agent.SetDestination(playerDest + positionOffset);

        yield return null;
        yield return new WaitUntil(() => agent.remainingDistance <= agent.stoppingDistance);

        companion.ChangeState(CompanionState.Idle); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
