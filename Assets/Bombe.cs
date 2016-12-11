using UnityEngine;
using System.Collections;

public class Bombe : MonoBehaviour {
	public LayerMask DestroyMask;
	public float deflagrationRayon=1f;
	// Use this for initialization
	void Start () {
		//Detonate ();
	}
	
	// Update is called once per frame
	void Update () {
		//Detonate ();
	}
	void Detonate() {
		
		Collider2D[] touchGrounded =  Physics2D.OverlapCircleAll (this.transform.position, deflagrationRayon, DestroyMask);
		Debug.Log ("DETON?ATE "+ touchGrounded.Length);
		foreach (Collider2D col in touchGrounded) {
			Destroy destroy = col.gameObject.GetComponent<Destroy> ();
			if (destroy)
				destroy.DestroyObject ();
			}
		this.gameObject.SetActive (false);
	}
}
