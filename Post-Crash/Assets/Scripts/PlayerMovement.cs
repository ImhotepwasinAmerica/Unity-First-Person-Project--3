using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject rotation_thing, data_container;
    public float speed, jump_takeoff_speed, height_standing, height_squatting, lean_distance, distance_to_ground, speed_multiplier_squatting;

    private Vector3 velocity, velocity_endgoal;
    private float angular_speed, gravity_fake, time_fake;
    private bool is_squatting, current_grounded, previous_grounded;
    private Quaternion lean;

    private Transform transformation;
    private CharacterController controller;
    private CapsuleCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        transformation = GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        collider = GetComponent<CapsuleCollider>();

        current_grounded = true;
        previous_grounded = true;
        
        angular_speed = Mathf.Sqrt((speed * speed / 2.0f));

        time_fake = 0.01666f;
        gravity_fake = Physics.gravity.y * time_fake;

        lean = Quaternion.Euler(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        Walk();

        BetterMovement();
        ApplyGravity();
        Jump();

        CycaBlyat();

        MovementLean();

        velocity_endgoal = transformation.rotation * velocity_endgoal;

        velocity.x = Mathf.Lerp(velocity.x, velocity_endgoal.x, 0.15f);
        velocity.z = Mathf.Lerp(velocity.z, velocity_endgoal.z, 0.15f);
        velocity.y = velocity_endgoal.y;

        previous_grounded = current_grounded;
        current_grounded = IsGrounded();
    }

    void FixedUpdate()
    {
        controller.Move(velocity);
    }

    private void Walk()
    {
        if (Input.GetButton(PlayerPrefs.GetString("Move Forward"))
            && !Input.GetButton(PlayerPrefs.GetString("Move Backward")))
        {
            velocity_endgoal.z = time_fake;
        }
        else if (!Input.GetButton(PlayerPrefs.GetString("Move Forward"))
            && Input.GetButton(PlayerPrefs.GetString("Move Backward")))
        {
            velocity_endgoal.z = -time_fake;
        }
        else
        {
            velocity_endgoal.z = 0;
        }

        if (Input.GetButton(PlayerPrefs.GetString("Move Right"))
            && !Input.GetButton(PlayerPrefs.GetString("Move Left")))
        {
            velocity_endgoal.x = time_fake;
        }
        else if (!Input.GetButton(PlayerPrefs.GetString("Move Right"))
            && Input.GetButton(PlayerPrefs.GetString("Move Left")))
        {
            velocity_endgoal.x = -time_fake;
        }
        else
        {
            velocity_endgoal.x = 0;
        }

        if ((Input.GetButton(PlayerPrefs.GetString("Move Forward")) && Input.GetButton(PlayerPrefs.GetString("Move Left")))
            || (Input.GetButton(PlayerPrefs.GetString("Move Forward")) && Input.GetButton(PlayerPrefs.GetString("Move Right")))
            || (Input.GetButton(PlayerPrefs.GetString("Move Backward")) && Input.GetButton(PlayerPrefs.GetString("Move Left")))
            || (Input.GetButton(PlayerPrefs.GetString("Move Backward")) && Input.GetButton(PlayerPrefs.GetString("Move Right"))))
        {
            velocity_endgoal.z *= angular_speed;
            velocity_endgoal.x *= angular_speed;
        }
        else
        {
            velocity_endgoal.z *= speed;
            velocity_endgoal.x *= speed;
        }
    }

    private void BetterMovement()
    {
        // Better jumping and falling
        if (velocity_endgoal.y < -0.00327654)
        {
            velocity_endgoal.y += gravity_fake * time_fake;
        }
        else if (velocity_endgoal.y > -0.00327654 && !(Input.GetButton(PlayerPrefs.GetString("Jump"))))
        {
            velocity_endgoal.y += (0.5f * gravity_fake * time_fake);
        }
    }

    private void ApplyGravity()
    {
        if (IsGrounded())
        {
            velocity_endgoal.y = (gravity_fake * time_fake);
        }
        else
        {
            velocity_endgoal.y += (gravity_fake * time_fake);
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
            velocity_endgoal.y = (jump_takeoff_speed * time_fake);
        }
    }

    private void MovementLean()
    {
        lean.z = Mathf.Lerp(lean.z, -velocity_endgoal.x/5, 0.09f);
        rotation_thing.transform.localRotation = lean;
    }

    
    private void CycaBlyat()
    {
        if (Input.GetButton(PlayerPrefs.GetString("Squat")) 
            && (collider.height > height_squatting))
        {
            velocity_endgoal.x *= speed_multiplier_squatting;
            velocity_endgoal.z *= speed_multiplier_squatting;

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
