using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    public float move_speed = 10.0f;
    public float jump_force = 7.0f;
    public float distance_to_ground = 1.1f;
    public float fall_multiplier = 2.5f;
    public float low_jump_multiplier = 2f;

    public CapsuleCollider capsule_collider;
    public LayerMask ground_layer;

    private Rigidbody rigid_body;

    public Object thingamabob;

	// Use this for initialization
	void Start ()
    {
        rigid_body = GetComponent<Rigidbody>();
        capsule_collider = GetComponent<CapsuleCollider>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //rigid_body.isKinematic = true;
        //rigid_body.detectCollisions = true;
        //Debug.Log(Physics.Raycast(transform.position, Vector3.down, distance_to_ground));


        float vertical = Input.GetAxis("Vertical") * move_speed * Time.deltaTime;
        float horizontal = Input.GetAxis("Horizontal") * move_speed * Time.deltaTime;
        //vertical *= Time.deltaTime;
        //horizontal *= Time.deltaTime;

        //Vector3 movement = new Vector3(vertical,0,horizontal);
        //rigid_body.MovePosition(transform.position+movement);

        transform.Translate(horizontal,0,vertical);

        if(rigid_body.velocity.y < 0)
        {
            rigid_body.velocity += Vector3.up * Physics.gravity.y * (fall_multiplier - 1) * Time.deltaTime;
        }
        else if (rigid_body.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rigid_body.velocity += Vector3.up * Physics.gravity.y * (low_jump_multiplier - 1) * Time.deltaTime;
        }

        // Jump code
        if(IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rigid_body.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        }
	}

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distance_to_ground);

        //return Physics.CheckCapsule(capsule_collider.bounds.center, 
        //    new Vector3(capsule_collider.bounds.center.x, capsule_collider.bounds.min.y, capsule_collider.bounds.center.z), 
        //    capsule_collider.radius*0.9f, 
        //    ground_layer);
        
        //return false;
    }
}
