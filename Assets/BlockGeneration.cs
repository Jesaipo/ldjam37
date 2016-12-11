using UnityEngine;
using System.Collections;

public class BlockGeneration : MonoBehaviour {
	public GameObject[] forms;
	// Use this for initialization
	void Start () {
		int index = Random.Range (0, forms.Length);
		int rotate = Random.Range (0, 3);
		var instance = Instantiate (forms [index]);
		instance.transform.position = this.transform.position;
		instance.transform.Rotate (0, 0, rotate * 90f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
