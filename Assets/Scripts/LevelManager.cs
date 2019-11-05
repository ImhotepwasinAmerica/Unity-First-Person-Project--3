using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * LevelManager
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         This class handles what operations are common to all scenes, such as placing loaded ojects. 
 * 
 * Notes:
 * 
*/
public class LevelManager : MonoBehaviour
{
    public GameObject data_container, player, box_test;
    public string scene_name;
    public Camera cam;

    private Dictionary<string, GameObject> item_index = new Dictionary<string, GameObject>();
    private SavedCharacterScript guy;

    private void Awake()
    {
       
    }

    // Use this for initialization
    void Start ()
    {
        item_index = new Dictionary<string, GameObject>();
        // The item index is created.
        // There has got to be a more efficient way of doing this.
        item_index.Add("box_test", box_test);

        Time.timeScale = 1f;

        string slot = data_container.GetComponent<DataContainment>().saved_game_slot;
        string scene = data_container.GetComponent<DataContainment>().saved_game_scene;
        guy = data_container.GetComponent<DataContainment>().character;
        GameObject thang;

        // If the scene has been previously saved in a saved game,
        // it will be changed to its saved condition.
        // Otherwise, it will remain unchanged.
        if (DirectoryExists(Application.persistentDataPath + "/saves/savedgames/" + slot + "/" + scene))
        {
            // All objects in a used scene are removed,
            // to be replaced with objects from the 'saved object list'.
            GameObject[] alla_deeze = GameObject.FindGameObjectsWithTag("Item");
            for (int i = 0; i < alla_deeze.Length; i++)
            {
                Object.Destroy(alla_deeze[i]);
            }

            // The player character is moved from his default position to his saved position,
            // and from his default rotation to his saved rotation
            //SetPlayerPosition();
            SetPlayerPosition();
            SetPlayerRotation();

            // Each object in the data container's object list is spawned.
            // The object list should be set as that of the current scene BEFORE the scene is loaded.
            foreach (SavedObjectScript saved_object in data_container.GetComponent<DataContainment>().saved_objects)
            {
                thang = GameObject.Instantiate(item_index[saved_object.saved_thing],
                    new Vector3(saved_object.thing_position[0], saved_object.thing_position[1], saved_object.thing_position[2]),
                    Quaternion.Euler(saved_object.thing_rotation[0], saved_object.thing_rotation[1], saved_object.thing_rotation[2]));

                thang.GetComponent<ObjectDefaultBehavior>().SetObjectData(saved_object);
            }
        }
        else
        {
            guy = new SavedCharacterScript();
            data_container.GetComponent<DataContainment>().character.guy_position = new float[] { player.transform.position.x, player.transform.position.x, player.transform.position.z };
            data_container.GetComponent<DataContainment>().character.guy_rotation = new float[] { player.transform.localRotation.x, player.transform.localRotation.y, player.transform.localRotation.z };

            guy.health = 100;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        data_container.GetComponent<DataContainment>().character.guy_position[0] = player.transform.position.x;
        data_container.GetComponent<DataContainment>().character.guy_position[1] = player.transform.position.y;
        data_container.GetComponent<DataContainment>().character.guy_position[2] = player.transform.position.z;

        data_container.GetComponent<DataContainment>().character.guy_rotation[0] = player.transform.eulerAngles.x;
        data_container.GetComponent<DataContainment>().character.guy_rotation[1] = player.transform.eulerAngles.y;
        data_container.GetComponent<DataContainment>().character.guy_rotation[2] = player.transform.eulerAngles.z;

        data_container.GetComponent<DataContainment>().character.height = player.GetComponent<CharacterController>().height;
    }

    public void SetPlayerRotation()
    {
        cam.GetComponent<Player_Looking>().why = guy.guy_rotation[1];
    }

    public void SetPlayerPosition()
    {
        Vector3 position = new Vector3();

        position.x = guy.guy_position[0];
        position.y = guy.guy_position[1];
        position.z = guy.guy_position[2];
        player.transform.position = position;
    }

    public void SetPlayerHeight()
    {
        player.GetComponent<CharacterController>().height = data_container.GetComponent<DataContainment>().character.height;
        player.GetComponent<CapsuleCollider>().height = data_container.GetComponent<DataContainment>().character.height;
        player.GetComponent<Player_Movement_Controller>().distance_to_ground = (data_container.GetComponent<DataContainment>().character.height / 2);
        player.transform.localScale = new Vector3(1.0f, (data_container.GetComponent<DataContainment>().character.height / 2), 1.0f);
    }

    public bool SaveExists(string file_location)
    {
        return SaveLoad.SaveExists(file_location);
    }

    public bool DirectoryExists(string directory_location)
    {
        return SaveLoad.DirectoryExists(directory_location);
    }
}
