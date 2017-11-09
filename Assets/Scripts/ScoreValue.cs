using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreValue : MonoBehaviour {

    public int scoreValue;

    private ScoreKeeper scoreKeeper;

	// Use this for initialization
	void Start () {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }
		
	void OnDestroy () {
        if (scoreKeeper != null) {
            scoreKeeper.Score(scoreValue);
        }
	}
		
}
