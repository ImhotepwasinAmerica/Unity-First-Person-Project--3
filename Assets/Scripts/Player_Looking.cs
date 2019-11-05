using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player_Looking
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         These scripts handle player camera control and associated character controls.
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
 * 
 * 
*/
public class Player_Looking : MonoBehaviour
{
    public float sensitivity, smoothing, reach;

    public GameObject held_object_anchor, character;
    public DataContainment data_containment;

    private RaycastHit hit;
    private GameObject usage_target, held_thing;
    private Quaternion held_thing_rotation;
    private Vector2 mouse_look, smooth_v, md;
    private string general_action, attack_primary, attack_secondary, attack_tertiary;
    public float why;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        usage_target = null;
        held_thing = null;
        why += character.transform.eulerAngles.y; // This value is further edited in the game loading process
    }

    private void FixedUpdate()
    {
        general_action = data_containment.GetComponent<DataContainment>().binds.keybind_log[0];

        if (Input.GetMouseButton(1))
        {
            held_object_anchor.transform.Rotate(Input.GetAxisRaw("Mouse X") * Vector3.right);
            held_object_anchor.transform.Rotate(Input.GetAxisRaw("Mouse Y") * Vector3.down);
        }
        else
        {
            md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        }

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smooth_v.x = Mathf.Lerp(smooth_v.x, md.x, 1f / smoothing);
        smooth_v.y = Mathf.Lerp(smooth_v.y, md.y, 1f / smoothing);
        mouse_look += smooth_v;

        mouse_look.y = Mathf.Clamp(mouse_look.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-mouse_look.y, Vector3.right);
        character.transform.localRotation = Quaternion.Euler(0, why + mouse_look.x, 0);

        // The 'E' button will both grab hold of and release a physics object 
        if (Input.GetButtonDown(general_action))
        {
            try
            {
                usage_target = ReturnUsableObject();
                usage_target.GetComponent<ObjectDefaultBehavior>().UseDefault(held_object_anchor);
            }
            catch (System.NullReferenceException e)
            {
                usage_target = null;
                Debug.Log("No object found");
            }
        }
    }

    private GameObject ReturnUsableObject()
    {
        GameObject thing = null;

        Physics.Raycast(transform.position, transform.forward, out hit, reach);

        if (hit.collider.gameObject != null)
        {
            thing = hit.collider.gameObject;
        }

        return thing;
    }
}
