using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class ItemLoader : MonoBehaviour
{
    public GameObject data_container, item_spawner;
    private string[] files;
    private int index;
    private bool has_been_created;
    // Start is called before the first frame update
    void Start()
    {
        if (Serialization.DirectoryExists(Application.persistentDataPath + "/saves/savedgames/"
            + data_container.GetComponent<DataContainer>().saved_game_slot
            + "/" + SceneManager.GetActiveScene().name))
        {
            files = Directory.GetFiles(Application.persistentDataPath + "/saves/savedgames/"
            + data_container.GetComponent<DataContainer>().saved_game_slot
            + "/" + SceneManager.GetActiveScene().name
            + "/items");

            index = 0;

            has_been_created = true;
        }
        else
        {
            has_been_created = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (has_been_created = true
            && index < files.Length)
        {
            item_spawner.GetComponent<ItemSpawner>().item_stack.Push(Serialization.Load<SavedObject>(
                Application.persistentDataPath + "/saves/savedgames/"
            + data_container.GetComponent<DataContainer>().saved_game_slot
            + "/" + SceneManager.GetActiveScene().name
            + "/items/" + files[index]));
        }
    }
}
