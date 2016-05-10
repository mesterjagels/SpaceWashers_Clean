using UnityEngine;
using System.Collections;

public class bankLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AkBankManager.LoadBank("main");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
