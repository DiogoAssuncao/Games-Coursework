using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles a door behaviour
// Anything that will move from one position to another 
// Children of these objects must be correctly ordered
// 0 - Moving Structure, 1 - closed Position, 2 - open position
public class Door : MonoBehaviour
{
    Transform structure, closedPosition, openPosition, target;
    public GameObject sound;
    public float speed = 1.0f;

    // Sets up children
    void Start() {
        structure = transform.GetChild(0);
        closedPosition = transform.GetChild(1);
        openPosition = transform.GetChild(2);
        target = closedPosition;
    }

    // Checks for changes in target positon
    void FixedUpdate() {
        if(structure.position != target.position) {
            Vector3 velocity = GetVector(structure.position, target.position);
            structure.Translate(velocity * speed * Time.deltaTime, Space.World);
        } else {
            sound.GetComponent<AudioSource>().Pause();
        }
    }

    // Changes target positon to openPosition
    // Plays a sound
    public void Open() { 
        target = openPosition;
        sound.GetComponent<AudioSource>().Play(0);
    }

    // Changes target positon to closedPosition
    // Plays a sound
    public void Close() {
        target = closedPosition;
        sound.GetComponent<AudioSource>().Play(0);
    }

    private Vector3 GetVector(Vector3 from, Vector3 to){
        float x = to.x - from.x;
        float y = to.y - from.y;
        float z = to.z - from.z;
        return (new Vector3(x, y, z));
    }
}
