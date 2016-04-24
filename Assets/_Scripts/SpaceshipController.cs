using UnityEngine;
using System.Collections;
using Uniduino;


public class SpaceshipController : MonoBehaviour
{

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
    public float throttle1 = 10,
                 throttle2 = 20,
                 throttle3 = 30,
                 throttle4 = 40,
                 throttle5 = 50,
                 horizontalTurnAcc,
                 horizontalTurnDec,
                 throttleAcc;

    [HideInInspector]
    //Accessed by other scripts to determine movement
    public float horizontalSpeed,
                 verticalSpeed = 5f;


    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
    }

    void Update()
    {
        Controls();
    }

    void Controls()
    {
        if (Input.GetMouseButton(0) | arduino.digitalRead(pinLeft) == 1)
        {
            //Go left
            Debug.Log("Left Mouse clicked");
            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, turnSpeed * -1, horizontalTurnAcc * Time.deltaTime);
            pinLeftLast = 1;
        }
        else if (Input.GetMouseButton(1) | arduino.digitalRead(pinRight) == 1)
        {
            //Go right
            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, turnSpeed, horizontalTurnAcc * Time.deltaTime);
            pinRightLast = 1;
        } else
        {
            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, 0, horizontalTurnDec * Time.deltaTime);
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
