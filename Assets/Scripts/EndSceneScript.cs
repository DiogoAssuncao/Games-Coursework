using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


// Will handle the end Scene
// According to the time of the last played match will return the 3 possibilities
// Make sure that lockEndingTime matches with the one set on the clock
// Time = 0 or less - Lose
//      = lockEndingTime or more - Win
//      = other - Fake Win
public class EndSceneScript : MonoBehaviour {

    public ScoreManager scoreManager;
    public GameObject endText, timer;
    public float lockEndingTime = 20;
    private string winFake = "You Win?";
    private string lose = "You Lose";
    private string win = "You Escaped";
    public float blinkTime;
    private float elapsedTime;
    private float timeToUnlock = 3.0f;
    private bool off = false;
    
    // Setting up of the text
    // If a session record as been set the time will blink
    void Start() {
        if(scoreManager.lastTime <= 0) {
            endText.GetComponent<TextMeshProUGUI>().text = lose;
            endText.GetComponent<TextMeshProUGUI>().color = new Color32(210, 0, 0, 255);
        } else if (scoreManager.lastTime <= lockEndingTime * 60) {
            endText.GetComponent<TextMeshProUGUI>().text = winFake; 
        } else {
            endText.GetComponent<TextMeshProUGUI>().text = win;
        }
        (int m, int s) = GetMinutesAndSeconds(scoreManager.lastTime);
        timer.GetComponent<TextMeshProUGUI>().text = m.ToString("00") + ":" + s.ToString("00");
        if(scoreManager.lastTime == scoreManager.bestTime){
            blinkTime = 0.75f;
        } else {
            blinkTime = 0f;
        }
    }

    // Blinking updates
    // After the set time any input will return to the menu
    void Update() {
        if(blinkTime > 0){
            elapsedTime -= Time.deltaTime;
            if (elapsedTime < 0) {
                if (!off) {
                    timer.SetActive(false) ;
                    elapsedTime = 0.3f;
                    off = true;
                } else {
                    timer.SetActive(true);
                    elapsedTime = blinkTime;
                    off = false;
                }
            }
        }
        timeToUnlock -= Time.deltaTime;
        if(Input.anyKey && timeToUnlock < 0){ 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0); 
        }
    }

    (int, int) GetMinutesAndSeconds (float timeInSeconds) {
        int minutes = (int) (timeInSeconds / 60);
        int seconds = (int) (timeInSeconds % 60);
        return (minutes, seconds);
    }
}
