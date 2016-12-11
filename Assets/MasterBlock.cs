using UnityEngine;
using System.Collections;

public class MasterBlock : MonoBehaviour {
	public LayerMask groundedMask;
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		Collider2D[] touchGroundedRight =  Physics2D.OverlapCircleAll (this.transform.position - new Vector3 (1, 1.5f), 0.05f, groundedMask);
		Collider2D[] touchGroundedLeft =  Physics2D.OverlapCircleAll (this.transform.position - new Vector3 (-1, 1.5f), 0.05f, groundedMask);
		Collider2D[] touchGroundedCenter =  Physics2D.OverlapCircleAll (this.transform.position - new Vector3 (0, 1.5f), 0.05f, groundedMask);
		bool grounded = false;
		if (touchGroundedRight.Length > 1 || touchGroundedLeft.Length > 1 || touchGroundedCenter.Length> 1) {
			
			grounded = true;
		}
		if (this.transform.position.y <= -23.5f) {
			this.transform.position = new Vector3 (this.transform.position.x,  -23.5f, this.transform.position.z);
			grounded = true;
		}
		if (rb && grounded) {
			rb.isKinematic = grounded;
			var blockList = this.GetComponentsInChildren<Block> ();
				foreach(Block b in blockList){
				b.enabled = true;
				}
			//this.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			this.enabled = false;
		}

	}


	public void Left(){
		Debug.Log ("LEFT");
		Collider2D[] touchGroundedRight =  Physics2D.OverlapCircleAll (this.transform.position - new Vector3 (1.5f, 1.5f), 0.05f, groundedMask);
		Collider2D[] touchGroundedLeft =  Physics2D.OverlapCircleAll (this.transform.position - new Vector3 (1.5f, -1.5f), 0.05f, groundedMask);
		Collider2D[] touchGroundedCenter =  Physics2D.OverlapCircleAll (this.transform.position - new Vector3 (1.5f, 0), 0.05f, groundedMask);

		if (touchGroundedRight.Length < 2 && touchGroundedLeft.Length < 2 && touchGroundedCenter.Length < 2) {
			this.transform.position += new Vector3 (-3f, 0, 0);
			if (this.transform.position.x < -9f)
				this.transform.position = new Vector3 (-9f, this.transform.position.y, this.transform.position.z);
		}
	}
	public void Right(){
		Debug.Log ("Right");
		Collider2D[] touchGroundedRight =  Physics2D.OverlapCircleAll (this.transform.position - new Vector3 (-1.5f, 1.5f), 0.05f, groundedMask);
		Collider2D[] touchGroundedLeft =  Physics2D.OverlapCircleAll (this.transform.position - new Vector3 (-1.5f, -1.5f), 0.05f, groundedMask);
		Collider2D[] touchGroundedCenter =  Physics2D.OverlapCircleAll (this.transform.position - new Vector3 (-1.5f, 0), 0.05f, groundedMask);

		if (touchGroundedRight.Length < 2 && touchGroundedLeft.Length < 2 && touchGroundedCenter.Length < 2) {
			this.transform.position += new Vector3 (3f, 0, 0);
			if (this.transform.position.x > 9f)
				this.transform.position = new Vector3 (9f, this.transform.position.y, this.transform.position.z);
		}
	}
	public void TurnRight(){
		Debug.Log ("RTurnight");
		this.transform.Rotate(0,0,90);
	}
	public void Fall(){
		rb.AddForce (new Vector2(0, -500));
	}
}
