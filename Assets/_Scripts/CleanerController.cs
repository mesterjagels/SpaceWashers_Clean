using UnityEngine;
using System.Collections;

public class CleanerController : MonoBehaviour {

	private Rigidbody2D rb;
	public float force = 1;
    public KeyCode up, down, left, right;


	void Awake () {

		rb = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
	

		if (Input.GetKey (up)) {
			rb.velocity = new Vector2(rb.velocity.x, force);
		}

		if (Input.GetKey (left)) {
			rb.velocity = new Vector2(-force, rb.velocity.y);
		}

		if (Input.GetKey (right)) {
			rb.velocity = new Vector2(force, rb.velocity.y);
		}

		if (Input.GetKey (down)) {
			rb.velocity = new Vector2(rb.velocity.x, -force);

		}

		
	}

}
