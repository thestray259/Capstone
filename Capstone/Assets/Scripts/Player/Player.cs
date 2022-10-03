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

    public Vector3 velocity = Vector3.zero;
    float airTime = 0;

    void Start()
    {
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
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
        }
        velocity += Physics.gravity * Time.deltaTime;

        // move character (xyz)
        controller.Move(((direction * speed) + velocity) * Time.deltaTime);
    }
}
