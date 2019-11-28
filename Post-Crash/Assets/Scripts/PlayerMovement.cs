using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject rotation_thing, data_container;
    public float speed, jump_takeoff_speed, height_standing, height_squatting, lean_distance, distance_to_ground, speed_multiplier_squatting;

    private Vector3 velocity;
    private float angular_speed, endgoal_horizontal, endgoal_vertical, gravity_fake, time_fake;
    private bool is_squatting, current_grounded, previous_grounded;

    private Transform transformation;
    private CharacterController controller;
    private CapsuleCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        transformation = GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        collider = GetComponent<CapsuleCollider>();
        
        angular_speed = Mathf.Sqrt((speed * speed / 2.0f));

        time_fake = 0.01666f;
        gravity_fake = Physics.gravity.y * time_fake;
    }

    // Update is called once per frame
    void Update()
    {
        Walk();

        BetterMovement();

        ApplyGravity();

        Jump();

        CycaBlyat();

        MovementLean(velocity.x);

        previous_grounded = current_grounded;
        current_grounded = IsGrounded();
    }

    void FixedUpdate()
    {
        velocity.x = Mathf.Lerp(velocity.x, endgoal_horizontal, 0.15f);
        velocity.z = Mathf.Lerp(velocity.z, endgoal_vertical, 0.15f);

        velocity = transformation.rotation * velocity;

        controller.Move(velocity);
    }

    private void Walk()
    {
        if (Input.GetButton(PlayerPrefs.GetString("Move Forward")))
        {
            if (Input.GetButton(PlayerPrefs.GetString("Move Right")))
            {
                endgoal_horizontal = 1 * angular_speed * time_fake;
                endgoal_vertical = 1 * angular_speed * time_fake;
            }
            else if (Input.GetButton(PlayerPrefs.GetString("Move Left")))
            {
                endgoal_horizontal = -1 * angular_speed * time_fake;
                endgoal_vertical = 1 * angular_speed * time_fake;
            }
            else
            {
                endgoal_vertical = 1 * speed * time_fake;
            }
        }
        else if (Input.GetButton(PlayerPrefs.GetString("Move Backward")))
        {
            if (Input.GetButton(PlayerPrefs.GetString("Move Right")))
            {
                endgoal_horizontal = 1 * angular_speed * time_fake;
                endgoal_vertical = -1 * angular_speed * time_fake;
            }
            else if (Input.GetButton(PlayerPrefs.GetString("Move Left")))
            {
                endgoal_horizontal = -1 * angular_speed * time_fake;
                endgoal_vertical = -1 * angular_speed * time_fake;
            }
            else
            {
                endgoal_vertical = -1 * speed * time_fake;
            }
        }
        else if (Input.GetButton(PlayerPrefs.GetString("Move Left")))
        {
            endgoal_horizontal = -1 * speed * time_fake;
        }
        else if (Input.GetButton(PlayerPrefs.GetString("Move Right")))
        {
            endgoal_horizontal = 1 * speed * time_fake;
        }

        if (!Input.GetButton(PlayerPrefs.GetString("Move Left"))
            && !Input.GetButton(PlayerPrefs.GetString("Move Right")))
        {
            endgoal_horizontal = 0;
        }

        if (!Input.GetButton(PlayerPrefs.GetString("Move Forward"))
            && !Input.GetButton(PlayerPrefs.GetString("Move Backward")))
        {
            endgoal_vertical = 0;
        }
    }

    private void BetterMovement()
    {
        // Better jumping and falling
        if (velocity.y < -0.00327654)
        {
            velocity.y += gravity_fake * time_fake;
        }
        else if (velocity.y > -0.00327654 && !(Input.GetButton(PlayerPrefs.GetString("Jump"))))
        {
            velocity.y += 0.5f * gravity_fake * time_fake;
        }
    }

    private void ApplyGravity()
    {
        if (IsGrounded())
        {
            velocity.y = (gravity_fake * time_fake);
        }
        else
        {
            velocity.y += (gravity_fake * time_fake);
        }
    }

    private bool IsGrounded()
    {
        if (controller.isGrounded)
        {
            return true;
        }

        Vector3 bottom = controller.transform.position - new Vector3(0, controller.height / 2, 0);

        RaycastHit hit;
        if (Physics.Raycast(bottom, new Vector3(0, -1, 0), out hit, 0.45f)
            && !(Input.GetKey(KeyCode.Space))
            && !Input.GetKeyDown(KeyCode.Space))
        {
            if (current_grounded && previous_grounded)
            {
                controller.Move(new Vector3(0, -hit.distance, 0));
                return true;
            }
        }
        return false;
    }

    private bool IsBeneathSomething()
    {
        return Physics.Raycast(transform.position, Vector3.up, distance_to_ground);
    }

    private void Jump()
    {
        if (Input.GetButtonDown(PlayerPrefs.GetString("Jump"))
            && IsGrounded())
        {
            velocity.y = (jump_takeoff_speed * time_fake);
        }
    }

    private void MovementLean(float ex_input)
    {
        rotation_thing.transform.localRotation = Quaternion.Euler(0, 0, ex_input * -(lean_distance));
    }

    
    private void CycaBlyat()
    {
        if (Input.GetButton(PlayerPrefs.GetString("Squat")) 
            && (collider.height > height_squatting))
        {
            endgoal_horizontal *= speed_multiplier_squatting;
            endgoal_vertical *= speed_multiplier_squatting;

            collider.height -= 0.1f;
            controller.height -= 0.1f;
            distance_to_ground -= 0.05f;
            transformation.localScale -= new Vector3(0, 0.05f, 0);
        }
        else if (!Input.GetButton(PlayerPrefs.GetString("Squat")) 
            && collider.height < height_standing 
            && !IsBeneathSomething())
        {
            collider.height += 0.1f;
            controller.height += 0.1f;
            distance_to_ground += 0.05f;
            transformation.localScale += new Vector3(0, 0.05f, 0);
        }
    }
}
