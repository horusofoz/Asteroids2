using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text scoreText;
    public static int score;

	// Use this for initialization
	void Start () {
        score = 0;
        AddScore(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void AddScore(int addScore)
    {
        score += addScore;
        scoreText.text = score.ToString("0000000000");
    }

}
