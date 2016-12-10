using UnityEngine;
using System.Collections;

public class BlockManager : MonoBehaviour {

	public GameObject[] blockList;
	public static Block currentBlock= null;
	public static bool GENERATE = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GENERATE || currentBlock && currentBlock.GetComponent<Rigidbody2D>().isKinematic) {
			GENERATE = false;
			var Block = Instantiate (blockList [0]);
			Block.transform.position = this.transform.position;
			currentBlock = Block.GetComponent<Block>();
		}
	
	}

	#region Intéraction
	public static void UP(){
		if (currentBlock)
		currentBlock.TurnRight ();
	}

	public static void DOWN(){
		if (currentBlock)
		currentBlock.Fall ();
	}

	public static void LEFT(){
		if (currentBlock)
		currentBlock.Left ();
	}

	public static void RIGHT(){
		if (currentBlock)
		currentBlock.Right ();
	}
		
	#endregion Intéraction

}
