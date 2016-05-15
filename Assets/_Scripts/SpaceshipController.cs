using UnityEngine;
using System.Collections;
using Uniduino;
using DG.Tweening;

public class SpaceshipController : MonoBehaviour
{
    //Scoreboard
    GameManager gameManager;
    public bool levelEnded = false;

    //Control scheme
    public KeyCode 
        throttleUp = KeyCode.UpArrow,
        throttleDown = KeyCode.DownArrow, 
        left = KeyCode.LeftArrow, 
        right = KeyCode.RightArrow, 
        shield, boost;

    //Boost
    public bool isBoosting;
    public float boostFuel = 0;
    private float currentSpeed;
    public int boostSpeed = 100;
    public GameObject leftWind, rightWind;


    //Variables to make the custom controller to work
    private Arduino arduino;
    private int pinUp = 2,
                pinDown = 3,
                pinLeft = 4,
                pinRight = 5,
                pinBtn1 = 6,
                pinBtn2 = 7;

    //Varibles to tweak for spaceship speed
    [Header("Spaceship speed settings")]
    public float turnSpeed;
    public float[] throttleSpeeds;
    public int currentGear = 0;
    [Header("Should be between 0 - 7")]
    public float throttle1;
    public float throttle2,
                 throttle3,
                 throttle4,
                 throttle5;
    [Header("Turn acceleration and deceleration")]
    public float horizontalTurnAcc;
    public float horizontalTurnDec;
    

    //[HideInInspector]
    //Accessed by other scripts to determine movement
    public float horizontalSpeed;
    public float verticalSpeed;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        AkSoundEngine.PostEvent("thrusters", gameObject);
        AkSoundEngine.SetState("isBoosting", "notBoosting");
        throttleSpeeds = new float[5];
        throttleSpeeds[0] = throttle1;
        throttleSpeeds[1] = throttle2;
        throttleSpeeds[2] = throttle3;
        throttleSpeeds[3] = throttle4;
        throttleSpeeds[4] = throttle5;

        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
    }

    void Update()
    {
        AkSoundEngine.SetRTPCValue("vertical_speed", verticalSpeed);
        Controls();

        if (Application.loadedLevel != 0)
        {
            if (gameManager.distanceMath <= 1 && !levelEnded)
            {
                levelEnded = true;
                GameObject.Find("Earth").GetComponent<WorldMovement>().speedmultiplier = 0.2f;
                gameObject.transform.DOScale(0, 5);
            }
        }
    }

    void Controls()
    {
        if (!isBoosting)
        {
            if (boostFuel < 500) { 
            boostFuel++;
            }
            if (Input.GetKey(left) | arduino.digitalRead(pinLeft) == 1)
            {
                horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, turnSpeed * -1, horizontalTurnAcc * Time.deltaTime);
                gameObject.transform.DORotate(new Vector3(0, 0, 30), 2);

                if (!rightWind.active && !rightWind == null)
                {
                    rightWind.SetActive(true);
                }

            }
            else if (Input.GetKey(right) | arduino.digitalRead(pinRight) == 1)
            {
                horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, turnSpeed, horizontalTurnAcc * Time.deltaTime);
                gameObject.transform.DORotate(new Vector3(0, 0, -30), 2);

                if (!leftWind.active && !leftWind == null)
                {
                    leftWind.SetActive(true);
                }
            }
            else
            {   if(Application.loadedLevel != 0) {
                    horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, 0, horizontalTurnDec * Time.deltaTime);
                    if (!leftWind == null && !rightWind == null)
                    {
                        leftWind.SetActive(false);
                        rightWind.SetActive(false);
                    }

                    if (gameObject.transform.rotation.z != 0)
                    {
                        gameObject.transform.DORotate(new Vector3(0, 0, 0), 1);
                    }
                }
                
            }
            if (Input.GetKeyDown(throttleUp) || arduino.digitalRead(pinUp) ==1 )
            {
                //Speed up
                if (currentGear < throttleSpeeds.Length - 1)
                {
                    currentGear++;
                }
                DOTween.To(() => verticalSpeed, x=> verticalSpeed = x, throttleSpeeds[currentGear], 1);
               // verticalSpeed = throttleSpeeds[currentGear];          
            }
            if (Input.GetKeyDown(throttleDown) || arduino.digitalRead(pinDown)==1 )
            {
                if (currentGear > 0)
                {
                    currentGear--;
                }
                DOTween.To(() => verticalSpeed, x => verticalSpeed = x, throttleSpeeds[currentGear], 1);

                //verticalSpeed = throttleSpeeds[currentGear];
                // verticalSpeed = Mathf.MoveTowards(verticalSpeed, throttleSpeeds[currentGear], throttleAcc * Time.deltaTime);
            }
            if (Input.GetKeyDown(boost) || arduino.digitalRead(pinBtn1)==1)
            {
                if (boostFuel > 100)
                {
                AkSoundEngine.SetState("isBoosting", "boosting");
                AkSoundEngine.PostEvent("boost", gameObject);
                currentSpeed = verticalSpeed;
                isBoosting = true;
                }
            }
        }
        if (isBoosting) {
           
            if (boostFuel > 0)
            {
                verticalSpeed = Mathf.MoveTowards(verticalSpeed, boostSpeed, 50 * Time.deltaTime);
                boostFuel = boostFuel - 2.50f;
                isBoosting = true;
            }
            if (boostFuel <= 0)
            {
                AkSoundEngine.PostEvent("boost", gameObject);
                AkSoundEngine.SetState("isBoosting", "notBoosting");
                verticalSpeed = currentSpeed;
                isBoosting = false;
            }
        }
    }

    void ConfigurePins()
    {
        arduino.pinMode(pinUp, PinMode.INPUT);
        arduino.pinMode(pinDown, PinMode.INPUT);
        arduino.pinMode(pinLeft, PinMode.INPUT);
        arduino.pinMode(pinRight, PinMode.INPUT);
        arduino.pinMode(pinBtn1, PinMode.INPUT);
        arduino.pinMode(pinBtn2, PinMode.INPUT);

        //Only need to activate once for one pin, becuase all pins are on the same Port
        arduino.reportDigital((byte)(pinUp / 8), 1);
    }
}
