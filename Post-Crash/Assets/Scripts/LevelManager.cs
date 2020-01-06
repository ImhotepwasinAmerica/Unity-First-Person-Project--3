using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject data_container, character;
    private Vector3 character_position;
    private Quaternion character_rotation;
    private Character guy;
    
    // Start is called before the first frame update
    void Start()
    {
        guy = data_container.GetComponent<DataContainer>().character;

        if (data_container.GetComponent<DataContainer>().character.rotation_x != null) // It will be necessary to alter the position and rotation of the character when entering a new scene
        {
            data_container.GetComponent<DataContainer>().saved_game_scene = SceneManager.GetActiveScene().name;
            data_container.GetComponent<DataContainer>().game.current_scene_name = SceneManager.GetActiveScene().name;

            character.transform.position = new Vector3(
                guy.position_x,
                guy.position_y,
                guy.position_z);

            character.transform.rotation = Quaternion.Euler(
                guy.rotation_x,
                guy.rotation_y,
                guy.rotation_z);
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

        guy.position_x = character_position.x;
        guy.position_y = character_position.y;
        guy.position_z = character_position.z;

        guy.rotation_x = character_rotation.x;
        guy.rotation_y = character_rotation.y;
        guy.rotation_z = character_rotation.z;
    }
}
