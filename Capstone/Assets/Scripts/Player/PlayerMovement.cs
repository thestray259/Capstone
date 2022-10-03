using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Player2 player; 
    [SerializeField] Camera camera = null;
    [SerializeField] LayerMask LayerMask;
    [SerializeField] Vector3 CameraOffset;
    [SerializeField] Transform view;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float turnRate;

    public Vector3 velocity = Vector3.zero;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); 
    }

    void Update()
    {
        // movement 
        // xz movement
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction = Vector3.ClampMagnitude(direction, 1);

        // convert direction from world space to view space
        Quaternion viewSpace = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);
        direction = viewSpace * direction;

        // y movement
        // !!! check if grounded for jump !!!
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
        }
        velocity += Physics.gravity * Time.deltaTime;

        // move character (xyz)
        controller.Move(((direction * speed) + velocity) * Time.deltaTime);

        if (agent.enabled && agent.remainingDistance <= agent.stoppingDistance && player.State == PlayerState.Moving)
        {
            player.ChangeState(PlayerState.Idle); 
        }
    }

    private void LateUpdate()
    {
        //camera.transform.position = transform.position + CameraOffset; 
    }
}
