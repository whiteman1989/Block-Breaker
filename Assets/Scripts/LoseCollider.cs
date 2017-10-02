using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour {

	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager> ();

		Debug.Log ("triger is started");
	}
	
	void OnTriggerEnter2D (Collider2D trigger){
		Debug.Log (gameObject.name + " trigger " + trigger.name);
		levelManager.LoadLevel ("Loose");
	}
}
