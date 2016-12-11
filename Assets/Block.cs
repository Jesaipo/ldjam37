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
		Collider2D[] touchGrounded =  Physics2D.OverlapCircleAll (this.transform.position - new Vector3 (0, 0.5f), 0.05f, groundedMask);
		bool grounded = false;
		if (touchGrounded.Length > 1) {
			foreach (Collider2D col in touchGrounded) {
				if (this.transform.position != col.gameObject.transform.position) {
					this.transform.position = col.gameObject.transform.position+ new Vector3(0,1f);
					break;
				}
			}
			grounded = true;
		}
		if (this.transform.position.y <= -24.5f) {
			this.transform.position = new Vector3 (this.transform.position.x,  -24.5f, this.transform.position.z);
			grounded = true;
		}
		if(rb)
		rb.isKinematic = grounded;

	}
}
