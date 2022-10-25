using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    //[SerializeField] Animator animator;
    [SerializeField] Transform view;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float turnRate;
    [SerializeField] ForceMode forceMode; 

    Rigidbody rb;
    Vector3 force = Vector3.zero; 
    [SerializeField] Vector3 velocity = Vector3.zero;
    bool isGrounded = false; 
    float airTime = 0;
    float distToGround = 0.5f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        view = (view == null) ? Camera.main.transform : view;
    }

    void Update()
    {
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
/*        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jumping");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 

            //if (velocity.y < 0) velocity.y = 0;
            //velocity.y = jumpForce;
            //rb.velocity += (Vector3.up * jumpForce); 
            Debug.Log("Y Velocity: " + velocity.y); 
            Debug.Log("RB Y Velocity: " + rb.velocity.y); 
            *//*            airTime = 0;
                        if (velocity.y < 0) velocity.y = 0;
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            velocity.y = jumpForce;
                        }*//*
        }*/
/*        else
        {
            airTime += Time.deltaTime;
        }*/
        velocity += Physics.gravity * Time.deltaTime;

        // move character (xyz)
        Move(view);
        OnJump(); 

        // face direction (needs fixing)
        if (direction.magnitude > 0)
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), turnRate * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, forceMode);
        GroundCheck(); 
    }

    private void Move(Transform view)
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // move forward
            transform.position += speed * Time.deltaTime * view.forward;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // move left 
            transform.position += speed * Time.deltaTime * -view.right; 
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            // move backwards 
            transform.position += speed * Time.deltaTime * -view.forward;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // move right 
            transform.position += speed * Time.deltaTime * view.right;
        }
    }

    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f))
        {
            Debug.Log("Grounded");
            isGrounded = true; 
        }
        else
        {
            Debug.Log("Not Grounded");
            isGrounded = false; 
        }
    }

    private void OnJump()
    {
        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Jumping"); 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        } 
    }
}
