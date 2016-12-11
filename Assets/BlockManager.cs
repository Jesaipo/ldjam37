using UnityEngine;
using System.Collections;

public class BlockManager : MonoBehaviour {

	public nextBlock nextDisplay;
	public GameObject[] blockList;
	public static MasterBlock currentMasterBlock= null;
	public static bool GENERATE = true;
	public GameObject nextGameObject= null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (nextGameObject == null) {
			genereNewGO ();
		}
		if (GENERATE || currentMasterBlock && currentMasterBlock.GetComponent<Rigidbody2D>().isKinematic) {
			if (currentMasterBlock && currentMasterBlock.transform.position.y > 9.5f) {
			
				GameStateManager.setGameState (GameState.GameOver);
				Application.LoadLevelAsync ("GameOverScene");
			}
			GENERATE = false;
			MapManager.m_instance.refreshUsedDot ();
			var instance = Instantiate (nextGameObject);
			instance.transform.position = this.transform.position;
			currentMasterBlock = instance.GetComponent<MasterBlock>();
			genereNewGO ();

		}
	}

	void genereNewGO(){
		int index = Random.Range (0, blockList.Length);
		int rotate = Random.Range (0, 3);
		nextGameObject = blockList [index];
		nextGameObject.transform.position = this.transform.position;
		nextGameObject.transform.Rotate (0, 0, rotate * 90f);
		nextDisplay.setNewNext (nextGameObject);
	}

	#region Intéraction
	public static void UP(){
		if (currentMasterBlock)
			currentMasterBlock.TurnRight ();
	}

	public static void DOWN(){
		if (currentMasterBlock)
			currentMasterBlock.Fall ();
	}

	public static void LEFT(){
		if (currentMasterBlock)
			currentMasterBlock.Left ();
	}

	public static void RIGHT(){
		if (currentMasterBlock)
			currentMasterBlock.Right ();
	}
		
	#endregion Intéraction

}
