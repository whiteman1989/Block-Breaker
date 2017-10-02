using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public int maxHits;
	public int timesHit;

	// Use this for initialization
	void Start () {
		timesHit = 0;
	
	}

	void OnCollisionEnter2D (Collision2D col) {
		timesHit ++;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
