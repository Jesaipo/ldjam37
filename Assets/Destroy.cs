using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {


	public void DestroyObject(){
		this.gameObject.SetActive (false);
		Destroy (this);

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
