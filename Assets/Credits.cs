using UnityEngine;
using System.Collections;
using InControl;

public class Credits : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        if (InputManager.Devices[0].AnyButton || InputManager.Devices[1].AnyButton || InputManager.Devices[2].AnyButton)
        {
            Application.LoadLevel(0);
        }	
	}
}
