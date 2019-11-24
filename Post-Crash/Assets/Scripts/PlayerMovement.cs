using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject rotation_thing, data_container;
    public float speed, jump_takeoff_speed, height_standing, height_squatting, lean_distance, distance_to_ground, gravity_fake, speed_multiplier_squatting;

    private Vector3 velocity;
    private float angular_speed, endgoal_horizontal, endgoal_vertical;
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
        gravity_fake = Physics.gravity.y;
    }

    // Update is called once per frame
    void Update()
    {
        Walk();

        BetterMovement();

        ApplyGravity();

        Jump();

        CycaBlyat();

        MovementLean(velocity.y / (speed * Time.deltaTime));

        previous_grounded = current_grounded;
        current_grounded = IsGrounded();
    }

    void FixedUpdate()
    {
        controller.Move(velocity);
    }

    private void Walk()
    {
        if (Input.GetButton(PlayerPrefs.GetString("Move Forward")))
        {
            if (Input.GetButton(PlayerPrefs.GetString("Move Right")))
            {
                endgoal_horizontal = 1 * angular_speed * Time.deltaTime;
                endgoal_vertical = 1 * angular_speed * Time.deltaTime;
            }
            else if (Input.GetButton(PlayerPrefs.GetString("Move left")))
            {
                endgoal_horizontal = -1 * angular_speed * Time.deltaTime;
                endgoal_vertical = 1 * angular_speed * Time.deltaTime;
            }
            else
            {
                endgoal_vertical = 1 * speed * Time.deltaTime;
            }
        }
        else if (Input.GetButton(PlayerPrefs.GetString("Move Backward")))
        {
            if (Input.GetButton(PlayerPrefs.GetString("Move Right")))
            {
                endgoal_horizontal = 1 * angular_speed * Time.deltaTime;
                endgoal_vertical = -1 * angular_speed * Time.deltaTime;
            }
            else if (Input.GetButton(PlayerPrefs.GetString("Move left")))
            {
                endgoal_horizontal = -1 * angular_speed * Time.deltaTime;
                endgoal_vertical = -1 * angular_speed * Time.deltaTime;
            }
            else
            {
                endgoal_vertical = -1 * speed * Time.deltaTime;
            }
        }
        else if (Input.GetButton(PlayerPrefs.GetString("Move left")))
        {
            endgoal_horizontal = -1 * speed * Time.deltaTime;
        }
        else if (Input.GetButton(PlayerPrefs.GetString("Move Right")))
        {
            endgoal_horizontal = 1 * speed * Time.deltaTime;
        }
        else if (!Input.GetButton(PlayerPrefs.GetString("Move left"))
            && !Input.GetButton(PlayerPrefs.GetString("Move Right")))
        {
            endgoal_horizontal = 0;
        }
        else if (Input.GetButton(PlayerPrefs.GetString("Move Forward"))
            && Input.GetButton(PlayerPrefs.GetString("Move Backward")))
        {
            endgoal_vertical = 0;
        }
    }

    private void BetterMovement()
    {
        // Better jumping and falling
        if (velocity.y < -0.00327654)
        {
            velocity.y += gravity_fake * Time.deltaTime;
        }
        else if (velocity.y > -0.00327654 && !(Input.GetButton(PlayerPrefs.GetString("Jump"))))
        {
            velocity.y += 0.5f * gravity_fake * Time.deltaTime;
        }
    }

    private void ApplyGravity()
    {
        if (IsGrounded())
        {
            velocity.y = (gravity_fake * Time.deltaTime);
        }
        else
        {
            velocity.y += (gravity_fake * Time.deltaTime);
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
        if (Input.GetButton(PlayerPrefs.GetString("Jump")))
        {
            velocity.y = (jump_takeoff_speed * Time.deltaTime);
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
