using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Makes sure no cube can be removed from Chamber 1 into the main chamber
// If the cube hits the wall it will return to its spawn position
// Stops the player to use these to help in Chamber 3
public class CubeWall : MonoBehaviour {
    public GameObject gameController;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "ButtonCube") {
            gameController.GetComponent<GameController>().resetCube(other.gameObject);
        }
    }
}
