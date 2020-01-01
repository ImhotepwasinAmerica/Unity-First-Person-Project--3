using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject data_container, character;
    private Vector3 character_position;
    private Quaternion character_rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        if (data_container.GetComponent<DataContainer>().character.rotation_x != null) // It will be necessary to alter the position and rotation of the character when entering a new scene
        {
            character.transform.position = new Vector3(
                data_container.GetComponent<DataContainer>().character.position_x,
                data_container.GetComponent<DataContainer>().character.position_y,
                data_container.GetComponent<DataContainer>().character.position_z);

            character.transform.rotation = Quaternion.Euler(
                data_container.GetComponent<DataContainer>().character.rotation_x,
                data_container.GetComponent<DataContainer>().character.rotation_y,
                data_container.GetComponent<DataContainer>().character.rotation_z);//data_container.GetComponent<DataContainer>().character.thing_rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        character_position = character.transform.position;
        character_rotation = character.transform.rotation;

        data_container.GetComponent<DataContainer>().character.position_x = character_position.x;
        data_container.GetComponent<DataContainer>().character.position_y = character_position.y;
        data_container.GetComponent<DataContainer>().character.position_z = character_position.z;

        data_container.GetComponent<DataContainer>().character.rotation_x = character_rotation.x;
        data_container.GetComponent<DataContainer>().character.rotation_y = character_rotation.y;
        data_container.GetComponent<DataContainer>().character.rotation_z = character_rotation.z;
    }
}
