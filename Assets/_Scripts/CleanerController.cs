using UnityEngine;
using System.Collections;

public class CleanerController : MonoBehaviour {


	private Rigidbody2D rb;
	public float force = 1;


	void Awake () {

		rb = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
	

		if (Input.GetKey ("w")) {
			rb.velocity = new Vector2(rb.velocity.x, force);
		}

		if (Input.GetKey ("a")) {
			rb.velocity = new Vector2(-force, rb.velocity.y);
		}

		if (Input.GetKey ("d")) {
			rb.velocity = new Vector2(force, rb.velocity.y);
		}

		if (Input.GetKey ("s")) {
			rb.velocity = new Vector2(rb.velocity.x, -force);

		}

		
	}

}
