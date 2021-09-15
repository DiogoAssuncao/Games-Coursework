using UnityEngine;

[CreateAssetMenu(fileName = "ScoreManagerData", menuName = "ScriptableObjects/ScoreManager", order = 1)]

// Keeps tracks of the session's last and sessions best
public class ScoreManager : ScriptableObject {
    public float bestTime = 1800.0f;
    public float lastTime = 1800.0f;

    public void setTime(float newTime) {
        lastTime = newTime;
        if (bestTime < newTime)
        bestTime = newTime;
    }
}