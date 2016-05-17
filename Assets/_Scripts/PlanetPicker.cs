using UnityEngine;
using System.Collections;

public class PlanetPicker : MonoBehaviour {

	public Sprite[] planetSkin;
    private Rigidbody2D rb;
    private SpaceshipController spaceshipRb;
    public int planetSpeed;

	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spaceshipRb = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<SpaceshipController>();
		gameObject.GetComponent<SpriteRenderer> ().sprite = planetSkin[Random.Range (0, planetSkin.Length-1)];
        float size = Random.Range(2, 5);
        transform.localScale = new Vector2(size, size);
        GetComponent<SpriteRenderer>().sortingLayerName = "Background";
	}

    void FixedUpdate()
    {
        rb.velocity = new Vector3(-spaceshipRb.horizontalSpeed/5, -spaceshipRb.verticalSpeed/10);
    }
}
