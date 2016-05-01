using UnityEngine;
using System.Collections;

public class HorizontalWorldMovement : MonoBehaviour {

    private SpaceshipController sc;
    private Rigidbody2D rb;
    public float amount;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sc = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<SpaceshipController>();

    }
	
	// Update is called once per frame
	void Update () {
        rb.velocity = new Vector2(-sc.horizontalSpeed * amount, rb.velocity.y);
    }
}
