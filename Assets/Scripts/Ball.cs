using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float adjustingMult = 4f;
	public float tweakMult = 1f;
	public float maxSpeed = 10f;
	public GameObject boom;
	public AudioClip launchSound;

	private Paddle paddle;
	private bool hasStarted = false;
	private Vector3 paddleToBallVector;

	// Use this for initialization
	void Start () {
		paddle = GameObject.FindObjectOfType<Paddle> ();

		paddleToBallVector = this.transform.position - paddle.transform.position;
		this.GetComponent<Rigidbody2D>().simulated = false;
		print (paddleToBallVector);
	
	}

	void OnCollisionEnter2D (Collision2D col){

		foreach (ContactPoint2D missileHit in col.contacts)
		{
			Vector2 hitPoint = missileHit.point;
			Instantiate(boom, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
		}
			
		TweakVelociti ();

		if (!(col.gameObject.tag == "Breakable"))
		GetComponent<AudioSource>().Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasStarted) {
			// Lock ball relative to the paddle
			this.transform.position = paddle.transform.position + paddleToBallVector;

			//Wait for mouse press to lunch 
			if (Input.GetMouseButtonDown (0)) {
				StartTheBall ();
			}
		}

	}




	void TweakVelociti (){
		if (GetComponent<Rigidbody2D>().velocity.magnitude <= maxSpeed) {
			Vector2 tweak = GetComponent<Rigidbody2D>().velocity * Random.Range (0f, 0.05f) * tweakMult;
			GetComponent<Rigidbody2D>().velocity += tweak;
		}
		GetComponent<Rigidbody2D>().velocity = Rotate (this.GetComponent<Rigidbody2D>().velocity, Random.Range(0f,0.1f));
		print ("Velocity = " + GetComponent<Rigidbody2D>().velocity.magnitude);
	}

	void TweakAngle(){
		float adjusting = Mathf.Clamp( (paddle.transform.position.x - this.transform.position.x),-0.5f,0.5f)*adjustingMult;
		this.GetComponent<Rigidbody2D>().velocity = Rotate (this.GetComponent<Rigidbody2D>().velocity, adjusting);
	}

	Vector2 Rotate(Vector2 aPoint, float aDegree)
	{
		float s = Mathf.Sin(aDegree);
		float c = Mathf.Cos(aDegree);
		return new Vector2( aPoint.x * c - aPoint.y * s, aPoint.y * c + aPoint.x * s);
	}

	void StartTheBall (){
		hasStarted = true;
		AudioSource.PlayClipAtPoint (launchSound, transform.position, 1f);
		this.GetComponent<Rigidbody2D>().simulated = true;
		this.GetComponent<Rigidbody2D>().velocity = new Vector2 (Random.Range(-1f,1f), 7f);
	}
}
