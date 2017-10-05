using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float adjustingMult = 4f;
	public float tweakMult = 1f;
	public float maxSpeed = 17f;

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

		TweakVelociti ();


		if (col.gameObject.tag == "Paddle") {
			TweakAngle();
		}

		if (!(col.gameObject.tag == "Breakable"))
		audio.Play ();
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
		if (rigidbody2D.velocity.magnitude <= maxSpeed) {
			Vector2 tweak = rigidbody2D.velocity * Random.Range (0f, 0.05f) * tweakMult;
			rigidbody2D.velocity += tweak;
		}
		rigidbody2D.velocity = Rotate (this.rigidbody2D.velocity, Random.Range(0f,0.1f));
		print ("Velocity = " + rigidbody2D.velocity.magnitude);
	}

	void TweakAngle(){
		float adjusting = Mathf.Clamp( (paddle.transform.position.x - this.transform.position.x),-0.5f,0.5f)*adjustingMult;
		this.rigidbody2D.velocity = Rotate (this.rigidbody2D.velocity, adjusting);
	}

	Vector2 Rotate(Vector2 aPoint, float aDegree)
	{
		float rad = aDegree * Mathf.Deg2Rad;
		float s = Mathf.Sin(aDegree);
		float c = Mathf.Cos(aDegree);
		return new Vector2( aPoint.x * c - aPoint.y * s, aPoint.y * c + aPoint.x * s);
	}

	void StartTheBall (){
		hasStarted = true;
		this.GetComponent<Rigidbody2D>().simulated = true;
		this.rigidbody2D.velocity = new Vector2 (2f, 10f);
	}
}
