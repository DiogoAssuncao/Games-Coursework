using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//On trigger will respawn player and key moveable items
public class RespawnZone : MonoBehaviour {

    public GameObject GameController;
    public float waitTime = 1.0f; 
    private float elapsedTime;

    void Start() {
        elapsedTime = waitTime;
    }

    private void onTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            elapsedTime = waitTime;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            elapsedTime -= Time.deltaTime;
            if(elapsedTime <= 0) {
                GameController.GetComponent<GameController>().Respawn();
            }
        }
    }
}
