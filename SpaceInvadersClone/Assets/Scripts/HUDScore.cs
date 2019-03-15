using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScore : MonoBehaviour {
    private Text score;
    void Start () {
        score = GetComponent<Text> ();
    }

    public void SetScore (int score) {
        this.score.text = score.ToString ();
    }
}