using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScriptKeybinds : MonoBehaviour
{
    public GameObject keybindings_menu, options_menu, data_container;

    private GameObject current_button;

    private string current_action, current_key;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (current_key != null) // set when a button is clicked
        {
            Event e = Event.current;
            if (e.isKey)
            {
                KeyEntry(e.keyCode.ToString());
            }
            else if (e.shift)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    KeyEntry(KeyCode.LeftShift.ToString());
                }
                else if (Input.GetKey(KeyCode.RightShift))
                {
                    KeyEntry(KeyCode.RightShift.ToString());
                }
            }
            else if (e.isMouse)
            {
                if (e.button == 0)
                {
                    KeyEntry(KeyCode.Mouse0.ToString());
                }
                else if (e.button == 1)
                {
                    KeyEntry(KeyCode.Mouse1.ToString());
                }
                else if (e.button == 2)
                {
                    KeyEntry(KeyCode.Mouse2.ToString());
                }
            }
            else if (e.isScrollWheel)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    KeyEntry("MouseWheelUp");
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    KeyEntry("MouseWheelDown");
                }
            }
        }
    }

    public void KeyEntry(string entry)
    {
        //current_button.transform.GetChild(0).GetComponent<Text>().text = entry;
        current_key = null;

        //SetCurrentInputaxis(entry);
        //CreateNewEntry();
        //ApplyBinds();
        //SaveBinds();
    }

    public void ChangeKey(GameObject clicked)
    {
        //current_key = clicked;
    }

    public void Open_Menu_Page(GameObject page)
    {
        page.SetActive(true);
        keybindings_menu.SetActive(false);
    }

    public void Open_Options()
    {
        Open_Menu_Page(options_menu);
    }
}
