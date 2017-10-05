using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public bool autoPlay = false;

	private Ball ball;


	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!autoPlay) {
			MoveWithMouse ();
		} else {
			AutoPlay ();
		}
	}

	void MoveWithMouse (){
		Vector3 paddlePos = new Vector3 (0.5f, this.transform.position.y , 0f);
		float mousePositionBlocks = Input.mousePosition.x / Screen.width * 16;
		paddlePos.x = Mathf.Clamp(mousePositionBlocks , 0.5f , 15.5f);
		this.transform.position = paddlePos;
	}

	void AutoPlay (){
		Vector3 paddlePos = new Vector3 (0.5f, this.transform.position.y , 0f);
		paddlePos.x = Mathf.Clamp(ball.transform.position.x , 0.5f , 15.5f);
		this.transform.position = paddlePos;
	}
}
