using UnityEngine;
using System.Collections;

public class PlanetPicker : MonoBehaviour {

	public Sprite[] planetSkin;
    private Rigidbody2D rb;
    private SpaceshipController spaceshipRb;
    public int planetSpeed;

	void Awake () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spaceshipRb = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<SpaceshipController>();

		gameObject.GetComponent<SpriteRenderer> ().sprite = planetSkin[Random.Range (0, planetSkin.Length - 1)];
	}

    void FixedUpdate()
    {
        rb.velocity = new Vector3(-spaceshipRb.horizontalSpeed, -spaceshipRb.verticalSpeed/10);
    }
}
