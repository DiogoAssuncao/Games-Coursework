using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Final Puzzle
// Opens the main door if the right combination of podiums is set on Chamber 3
public class ComboLock : MonoBehaviour
{
    public Door door;
    private bool open = false;
    private PodiumScript podium1 , podium2, podium3;

    void Start() {
        podium1 = transform.GetChild(0).gameObject.GetComponent<PodiumScript>();
        podium2 = transform.GetChild(1).gameObject.GetComponent<PodiumScript>();
        podium3 = transform.GetChild(2).gameObject.GetComponent<PodiumScript>();
    }


    void Update() {
        if (podium1.open && podium2.open && podium3.open) {
            if(!open){
                door.Open();
                open = true;
            }
        } else {
            if(open) {
                door.Close();
                open = false;
            }
        }
    }
}
