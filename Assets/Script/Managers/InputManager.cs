﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	#region Singleton
	private static InputManager m_instance;
	void Awake(){
		if(m_instance == null){
			//If I am the first instance, make me the Singleton
			m_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}else{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != m_instance)
				Destroy(this.gameObject);
		}
	}
	#endregion Singleton

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		/*switch (GameStateManager.getGameState ()) {
		case GameState.Menu:
			UpdateMenuState();
			break;
		case GameState.Playing:
			UpdatePlayingState();
			break;
		case GameState.Pause:
			UpdatePauseState();
			break;
		case GameState.GameOver:
			UpdateGameOverState();
			break;
		}*/
		UpdatePlayingState();
	}

	void UpdateMenuState(){
		if(Input.GetKeyDown(KeyCode.Return)){
			GameStateManager.setGameState (GameState.Playing);
			Application.LoadLevelAsync ("LevelScene");
		}
	}

	void UpdatePlayingState(){
		if(Input.GetKeyDown("p")){
			Debug.Log("PAUSE ! ");
			GameStateManager.setGameState(GameState.Pause);
		}

		if(Input.GetKeyDown("z") || Input.GetKeyDown("w")){
			BlockManager.UP();
		}
		
		if(Input.GetKeyDown("q") || Input.GetKeyDown("a")){
			BlockManager.LEFT();
		}
		
		if(Input.GetKeyDown("s")){
			BlockManager.DOWN();
		}
		if(Input.GetKeyUp("s")){
			BlockManager.DOWNRelease();
		}
		
		if(Input.GetKeyDown("d")){
			BlockManager.RIGHT();
		}

		if(Input.GetKeyDown(KeyCode.Mouse0)){
			PlayerManager.ASKFORBOMBE(Input.mousePosition);
		}

		if(Input.GetKeyDown(KeyCode.Mouse1)){
			PlayerManager.MOVEATPOSITION(Input.mousePosition);
		}


		if(Input.GetKeyDown(KeyCode.UpArrow)){
			BlockManager.UP();
		}

		if(Input.GetKeyDown(KeyCode.DownArrow)){
			BlockManager.DOWN();
		}
		if(Input.GetKeyUp(KeyCode.DownArrow)){
			BlockManager.DOWNRelease();
		}

		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			BlockManager.LEFT();
		}

		if(Input.GetKeyDown(KeyCode.RightArrow)){
			BlockManager.RIGHT();
		}

	}

	void UpdatePauseState(){
		if(Input.GetKeyDown("p")){
			Debug.Log("DÉPAUSE ! ");
			GameStateManager.setGameState(GameState.Playing);
		}
	}

	void UpdateGameOverState(){

	}

}
