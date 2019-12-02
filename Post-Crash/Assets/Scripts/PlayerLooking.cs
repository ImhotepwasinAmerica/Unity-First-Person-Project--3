using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLooking : MonoBehaviour
{
    public float sensitivity, smoothing, reach;

    public GameObject held_object_anchor, character;
    public DataContainer data_containment;

    private RaycastHit hit;
    private GameObject usage_target, held_thing;
    private Quaternion held_thing_rotation;
    private Vector2 mouse_look, smooth_v, md;
    private string general_action, attack_primary, attack_secondary, attack_tertiary;
    public float why;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        usage_target = null;
        held_thing = null;
        why += character.transform.eulerAngles.y; // This value is further edited in the game loading process
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton(PlayerPrefs.GetString("Secondary Attack")))
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

        // The 'General Action' button will both grab hold of and release a physics object 
        if (Input.GetButtonDown(PlayerPrefs.GetString("General Action")))
        {
            try
            {
                usage_target = ReturnUsableObject();
                usage_target.GetComponent<ObjectBehaviorDefault>().UseDefault(held_object_anchor);
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
