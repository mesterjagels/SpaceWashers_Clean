using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;
using DG.Tweening;
using Uniduino;

public class TitleScreen : MonoBehaviour {

    public GameObject player1, player2, player3;
    private Text p1txt, p2txt, p3txt;
    public GameObject p1, p2, p3;


    [HideInInspector]
    public int playerCountInTitle = 0;
    public bool player1joined = false, player2joined = false, player3joined = false;

    private Arduino arduino;
    private int pinUp = 2,
                pinDown = 3,
                pinLeft = 4,
                pinRight = 5,
                pinBtn1 = 6,
                pinBtn2 = 7;

    void Start()
    {
        p1txt = player1.GetComponent<Text>();
        p2txt = player2.GetComponent<Text>();
        p3txt = player3.GetComponent<Text>();

        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.O) | arduino.digitalRead(pinBtn1) == 1)
        {
            Application.LoadLevel(1);
        }
        if (player1joined | player2joined | player3joined) { 
            
        }

        if (InputManager.Devices[0].Action1 | InputManager.Devices[0].Action2 | InputManager.Devices[0].Action3 | InputManager.Devices[0].Action4 && !player1joined)
        {
            p1txt.text = "Player 1 joined";
            player1joined = true;
            p1.transform.localScale = new Vector3(0, 0, 0);
            p1.transform.DOScale(2, 2);
            p1.transform.DORotate(new Vector3(0, 0, 540), 2, RotateMode.FastBeyond360);
            p1.SetActive(true);
            playerCountInTitle++;
        }
        if (InputManager.Devices[1].Action1 | InputManager.Devices[1].Action2 | InputManager.Devices[1].Action3 | InputManager.Devices[1].Action4 && !player2joined)
        {
            p2txt.text = "Player 2 joined";
            player2joined = true;
            p2.SetActive(true);
            p2.transform.localScale = new Vector3(0, 0, 0);
            p2.transform.DOScale(2, 2);
            p2.transform.DORotate(new Vector3(0, 0, 540), 2, RotateMode.FastBeyond360);
            playerCountInTitle++;
        }
        if (InputManager.Devices[2].Action1 | InputManager.Devices[1].Action2 | InputManager.Devices[2].Action3 | InputManager.Devices[2].Action4 && !player3joined)
        {
            p3txt.text = "Player 3 joined";
            player3joined = true;
            p3.transform.localScale = new Vector3(0, 0, 0);
            p3.transform.DOScale(2, 2);
            p3.transform.DORotate(new Vector3(0, 0, 540), 2, RotateMode.FastBeyond360);
            p3.SetActive(true);
            playerCountInTitle++;
        }


    }

    void ConfigurePins()
    {
        arduino.pinMode(pinBtn1, PinMode.INPUT);
        arduino.pinMode(pinBtn2, PinMode.INPUT);

        //Only need to activate once for one pin, becuase all pins are on the same Port
        arduino.reportDigital((byte)(pinUp / 8), 1);
    }
}
