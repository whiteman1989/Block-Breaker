using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreValue : MonoBehaviour {

    public int scoreValue;

	private static ScoreKeeper scoreKeeper;

	// Use this for initialization
	void Start () {
		if (scoreKeeper == null) {
			scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
			if (scoreKeeper != null) {
				Debug.Log ("ScoreKeeper sucesfui finded");
			}

		}
    }
		
	void OnDestroy () {
        if (scoreKeeper != null) {
            scoreKeeper.Score(scoreValue);
        }
	}
		
}
