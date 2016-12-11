using UnityEngine;
using System.Collections;

public class Bombe : MonoBehaviour {
	public LayerMask DestroyMask;
	public float deflagrationRayon=1f;
	public bool detonate = true;
	// Use this for initialization
	void Start () {
		//Detonate ();
	}
	
	// Update is called once per frame
	void Update () {
		//Detonate ();
	}
	void Detonate() {
		if(detonate){
		detonate = false;
		Collider2D[] touchGrounded = Physics2D.OverlapCircleAll (this.transform.position, deflagrationRayon, DestroyMask);
		Debug.Log ("DETON?ATE " + touchGrounded.Length);
		foreach (Collider2D col in touchGrounded) {
			Destroy destroy = col.gameObject.GetComponent<Destroy> ();
			if (destroy)
				destroy.DestroyObject ();
		
			DestroyPlayer destroyPlayer = col.gameObject.GetComponent<DestroyPlayer> ();
			if (destroyPlayer)
				destroyPlayer.DestroyObject ();


		}
			this.GetComponent<SpriteRenderer> ().enabled = false;
			this.GetComponentInChildren<SpriteRenderer> ().enabled = false;
	}
		//this.gameObject.SetActive (false);
	}
}
