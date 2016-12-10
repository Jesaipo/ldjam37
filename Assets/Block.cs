using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	public LayerMask groundedMask;
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		Collider2D[] touchGroundedRight =  Physics2D.OverlapCircleAll (this.transform.position - new Vector3 (0.9f, 0.9f + 0.45f), 0.05f, groundedMask);
		Collider2D[] touchGroundedLeft =  Physics2D.OverlapCircleAll (this.transform.position - new Vector3 (-0.9f, 0.9f + 0.45f), 0.05f, groundedMask);
		bool grounded = false;
		if (touchGroundedRight.Length > 1 || touchGroundedLeft.Length > 1) {
			grounded = true;
		}
		if (this.transform.position.y < -7.2f) {
			this.transform.position = new Vector3 (this.transform.position.x, -7.2f, this.transform.position.z);
			grounded = true;
		}
			if (grounded && rb.isKinematic == false) {
			//BlockManager.GENERATE = true;
			rb.isKinematic = true;
		}

	}

	public void Left(){
		Debug.Log ("LEFT");
		this.transform.position += new Vector3 (-0.9f, 0, 0);
		if (this.transform.position.x < -13.5f)
			this.transform.position = new Vector3 (-13.5f, this.transform.position.y, this.transform.position.z);
	}
	public void Right(){
		Debug.Log ("Right");
		this.transform.position += new Vector3 (0.9f, 0, 0);
		if (this.transform.position.x > 13.5f)
			this.transform.position = new Vector3 (13.5f, this.transform.position.y, this.transform.position.z);
	}
	public void TurnRight(){
		Debug.Log ("RTurnight");
		this.transform.Rotate(0,0,90);
	}
	public void Fall(){
		rb.AddForce (new Vector2(0, -500));
	}
}
