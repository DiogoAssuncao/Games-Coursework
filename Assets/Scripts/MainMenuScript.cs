using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Handles the Main/Start Menu
public class MainMenuScript : MonoBehaviour {
    public ScoreManager scoreManager;
    public GameObject timer;
    public float blinktTime = 0.05f;
    private float elapsedTime;
    private bool off = false;

    void Start() {
        elapsedTime = blinktTime;
        (int m, int s) = GetMinutesAndSeconds(scoreManager.bestTime);
        if (scoreManager.bestTime == 0) {
            m = 30;
            s = 0;
        } 
        timer.GetComponent<TextMeshProUGUI>().text = m.ToString("00") + ":" + s.ToString("00");
    }

    void Update() {
        elapsedTime -= Time.deltaTime;
        if (elapsedTime < 0) {
            if (!off) {
                timer.SetActive(false) ;
                elapsedTime = 0.3f;
                off = true;
            } else {
                timer.SetActive(true);
                elapsedTime = blinktTime;
                off = false;
            }
        }
    }

    public void LoadGame() {
        SceneManager.LoadScene(1); 
    }

    public void ExitGame() {
        Application.Quit();
    }

    (int, int) GetMinutesAndSeconds (float timeInSeconds) {
        int minutes = (int) (timeInSeconds / 60);
        int seconds = (int) (timeInSeconds % 60);
        return (minutes, seconds);
    }
}
