using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bombeCOunter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addBombes(int nb){
		this.GetComponent<Animator> ().SetTrigger ("newBombe");
		this.GetComponentInChildren<Text> ().text = nb.ToString ();
	}

	public void removeBombe(int nb){
		this.GetComponentInChildren<Text> ().text = nb.ToString ();
	}
}
