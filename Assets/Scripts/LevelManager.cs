using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadLevel (string name){
		Debug.Log ("Load level requested for: " + name);
        SceneManager.LoadScene(name);
	}

	public void QuitRequest (){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

	public void LoadNextLevel () {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene (currentScene.buildIndex + 1);
	}

	public void BrickDestroyed () {
		if ( Brick.breakableCount <= 0){
			LoadNextLevel ();
		}
	}
}
