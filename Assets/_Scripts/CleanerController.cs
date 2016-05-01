﻿using UnityEngine;
using System.Collections;
using InControl;
using DG.Tweening;

public class CleanerController : MonoBehaviour
{

    private Rigidbody2D rb;
    public float force = 1;
    public KeyCode up, down, left, right;
    private int playerNumber;
    private Destructible2D.D2dRepeatStamp stamp;
    public GameObject soap;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stamp = GetComponent<Destructible2D.D2dRepeatStamp>();

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

    void FixedUpdate()  {
        if (Input.GetKey(up) || InputManager.Devices[playerNumber].LeftStickUp) {
            Debug.Log("Up");
            rb.velocity = new Vector2(rb.velocity.x, force);
        }
        if (Input.GetKey(down) || InputManager.Devices[playerNumber].LeftStickDown) {
            Debug.Log("down");
            rb.velocity = new Vector2(rb.velocity.x, -force);
        }
        if (Input.GetKey(left) || InputManager.Devices[playerNumber].LeftStickLeft){
            Debug.Log("left");
            rb.velocity = new Vector2(-force, rb.velocity.y);
        }
        if (Input.GetKey(right) || InputManager.Devices[playerNumber].LeftStickRight) {
            Debug.Log("right");
            rb.velocity = new Vector2(force, rb.velocity.y);
        }
        if (InputManager.Devices[playerNumber].Action1)
        {
            stamp.enabled = true;
            soap.GetComponent<ParticleSystem>().enableEmission = true;
         //   soap.SetActive(true);
        }
        else
        {
            stamp.enabled = false;
            soap.GetComponent<ParticleSystem>().enableEmission = false;
           // soap.SetActive(false);
        }
        
    }
}
