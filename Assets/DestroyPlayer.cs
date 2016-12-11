using UnityEngine;
using System.Collections;

public class DestroyPlayer : MonoBehaviour {


	public void DestroyObject(){
		this.gameObject.SetActive (false);
		GameStateManager.setGameState (GameState.GameOver);
		Application.LoadLevelAsync ("GameOverScene");

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
