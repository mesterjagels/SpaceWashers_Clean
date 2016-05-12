using UnityEngine;
using System.Collections;

public class CleanerCollision : MonoBehaviour {


    public GameObject obj;
    private CleanerController cc;

    // Use this for initialization
    void Start () {

        cc = obj.GetComponent<CleanerController>();
    }
	
	// Update is called once per frame
	void Update () {

        

    }

    public void OnCollisionEnter2D (Collision2D col)
    {
        if (cc.playerNumber == 0)
        {
            AkSoundEngine.SetSwitch("Cleaner", "Babe", gameObject);
           
        } 

        if (cc.playerNumber == 1)
        {
            AkSoundEngine.SetSwitch("Cleaner", "Gangsta", gameObject);
           

        }
        if (cc.playerNumber == 2)
        {
            AkSoundEngine.SetSwitch("Cleaner", "Grunger", gameObject);
        }

        AkSoundEngine.PostEvent("ow", gameObject);
     
       
       
    }
}
