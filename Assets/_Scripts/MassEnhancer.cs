using UnityEngine;
using System.Collections;
using InControl;

public class MassEnhancer : MonoBehaviour {


	private Rigidbody2D rb;
	public KeyCode clean;
	private int playerNumber;
	public float cleaningMass;

	void Awake()
	{

		rb = GetComponent<Rigidbody2D>();

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
	}

	void FixedUpdate() 
	{
		if (Input.GetKey(clean) || InputManager.Devices[playerNumber].Action1)
		{
			rb.mass = cleaningMass;	
		}
		else
		{
			
			rb.mass = 10;
		}
}

}
