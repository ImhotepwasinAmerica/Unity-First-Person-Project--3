using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SavedCharacterScript
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         This class holds all data in a saved game pertaining to the player character. 
 * 
 * Notes:
 * 
*/
[System.Serializable]
public class SavedCharacterScript
{
    public int health;

    public int max_health;
    
    public float height;
    //public string[] tools; // word IDs for game objects will be necessary, preferably in dictionary form
    
    public float[] guy_rotation;
    
    public float[] guy_position;   
}
