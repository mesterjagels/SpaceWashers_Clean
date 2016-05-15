using UnityEngine;
using System.Collections;

public class CleanerCollision : MonoBehaviour {


    public GameObject obj;
    private CleanerController cc;
    private float timer = 0;
    private bool mute = false;

    // Use this for initialization
    void Start () {

        cc = obj.GetComponent<CleanerController>();
    }
	
	// Update is called once per frame
	void Update () {

        if (timer > 0)
        {
            timer++;
        }

        if (timer == 50)
        {
            timer = 0;
        }
        

    }

    public void OnCollisionEnter2D (Collision2D col)
    {
        if (cc.playerNumber == 0 && !mute)
        {
            AkSoundEngine.SetSwitch("Cleaner", "Babe", gameObject);
            AkSoundEngine.PostEvent("ow", gameObject);
        } 

        if (cc.playerNumber == 1 && !mute)
        {
            AkSoundEngine.SetSwitch("Cleaner", "Gangsta", gameObject);
            AkSoundEngine.PostEvent("ow", gameObject);

        }
        if (cc.playerNumber == 2 && !mute)
        {
            AkSoundEngine.SetSwitch("Cleaner", "Grunger", gameObject);
            AkSoundEngine.PostEvent("ow", gameObject);
        }

        
     
       
       
    }
}
