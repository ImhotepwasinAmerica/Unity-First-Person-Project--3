﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectBehaviorDefault : MonoBehaviour
{
    public GameObject object_in_question, data_container;
    public SavedObject object_data;
    public bool has_been_interacted;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.DeleteAllTheThings += Destroy;
        GameEvents.current.SaveAllTheThings += SaveItem;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.gameObject != null
            && data_container != null
            && object_data != null)
        {
            RecordRotation();
            RecordPosition();
        }

        MoveAugment();
    }

    public void SetObjectData(SavedObject new_object_data)
    {
        object_data = new_object_data;
    }

    public SavedObject GetObjectData()
    {
        return object_data;
    }

    public void RecordRotation()
    {
        //object_data.thing_rotation[0] = this.gameObject.transform.rotation.x;
        //object_data.thing_rotation[1] = this.gameObject.transform.rotation.y;
        //object_data.thing_rotation[2] = this.gameObject.transform.rotation.z;
        object_data.thing_rotation = this.gameObject.transform.rotation;
    }

    public void RecordPosition()
    {
        //object_data.thing_position[0] = this.gameObject.transform.position.x;
        //object_data.thing_position[1] = this.gameObject.transform.position.y;
        //object_data.thing_position[2] = this.gameObject.transform.position.z;
        object_data.thing_position = this.gameObject.transform.position;
    }

    public void SpawnProceedings()
    {
        object_data = new SavedObject();
    }

    public void HealthChange(int hit_strength)
    {
        object_data.health += hit_strength;
    }

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
    }

    public void SaveItem()
    {
        Serialization.Save<SavedObject>(object_data,
            Application.persistentDataPath + "/saves/savedgames/"
            + data_container.GetComponent<DataContainer>().saved_game_slot
            + "/" + SceneManager.GetActiveScene().name
            + "/items/" + this.gameObject.GetInstanceID() + ".dat"); // The instance ID serves as the name of the object data file in memory
    }

    public void DestroyOrChange()
    {
        if (Serialization.SaveExists(
            Application.persistentDataPath + "/saves/savedgames/"
            + data_container.GetComponent<DataContainer>().saved_game_slot
            + "/" + SceneManager.GetActiveScene().name
            + "/presentitems/" + this.gameObject.GetInstanceID() + ".dat"))
        {
            object_data = Serialization.Load<SavedObject>(Application.persistentDataPath + "/saves/savedgames/"
            + data_container.GetComponent<DataContainer>().saved_game_slot
            + "/" + SceneManager.GetActiveScene().name
            + "/presentitems/" + this.gameObject.GetInstanceID() + ".dat");

            this.gameObject.transform.rotation = object_data.thing_rotation;
            this.gameObject.transform.position = object_data.thing_position;
        }
        else
        {
            this.Destroy();
        }
    }

    public virtual void UseDefault(GameObject thing) { }

    public virtual void UseDefault() { }

    public virtual void MoveAugment() { }

    public virtual void MakeVirtual() { }
}