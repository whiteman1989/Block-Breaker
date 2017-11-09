using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float adjustingMult = 4f;
	public float tweakMult = 1f;
	public float maxSpeed = 10f;
	public GameObject boom;
	public AudioClip launchSound;
    public int maxBounce = 5;
    public float sensitivityLops = 1;
    public float forceVerticalCorrection = 1f , forceHorizontalCorrection = 1f;
    // debug options
    public float startForceX = 1f , startForceY = 8f;
    public bool randomizeStartForce = true;

    //private var
    private Paddle paddle;
	private bool hasStarted = false;
	private Vector3 paddleToBallVector;
    private int horizontalBounce = 0;
    private int verticalBounce = 0;


	// Use this for initialization
	void Start () {
		paddle = FindObjectOfType<Paddle>();
        paddleToBallVector = this.transform.position - paddle.transform.position;
		this.GetComponent<Rigidbody2D>().simulated = false;
		print (paddleToBallVector);

	
	}
    /** collision enter **/
    void OnCollisionEnter2D (Collision2D col){

		foreach (ContactPoint2D missileHit in col.contacts)
		{
			Vector2 hitPoint = missileHit.point;
			Instantiate(boom, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
		}

        if (!(col.gameObject.tag == "Breakable")) { GetComponent<AudioSource>().Play(); }

        if (horizontalBounce >= maxBounce) { HorizontalLoppExit();}

        if (verticalBounce >= maxBounce) { VerticalLoppExit();}
	}
    
    /** collision exit **/
    void OnCollisionExit2D(Collision2D col)
    {
        TweakVelociti();
        CheckingHorizontal();
    }

    // Update is called once per frame
    void Update () {
		if (!hasStarted) {
			// Lock ball relative to the paddle
			this.transform.position = paddle.transform.position + paddleToBallVector;

			//Wait for mouse press to lunch 
			if (Input.GetMouseButtonDown (0)) { StartTheBall ();}
		}

	}




	void TweakVelociti (){
		if (GetComponent<Rigidbody2D>().velocity.magnitude < maxSpeed) {
            Vector2 curVelocity = GetComponent<Rigidbody2D>().velocity;
            curVelocity.Normalize();
            this.GetComponent<Rigidbody2D>().velocity = curVelocity * maxSpeed;
        }
	}


	Vector2 Rotate(Vector2 aPoint, float aDegree)
	{
		float s = Mathf.Sin(aDegree);
		float c = Mathf.Cos(aDegree);
		return new Vector2( aPoint.x * c - aPoint.y * s, aPoint.y * c + aPoint.x * s);
	}

	void StartTheBall (){
        // debug options
        if (randomizeStartForce) { startForceX = Random.Range(-1f, 1f); }
        hasStarted = true;
		AudioSource.PlayClipAtPoint (launchSound, transform.position, 1f);
		this.GetComponent<Rigidbody2D>().simulated = true;
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(startForceX , startForceY);
        this.GetComponent<Rigidbody2D>().AddTorque(7);

    }

    void CheckingHorizontal() {
        Rigidbody2D ball = this.GetComponent<Rigidbody2D>();
        bool horizontalLoop = ball.velocity.y < 1 * sensitivityLops && ball.velocity.y > -1 * sensitivityLops;
        bool verticalLoop = ball.velocity.x < 1 && ball.velocity.x > -1;
        if (horizontalLoop != verticalLoop)
        {
            //check horizonyal Loop and increment count horizontal bounce
            if (verticalBounce == 0 && horizontalLoop)
            {
                horizontalBounce++;
                verticalBounce = 0;
            }

            //check vertical Loop and increment count vertical bounce
            if (horizontalBounce == 0 && verticalLoop)
            {
                verticalBounce++;
                horizontalBounce = 0;
            }
        }
        else {
            // reset counts
            horizontalBounce = 0;
            verticalBounce = 0;
        }
        print("lopps horizontal: " + horizontalBounce + " and vertical: " + verticalBounce);
    }

    void HorizontalLoppExit() {
        Rigidbody2D ball = this.GetComponent<Rigidbody2D>();
        float speed = ball.velocity.magnitude;
        Vector2 correction = new Vector2(0f,-1f * forceVerticalCorrection);
        //print(correction);//////////////
        ball.velocity += correction;
        Vector2 curVelocity = ball.velocity;
        //print("curVelocity " + curVelocity);/////////////////
        curVelocity.Normalize();
        //print("nweVelocity " + curVelocity);/////////////////
        this.GetComponent<Rigidbody2D>().velocity = curVelocity * speed;
        //print("newVelocity " + this.GetComponent<Rigidbody2D>().velocity);///////////////
        horizontalBounce = 0;
        verticalBounce = 0;
    }

    void VerticalLoppExit()
    {
        Rigidbody2D ball = this.GetComponent<Rigidbody2D>();
        float speed = ball.velocity.magnitude;
        Vector2 correction = new Vector2(Random.Range(-1f,1f) * forceHorizontalCorrection, 0f);
        //print(correction);//////////////
        ball.velocity += correction;
        Vector2 curVelocity = ball.velocity;
        //print("curVelocity " + curVelocity);/////////////////
        curVelocity.Normalize();
        //print("nweVelocity " + curVelocity);/////////////////
        this.GetComponent<Rigidbody2D>().velocity = curVelocity * speed;
        //print("newVelocity " + this.GetComponent<Rigidbody2D>().velocity);///////////////
        horizontalBounce = 0;
        verticalBounce = 0;

    }
}
