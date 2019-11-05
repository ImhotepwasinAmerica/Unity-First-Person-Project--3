using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * SavedObjectScript
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         Instances of this class represent objects in a world.
 * 
 * Notes:
 * 
*/
[System.Serializable]
public class SavedObjectScript
{
    public string saved_thing;

    public float[] thing_rotation;
    public float[] thing_position;

    public int health;
    public int max_health;

    public bool invincible;

    public SavedObjectScript()
    {
        
    }
}
