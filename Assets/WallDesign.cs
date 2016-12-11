using UnityEngine;
using System.Collections;

public class WallDesign : MonoBehaviour {

	public Sprite[] spriteTab;
	// Use this for initialization
	void Start () {
		this.gameObject.GetComponentInChildren<SpriteRenderer> ().sprite = spriteTab [Random.Range (0, spriteTab.Length)];

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
