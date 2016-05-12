using UnityEngine;
using System.Collections;
using InControl;
using DG.Tweening;

public class CleanerController : MonoBehaviour
{

    private Rigidbody2D rb;
    public float force;
    public float handVel = 0;
    public KeyCode up, down, left, right, clean;
    public int playerNumber;
    private Destructible2D.D2dRepeatStamp stamp;
    public GameObject soap;
    
    private Animator scoringAnim;

    void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        stamp = GetComponent<Destructible2D.D2dRepeatStamp>();
		scoringAnim = GetComponentInChildren<Animator> ();

        switch (gameObject.tag)
        {
            case "Player1":
                playerNumber = 0;
                AkSoundEngine.SetSwitch("Cleaning_Utensils", "WindowThing", gameObject);
                break;

            case "Player2":
                playerNumber = 1;
                AkSoundEngine.SetSwitch("Cleaning_Utensils", "Soap", gameObject);
                break;

            case "Player3":
                playerNumber = 2;
                AkSoundEngine.SetSwitch("Cleaning_Utensils", "Brush", gameObject);
                break;
        }
    }

    void Start()
    {
        AkSoundEngine.PostEvent("cleaningSqueak", gameObject);
    }

    void FixedUpdate()  {

		force = 2;

        if (Input.GetKey(up) || InputManager.Devices[playerNumber].LeftStickUp) {
            rb.velocity = new Vector2(rb.velocity.x, force);
			Dash ();
        }
        if (Input.GetKey(down) || InputManager.Devices[playerNumber].LeftStickDown) {
            rb.velocity = new Vector2(rb.velocity.x, -force);
			Dash ();
        }
        if (Input.GetKey(left) || InputManager.Devices[playerNumber].LeftStickLeft){
            rb.velocity = new Vector2(-force, rb.velocity.y);
			Dash ();
        }
        if (Input.GetKey(right) || InputManager.Devices[playerNumber].LeftStickRight) {
            rb.velocity = new Vector2(force, rb.velocity.y);
			Dash ();
        }

        if (Input.GetKey(clean) || InputManager.Devices[playerNumber].Action1)
        {
            handVel = rb.velocity.magnitude;
            stamp.enabled = true;
            soap.GetComponent<ParticleSystem>().enableEmission = true;
        }
        else
        {
            handVel = 0;
            stamp.enabled = false;
            soap.GetComponent<ParticleSystem>().enableEmission = false;

        }
        AkSoundEngine.SetRTPCValue("windowSwipe", handVel, gameObject);
        AkSoundEngine.SetRTPCValue("soapSwipe", handVel, gameObject);
        AkSoundEngine.SetRTPCValue("brushSwipe", handVel, gameObject);
    }

	public void Dash()
	{
		if (InputManager.Devices [playerNumber].Action2) 
		{
			force = 8;
		}
	}

	public void ScoringAnimation()
	{
		//animation.Play;
	}

    void OnDestroy()
    {
        AkSoundEngine.PostEvent("muteObject", gameObject);
    }

    
}
