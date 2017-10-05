using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {


	public int timesHit;
	public Sprite[] hitSprites;
	public static int breakableCount = 0;
	public AudioClip crack;

	private int maxHits;
	private LevelManager levelManager;
	private bool isBreakable;

	// Use this for initialization
	void Start () {
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		maxHits = hitSprites.Length + 1;
		isBreakable = (this.tag == "Breakable");



		// keep tracking breakable briks
		if (isBreakable) {
			breakableCount ++;
		}
		// print breakable count
		print ("Breakable block - " + breakableCount);
	}

	void Awake () {
		breakableCount = 0;
	}

	void OnCollisionEnter2D (Collision2D col) {
	}

	void OnCollisionExit2D (Collision2D col) {
		if (isBreakable) {
			HandleHits ();
		}
	}

	void HandleHits () {
		timesHit ++;
		AudioSource.PlayClipAtPoint (crack, transform.position, 1f);
		
		if (timesHit >= maxHits) {
			breakableCount --;
			print ("Breakable blocks - " + breakableCount);
			levelManager.BrickDestroyed();
			Destroy (gameObject);
		} else {
			LoadSprites();
		}
	}

	void LoadSprites (){
		int spriteIndex = timesHit - 1;
		if (hitSprites [spriteIndex]) {
			this.GetComponent<SpriteRenderer> ().sprite = hitSprites [spriteIndex];
		}
	}

	// TODO Remove this method once we can actually win!
	void SimulateWin (){
		levelManager.LoadNextLevel ();
	}
	

}
