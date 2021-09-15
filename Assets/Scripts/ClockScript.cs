using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// Will handle the clock behaviour
public class ClockScript : MonoBehaviour {
    public GameObject gameController, lever, sound;
    public TextMeshPro clock;
    public int timeMinutes;
    public int lockEndingTimeMinutes;
    private int minutes, seconds;
    private bool started;
    public float secondsLeft;
    private bool stopped = false;

    // Sets the inserted time
    void Start() {
        secondsLeft = timeMinutes * 60;
        (minutes, seconds) = GetMinutesAndSeconds(secondsLeft);
        clock.GetComponent<TextMeshPro>().text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    // checks for lever input to start
    // starts the countdown and sound
    // stops if the game is paused
    // if the clock runs out the game is over
    // of the clock goes beyond a set point the true ending is locked out
    void Update() {
        if (!started && lever.GetComponent<LeverScript>().activated){
            started = true;
            sound.GetComponent<AudioSource>().Play(0);
        }
        if (started && !stopped) {
            secondsLeft -= Time.deltaTime;
            if (secondsLeft <= 0) {
                gameController.GetComponent<GameController>().EndGame();
            }
            if (secondsLeft <= lockEndingTimeMinutes*60) {
                gameController.GetComponent<GameController>().LockEnding();
            }
            (minutes, seconds) = GetMinutesAndSeconds(secondsLeft);
            clock.GetComponent<TextMeshPro>().text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        if (stopped) {
        }
    }

    // transforms seconds to minutes and seconds
    (int, int) GetMinutesAndSeconds (float timeInSeconds) {
        int minutes = (int) (timeInSeconds / 60);
        int seconds = (int) (timeInSeconds % 60);
        return (minutes, seconds);
    }

    // stopping the clock
    // used by player on pause
    public void stop(){
        stopped = true;
        sound.GetComponent<AudioSource>().Pause();
    }

    // starting the clock back again
    // used by player on return from pause
    public void start(){
        stopped = false;
        sound.GetComponent<AudioSource>().Play();
    }
}
