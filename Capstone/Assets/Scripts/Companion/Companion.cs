using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class Companion : Agent
{
    [SerializeField] GameObject player;
    [SerializeField] NavMeshMovement navMeshMovement;
    //[SerializeField] CharacterController controller; 
    [SerializeField] float jumpForce;

    public float speed = 5; 
    public FloatRef playerDistance;
    public Vector3 destination;
    public Quaternion rotation;
    public Vector3 forward;
    public bool isGrounded = false;
    public Vector3 velocity = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        //if (controller.isGrounded) isGrounded = true;
        //else isGrounded = false; 

        Move(); 
    }

    void Move()
    {
        // it's spinning on the y a bunch???
        
        //Vector3 destination = transform.position + forward * Random.Range(10f, 15f);

        playerDistance.value = Vector3.Distance(this.transform.position, player.transform.position);

        if (playerDistance.value >= 2.5f) // if player is too far away from companion 
        {
            // move towards player
            rotation = Quaternion.AngleAxis(Random.Range(-90, 90), Vector3.up);
            forward = rotation * Vector3.forward;

            destination = player.transform.position + forward;// * Random.Range(1f, 3f);// + new Vector3(1, 0, 1);
            if (navMeshMovement.navMeshAgent.isStopped == true)
            {
                movement.Resume();
            }
            else movement.MoveTowards(destination); 
            
            if (playerDistance.value <= 2.4f)
            {
                movement.Stop(); 
            }
        }

        // if grounded and y value above 1.5f 
        if (isGrounded == true && destination.y >= 1.5f)
        {
            // jump 
            velocity.y = jumpForce;
        }
        velocity += Physics.gravity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isGrounded = true; 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isGrounded = false; 
        }
    }

    /*    private void OnCollisionStay(Collision collision)
        {
            isGrounded = true; 
        }*/
}
