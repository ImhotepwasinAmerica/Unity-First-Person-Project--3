using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviorDefault : MonoBehaviour
{
    public GameObject object_in_question, data_container;
    public SavedObject object_data;
    public bool has_been_interacted;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void AddToRepository()
    {
        //data_container.GetComponent<DataContainer>().saved_objects.Add(object_data);
    }

    public void DeleteFromRepository()
    {
        //data_container.GetComponent<DataContainer>().saved_objects.Remove(object_data);
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
        AddToRepository();
    }

    public void HealthChange(int hit_strength)
    {
        object_data.health += hit_strength;
    }

    public virtual void UseDefault(GameObject new_anchor) { }

    public virtual void UseDefault() { }

    public virtual void MoveAugment() { }

    public virtual void MakeVirtual() { }
}
