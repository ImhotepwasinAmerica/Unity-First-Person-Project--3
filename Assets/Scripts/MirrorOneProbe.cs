using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorOneProbe : MonoBehaviour
{
    public enum Directions {X,Y,Z};
    public Directions orientation;
    public GameObject mirror, character;
    public float render_distance;

    private float offset;
    private Vector3 probePos;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Vector3.Distance(mirror.transform.position, character.transform.position) < render_distance)
        {
            if (orientation == Directions.X)
            {
                offset = mirror.transform.position.x - character.transform.position.x;

                probePos.x = mirror.transform.position.x + offset;
                probePos.y = character.transform.position.y;
                probePos.z = character.transform.position.z;
            }
            else if (orientation == Directions.Y)
            {
                offset = mirror.transform.position.y - character.transform.position.y;

                probePos.x = character.transform.position.x;
                probePos.y = mirror.transform.position.y + offset;
                probePos.z = character.transform.position.z;
            }
            else if (orientation == Directions.Z)
            {
                offset = mirror.transform.position.z - character.transform.position.z;

                probePos.x = character.transform.position.x;
                probePos.y = character.transform.position.y;
                probePos.z = mirror.transform.position.z + offset;
            }
        }
        else
        {
            probePos = mirror.transform.position;
        }
        

        transform.position = probePos;

        //Debug.Log(Vector3.Distance(mirror.transform.position,character.transform.position));
    }
}
