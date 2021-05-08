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
    private Vector3 startPosition;

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
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerAlive)
        {
            lastPosition = transform.position;
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
                jumpAudioData.Play();
                velocity.y = Mathf.Sqrt(jump_height * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            // this is a super weird hack for a start position glitch...
            if (transform.position.x == -1.0 && transform.position.z == -1.0)
            {
                transform.position = startPosition;
                lastPosition = transform.position;
            }

            // updated distance calculator to ignore y direction
            if (GameController.instance.gameActive)
            {
                Vector3 currentNoY = transform.position;
                Vector3 lastNoY = lastPosition;
                currentNoY.y = 0f;
                lastNoY.y = 0f;
                totalDistance += Vector3.Distance(lastNoY, currentNoY);
            }
        }
    }

    public void SetStart(Vector3 start)
    {
        transform.position = start;
        startPosition = start;
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
