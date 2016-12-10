using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public LayerMask VictoryMask;
	public LayerMask VoidMask;
	#region Singleton
	public static PlayerManager m_instance;
	void Awake(){
		if(m_instance == null){
			//If I am the first instance, make me the Singleton
			m_instance = this;
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
		GameStateManager.onChangeStateEvent += handleChangeGameState;
	}
	
	// Update is called once per frame
	void Update () {
		if (Physics2D.OverlapCircle (this.transform.position, 0.3f, VictoryMask)) {
			GameStateManager.setGameState (GameState.GameOver);
		}
		if (Physics2D.OverlapCircle (this.transform.position, 0.3f, VoidMask)) {
			GameStateManager.setGameState (GameState.GameOver);
		}
	
	}

	void handleChangeGameState(GameState newState){
		Debug.Log ("PLAYER SEE THE NEW STATE : " + newState);
	}



	#region Intéraction
	public static void UP(){
		Debug.Log("UP ! ");
		PlayerManager.m_instance.transform.position += new Vector3 (0, 0.45f);
	}

	public static void DOWN(){
		Debug.Log("DOWN ! ");
		PlayerManager.m_instance.transform.position += new Vector3 (0, -0.45f);
	}

	public static void LEFT(){
		Debug.Log("LEFT ! ");
		PlayerManager.m_instance.transform.position += new Vector3 (-0.45f,0);
	}

	public static void RIGHT(){
		PlayerManager.m_instance.transform.position += new Vector3 (0.45f, 0);
	}
	#endregion Intéraction

	void OnDestroy(){
		GameStateManager.onChangeStateEvent -= handleChangeGameState;
	}
}
