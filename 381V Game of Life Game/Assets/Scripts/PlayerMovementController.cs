using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private float x;
    private float z;
    private Vector3 velocity;
    private bool is_grounded;

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform ground_check;
    public float ground_dist = 0.4f;
    public LayerMask ground_mask;
    public float jump_height = 3f;

    // Update is called once per frame
    void Update()
    {
        is_grounded = Physics.CheckSphere(ground_check.position, ground_dist, ground_mask);

        if(is_grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move_dir = transform.right * x + transform.forward * z;

        controller.Move(move_dir * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && is_grounded)
        {
            velocity.y = Mathf.Sqrt(jump_height * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
