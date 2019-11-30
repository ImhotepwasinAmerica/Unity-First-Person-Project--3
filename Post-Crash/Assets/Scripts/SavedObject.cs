using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class SavedObject
{
    public string saved_thing;

    public float[] thing_rotation;
    public float[] thing_position;

    public int health;
    public int max_health;

    public bool invincible;

    public bool switch_on;

    public bool weapon_ammo_count;
}
