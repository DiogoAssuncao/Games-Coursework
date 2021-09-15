using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chamber 3 Dragon Statue Puzzle
// Both buttons need to be pressed to lower the case
public class GlassCaseScript : MonoBehaviour {
    public GameObject objectInside;
    public GameObject button1, button2;
    public Door GlassCase;
    private bool open = false;


    void Update() {
        bool bt1Open = button1.GetComponent<ButtonScript>().open;
        bool bt2Open = button2.GetComponent<ButtonScript>().open;
        if (bt1Open && bt2Open) {
            if(!open) {
                objectInside.GetComponent<SphereCollider>().enabled = true;
                GlassCase.Open();
                open = true;
            }
        } else if (open) {
            objectInside.GetComponent<SphereCollider>().enabled = false;
            GlassCase.Close();
            open = false;
        }
    }
}
