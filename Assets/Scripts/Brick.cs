using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public int maxHits;
	public int timesHit;

	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
	
	}

	void OnCollisionEnter2D (Collision2D col) {
		timesHit ++;
		SimulateWin ();
	}

	// TODO Remove this method once we can actually win!
	void SimulateWin (){
		levelManager.LoadNextLevel ();
	}
	

}
