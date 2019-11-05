using UnityEngine;

/*
 * Player_Movement_Controller
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         These scripts handle player movement and associated controls.
 * 
 * Notes:
 * The the system by which keybinds are saved involves saving key names to an array, 
 * in which the index numbers represent different actions.
 * These indexes and their reflective actions are as follows:
 * 0                action_general
 * 1                toolbelt_one
 * 2                toolbelt_two
 * 3                toolbelt_three
 * 4                toolbelt_four
 * 5                toolbelt_five
 * 6                toolbelt_six
 * 7                toolbelt_seven
 * 8                toolbelt_eight
 * 9                toolbelt_nine
 * 10               toolbelt_ten
 * 11               attack_primary
 * 12               attack_secondary
 * 13               attack_tertiary
 * 14               kick
 * 15               block
 * 16               move_forward
 * 17               move_backward
 * 18               move_left
 * 19               move_right
 * 20               jump
 * 21               squat
 * 22               speed_toggle
 * 23               toolbelt_scroll_up
 * 24               toolbelt_scroll_down
 * 25               quicksave
 * 26               quickload
 * 27               pause
 * 28               log
 * 29               objectives
 * 30               stats
 * 31               gamma_up
 * 32               gamma_down
*/
public class Player_Movement_Controller : MonoBehaviour
{

    private Transform transformation;
    private CharacterController controller;
    private CapsuleCollider collider;

    private Vector3 velocity;

    private bool is_squatting, current_grounded, previous_grounded;

    private float angular_speed, speed_multiplier_squatting;

    public float speed, jump_takeoff_speed, height_standing, height_squatting, lean_distance, distance_to_ground, gravity_fake;

    public GameObject rotation_thing, character, data_containment;

    private string move_forward, move_backward, move_left, move_right, jump, squat;
    
    

    // Use this for initialization
    void Start ()
    {

        transformation = GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        collider = GetComponent<CapsuleCollider>();
        
        velocity = Vector3.zero;
        
        angular_speed = Mathf.Sqrt((speed * speed / 2.0f));
        speed_multiplier_squatting = 0.6f;

        is_squatting = false;
        current_grounded = true;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        move_forward = data_containment.GetComponent<DataContainment>().binds.keybind_log[16];
        move_backward = data_containment.GetComponent<DataContainment>().binds.keybind_log[17];
        move_left = data_containment.GetComponent<DataContainment>().binds.keybind_log[18];
        move_right = data_containment.GetComponent<DataContainment>().binds.keybind_log[19];
        jump = data_containment.GetComponent<DataContainment>().binds.keybind_log[20];
        squat = data_containment.GetComponent<DataContainment>().binds.keybind_log[21];

        float input_X = Input.GetAxis(move_right) - Input.GetAxis(move_left);
        float input_Z = Input.GetAxis(move_forward) - Input.GetAxis(move_backward);

        if (Input.GetButton(move_left) && Input.GetButton(move_forward) ||
            Input.GetButton(move_left) && Input.GetButton(move_backward) ||
            Input.GetButton(move_right) && Input.GetButton(move_forward) ||
            Input.GetButton(move_right) && Input.GetButton(move_backward))
        {
            velocity.x = input_X * angular_speed * Time.deltaTime;
            velocity.z = input_Z * angular_speed * Time.deltaTime;
        }
        else
        {
            velocity.x = input_X * speed * Time.deltaTime;
            velocity.z = input_Z * speed * Time.deltaTime;
        }

        velocity = transformation.rotation * velocity;

        // Squat input (Cyca Blyat)
        if (Input.GetButton(squat))
        {
            is_squatting = true;
        }
        else
        {
            is_squatting = false;
        }

        // Squatting
        SquattingConditional();

        // Gravity application
        ApplyGravity();

        // Better jumping and falling
        if (velocity.y < -0.00327654)
        {
            velocity.y += gravity_fake * Time.deltaTime;
        }
        else if (velocity.y > -0.00327654 && !(Input.GetButton(jump)))
        {
            velocity.y += 0.5f * gravity_fake * Time.deltaTime;
        }

        if (IsGrounded())
        {
            if (is_squatting)
            {
                velocity.x *= speed_multiplier_squatting;
                velocity.z *= speed_multiplier_squatting;
            }

            if (Input.GetButtonDown(jump))
            {
                Jump();
            }
        }

        MovementLean(input_X);

        // Move the character (finally)
        controller.Move(velocity);

        previous_grounded = current_grounded;
        current_grounded = IsGrounded();
    }

    private bool IsBeneathSomething()
    {
        return Physics.Raycast(transform.position, Vector3.up, distance_to_ground);
    }

    private void SquattingConditional()
    {
        if (is_squatting && (collider.height > height_squatting))
        {
            collider.height -= 0.1f;
            controller.height -= 0.1f;
            distance_to_ground -= 0.05f;
            transformation.localScale -= new Vector3(0,0.05f,0);
        }
        else if (!is_squatting && collider.height < height_standing && !IsBeneathSomething())
        {
            collider.height += 0.1f;
            controller.height += 0.1f;
            distance_to_ground += 0.05f;
            transformation.localScale += new Vector3(0, 0.05f, 0);
        }
    }

    // Artificial gravity is applied to the character
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

    private void Jump()
    {
        velocity.y = (jump_takeoff_speed * Time.deltaTime);
    }

    private void MovementLean(float ex_input)
    {
        rotation_thing.transform.localRotation = Quaternion.Euler(0,0, ex_input * -(lean_distance));
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
            if(current_grounded && previous_grounded)
            {
                controller.Move(new Vector3(0, -hit.distance, 0));
                return true;
            }
            
        }
        return false;
    }
}
