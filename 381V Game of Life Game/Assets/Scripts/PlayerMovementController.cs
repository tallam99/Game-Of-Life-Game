using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementController : MonoBehaviour
{
    public static PlayerMovementController instance;

    private float x;
    private float z;
    private Vector3 velocity;
    private bool is_grounded;
    private bool playerAlive;

    private Vector3 lastPosition;
    private float totalDistance;

    public CharacterController controller;
    public float speed;
    public float gravity;
    public Transform ground_check;
    public float ground_dist;
    public LayerMask ground_mask;
    public float jump_height;

    public AudioSource jumpAudioData;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerAlive = true;
        totalDistance = 0;
        lastPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerAlive)
        {
            is_grounded = Physics.CheckSphere(ground_check.position, ground_dist, ground_mask);

            if(is_grounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            Vector3 move_dir = transform.right * x + transform.forward * z;

            float distance = Vector3.Distance(lastPosition, transform.position);
            totalDistance += distance;
            lastPosition = transform.position;

            distance += Mathf.Sqrt(move_dir.x * move_dir.x + move_dir.z + move_dir.z);

            controller.Move(move_dir * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && is_grounded)
            {
                jumpAudioData.Play();
                velocity.y = Mathf.Sqrt(jump_height * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    public void SetCoors(float x, float z)
    {
        this.x = x;
        this.z = z;
        this.lastPosition = new Vector3(x, 5, z);
    }

    public float GetDistance()
    {
        return totalDistance;
    }

    public void Die()
    {
        playerAlive = false;
    }
}
