using System.Collections;
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
        if (object_in_question != null
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
        object_data.thing_rotation[0] = object_in_question.transform.rotation.x;
        object_data.thing_rotation[1] = object_in_question.transform.rotation.y;
        object_data.thing_rotation[2] = object_in_question.transform.rotation.z;
    }

    public void RecordPosition()
    {
        object_data.thing_position[0] = object_in_question.transform.position.x;
        object_data.thing_position[1] = object_in_question.transform.position.y;
        object_data.thing_position[2] = object_in_question.transform.position.z;
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
        GameObject.Destroy(object_in_question);
    }

    public void SaveItem()
    {
        Serialization.Save<SavedObject>(object_data,
            Application.persistentDataPath + "/saves/savedgames/"
            + data_container.GetComponent<DataContainer>().saved_game_slot
            + "/" + SceneManager.GetActiveScene().name
            + "/items/" + this.gameObject.GetInstanceID() + ".dat"); // The instance ID serves as the name of the object data file in memory
    }

    public virtual void UseDefault(GameObject thing) { }

    public virtual void UseDefault() { }

    public virtual void MoveAugment() { }

    public virtual void MakeVirtual() { }
}
