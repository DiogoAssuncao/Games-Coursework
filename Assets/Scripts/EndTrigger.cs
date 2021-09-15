using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handles the end of the game on hit of the ending triggers
public class EndTrigger : MonoBehaviour {

    public GameObject gameController;
    public float waitTime = 3f;
    private float elapsedTime;
    private bool started = false;

    void Update() {
        if(started) {
            elapsedTime -= Time.deltaTime;
            if (elapsedTime <= 0) {
                gameController.GetComponent<GameController>().EndGame();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            elapsedTime = waitTime;
            started = true;
        }
    }
}
