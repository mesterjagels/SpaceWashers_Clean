using UnityEngine;
using System.Collections;
using Uniduino;
using DG.Tweening;

public class SpaceshipController : MonoBehaviour
{
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
                pinBtn2 = 7,
                pinBtn1Light = 8,
                pinBtn2Light = 9,
                pinLeftLast = 0,
                pinRightLast = 0,
                pinUpLast = 0,
                pinDownLast = 0,
                pinBtn1Last = 0,
                pinBtn2Last = 0;

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
        AkSoundEngine.PostEvent("thrusters", gameObject);
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
                //Go left
                Debug.Log("Left Mouse clicked");
                horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, turnSpeed * -1, horizontalTurnAcc * Time.deltaTime);
                pinLeftLast = 1;        
                gameObject.transform.DORotate(new Vector3(0, 0, 30), 2);
                if (!rightWind.active)
                {
                    rightWind.SetActive(true);
                }

            }
            else if (Input.GetKey(right) | arduino.digitalRead(pinRight) == 1)
            {
                //Go right
                horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, turnSpeed, horizontalTurnAcc * Time.deltaTime);
                pinRightLast = 1;
                gameObject.transform.DORotate(new Vector3(0, 0, -30), 2);

                if (!leftWind.active)
                {
                    leftWind.SetActive(true);
                }
            }
            else
            {
                horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, 0, horizontalTurnDec * Time.deltaTime);
                leftWind.SetActive(false);
                rightWind.SetActive(false);
                if (gameObject.transform.rotation.z != 0) { 
                gameObject.transform.DORotate(new Vector3(0, 0, 0), 1);
                }
            }
            if (Input.GetKeyDown(throttleUp))
            {
                //Speed up
                Debug.Log("Up clicked");
                if (currentGear < throttleSpeeds.Length - 1)
                {
                    currentGear++;
                }
                verticalSpeed = throttleSpeeds[currentGear];          
            }
            if (Input.GetKeyDown(throttleDown))
            {
                if (currentGear > 0)
                {
                    currentGear--;
                }
                verticalSpeed = throttleSpeeds[currentGear];
                // verticalSpeed = Mathf.MoveTowards(verticalSpeed, throttleSpeeds[currentGear], throttleAcc * Time.deltaTime);
            }
            if (Input.GetKeyDown(boost))
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
       

        //if (Input.GetMouseButtonUp(0) | arduino.digitalRead(pinLeft) == 0 && horizontalSpeed > 0 && pinLeftLast == 1)
        //{
        //    Debug.Log("Left Mouse clicked");
        //    //Stop going left
        //    horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, 0, horizontalTurnDec * Time.deltaTime);
        //    pinLeftLast = 0;
        //}

        //if (Input.GetMouseButtonUp(1) | arduino.digitalRead(pinRight) == 0 && horizontalSpeed < 0 && pinRightLast == 1)
        //{
        //    //Stop going right
        //    horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, 0, horizontalTurnDec * Time.deltaTime);
        //    pinRightLast = 0;
        //}

        //if (Input.GetAxis("Mouse ScrollWheel") > 0 | arduino.digitalRead(pinUp) == 1 && moveSpeed < maxSpeed)
        //{
        //    //Throttle up


        //    if (curSpeed < speeds.Count - 1 && moveSpeed == speeds[curSpeed])
        //    {
        //        curSpeed += 1;
        //    }
        //}
        //else if (Input.GetAxis("Mouse ScrollWheel") < 0 | arduino.digitalRead(pinDown) == 1 && moveSpeed > minSpeed)
        //{
        //    //Throttle down

        //    //moveSpeed -= speedPerAcceleration;
        //    if (curSpeed + 1 >= 0 && moveSpeed == speeds[curSpeed])
        //    {
        //        curSpeed -= 1;
        //    }
        //}
        //if (moveSpeed > maxSpeed)
        //{
        //    moveSpeed = maxSpeed;
        //}
        //if (moveSpeed < minSpeed)
        //{
        //    moveSpeed = minSpeed;
        //}

        //if ((Input.GetKey(shieldButton) | arduino.digitalRead(pinBtn1) == 1) && shieldActivatable && curShield > 0)
        //{
        //    //Shield activated if shield energy is over threshold
        //    shieldActive = true;
        //    pinBtn1Last = 1;
        //}
        //else {
        //    shieldActive = false;
        //}

        //if (curShield <= 0 && shieldActive)
        //{
        //    shieldActive = false;
        //}

        //if (Input.GetKeyDown(boostButton) | arduino.digitalRead(pinBtn2) == 1 && boostActivatable)
        //{
        //    //Boost activated if boost energy is over threshold 
        //    boostActive = true;
        //}
        //if (boostActive && curBoost <= 0)
        //{
        //    boostActive = false;
        //}
    }

    void ConfigurePins()
    {
        arduino.pinMode(pinUp, PinMode.INPUT);
        arduino.pinMode(pinDown, PinMode.INPUT);
        arduino.pinMode(pinLeft, PinMode.INPUT);
        arduino.pinMode(pinRight, PinMode.INPUT);
        arduino.pinMode(pinBtn1, PinMode.INPUT);
        arduino.pinMode(pinBtn2, PinMode.INPUT);
        arduino.pinMode(pinBtn1Light, PinMode.OUTPUT);
        arduino.pinMode(pinBtn2Light, PinMode.OUTPUT);

        //Only need to activate once for one pin, becuase all pins are on the same Port
        arduino.reportDigital((byte)(pinUp / 8), 1);
    }
}
