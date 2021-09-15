using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Will handle
//      Respawns
//      Resets
//      Ending Lock
//      Return to menu
//      End the game
//      Setting up the scores
public class GameController : MonoBehaviour {

    public List<GameObject> resetableObjects = new List<GameObject>();
    public GameObject clock, endingBridge;
    private List<Vector3> startPositions = new List<Vector3>();
    public ScoreManager scoreManager;

    // Gets the start position of the set resetable objects
    void Start() {
        foreach (GameObject child in resetableObjects) {
            startPositions.Add(child.transform.position); 
        }
    }

    // will reset all resetable objects to their original position
    // Called when player hits a respawn zone
    public void Respawn() {
        int count = 0;
        foreach (GameObject child in resetableObjects) {
            child.transform.position = startPositions[count];
            count++;
        }
    }

    // Resets a single resetable object to its original position
    // Used by the cube wall
    public void resetCube(GameObject cube) {
        int count = 0;
        foreach (GameObject child in resetableObjects) {
            if (cube == child) {
                cube.transform.position = startPositions[count];
                break;
            }
            count++;
        }
    }

    // Finshes the game and sets the scores
    public void EndGame() {
        float time = clock.GetComponent<ClockScript>().secondsLeft;
        scoreManager.setTime(time);
        SceneManager.LoadScene(2); 
    }

    // loads back to the menu
    // used on exit from escape menu
    public void BackToMenu() {
        SceneManager.LoadScene(0); 
    }

    // Locks out the ending
    // Makes the floor fake into hidden chamber
    public void LockEnding() {
        endingBridge.GetComponent<BoxCollider>().enabled = false;
    }
}
