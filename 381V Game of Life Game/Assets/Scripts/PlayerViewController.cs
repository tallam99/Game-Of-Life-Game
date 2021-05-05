using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewController : MonoBehaviour
{
    private Vector2 rotation; // current rotation, in degrees
    private Vector2 velocity; // current rotation velocity, in degrees
    private Vector2 last_input_event; // last received non-zero input value
    private float input_lag_timer; // time since last received non-zero input value

    public Vector2 sensitivity; // Maximum speed in degrees/s
    public Vector2 acceleration; // rotation accleration in degrees/second
    public float input_lag_period;
    public RotationDirection rot_dir;

    public enum RotationDirection
    {
        None,
        Horizontal = (1 << 0),
        Vertical = (1 << 1)
    }

    private Vector2 getInput()
    {
        input_lag_timer += Time.deltaTime;

        Vector2 input = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );

        if ((Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y)) == false || input_lag_timer >= input_lag_period)
        {
            last_input_event = input;
            input_lag_timer = 0;
        }

        return input;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target_velocity = getInput() * sensitivity;

        if((rot_dir & RotationDirection.Horizontal) == 0)
        {
            target_velocity.x = 0;
        }
        if((rot_dir & RotationDirection.Vertical) == 0)
        {
            target_velocity.y = 0;
        }

        velocity = new Vector2(
            Mathf.MoveTowards(velocity.x, target_velocity.x, acceleration.x * Time.deltaTime),
            Mathf.MoveTowards(velocity.y, target_velocity.y, acceleration.y * Time.deltaTime)
            );
        rotation += velocity * Time.deltaTime;

        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f);

        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
    }
}   