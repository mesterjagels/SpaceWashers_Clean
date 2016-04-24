using UnityEngine;
using System.Collections;

public class PlanetPicker : MonoBehaviour {

	public Sprite[] planetSkin;
    private Rigidbody2D rb;
    private Rigidbody2D spaceshipRb;
    public int planetSpeed;

	void Awake () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spaceshipRb = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Rigidbody2D>();

		gameObject.GetComponent<SpriteRenderer> ().sprite = planetSkin[Random.RandomRange (0, planetSkin.Length - 1)];
        rb.velocity = new Vector2(0,planetSpeed);
	}
}
