using UnityEngine;
using System.Collections;

public class nextBlock : MonoBehaviour {

	public GameObject currentNext= null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setNewNext(GameObject next){
		if (currentNext) {
			Destroy (currentNext);
		}
		currentNext = Instantiate (next);
		currentNext.transform.position = this.transform.position;
		currentNext.GetComponent<Rigidbody2D> ().isKinematic = true;
		foreach (SpriteRenderer sr in currentNext.GetComponentsInChildren<SpriteRenderer>()) {
			sr.sortingOrder += 1000;
		}
	}
}
