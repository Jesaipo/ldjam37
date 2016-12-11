using UnityEngine;
using System.Collections;

public class BlockManager : MonoBehaviour {

	public GameObject[] blockList;
	public static MasterBlock currentMasterBlock= null;
	public static bool GENERATE = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GENERATE || currentMasterBlock && currentMasterBlock.GetComponent<Rigidbody2D>().isKinematic) {
			GENERATE = false;
			MapManager.m_instance.refreshUsedDot ();
			int index = Random.Range (0, blockList.Length);
			int rotate = Random.Range (0, 3);
			var instance = Instantiate (blockList [index]);
			instance.transform.position = this.transform.position;
			instance.transform.Rotate (0, 0, rotate * 90f);
			currentMasterBlock = instance.GetComponent<MasterBlock>();

		}
	
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
