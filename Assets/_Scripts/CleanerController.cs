using UnityEngine;
using System.Collections;
using InControl;

public class CleanerController : MonoBehaviour {

	private Rigidbody2D rb;
	public float force = 1;
    public KeyCode up, down, left, right;
    private InputDevice device;
    private InputControl control;
    private int playerNumber;

    void Awake () {
        switch (gameObject.tag)
        {
            case "Player1":
                playerNumber = 0;
                break;

            case "Player2":
                playerNumber = 1;
                break;

            case "Player3":
                playerNumber = 2;
                break;


        }
		rb = GetComponent<Rigidbody2D>();


	}
	
	// Update is called once per frame
	void Update () {
	

		if (Input.GetKey (up) || InputManager.Devices[playerNumber].LeftStickUp) {
			rb.velocity = new Vector2(rb.velocity.x, force);
		}

		if (Input.GetKey (left) || InputManager.Devices[playerNumber].LeftStickLeft) {
			rb.velocity = new Vector2(-force, rb.velocity.y);
		}

		if (Input.GetKey (right) || InputManager.Devices[playerNumber].LeftStickRight) {
			rb.velocity = new Vector2(force, rb.velocity.y);
		}

		if (Input.GetKey (down) || InputManager.Devices[playerNumber].LeftStickDown) {
			rb.velocity = new Vector2(rb.velocity.x, -force);

		}

		
	}

}
