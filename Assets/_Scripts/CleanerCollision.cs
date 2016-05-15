using UnityEngine;
using System.Collections;

public class CleanerCollision : MonoBehaviour {


    public GameObject obj;
    private CleanerController cc;
    private float timer = 0;
    private bool mute = false;
    public Sprite normalFace, owFace;
    public GameObject face;
    private SpriteRenderer renderer; 

    // Use this for initialization
    void Start () {
        renderer = face.GetComponent<SpriteRenderer>();
        cc = obj.GetComponent<CleanerController>();
    }
	
	// Update is called once per frame
	void Update () {

        if (timer > 0)
        {
            renderer.sprite = owFace;
            timer++;
            mute = true;
        }

        if (timer == 50)
        {
            renderer.sprite = normalFace;
            timer = 0;
            mute = false;
        }
        

    }

    public void OnCollisionEnter2D (Collision2D col)
    {
        if (cc.playerNumber == 0 && !mute)
        {
            AkSoundEngine.SetSwitch("Cleaner", "Babe", gameObject);
            AkSoundEngine.PostEvent("ow", gameObject);
            timer = 1;
        } 

        if (cc.playerNumber == 1 && !mute)
        {
            AkSoundEngine.SetSwitch("Cleaner", "Gangsta", gameObject);
            AkSoundEngine.PostEvent("ow", gameObject);
            timer = 1;
        }
        if (cc.playerNumber == 2 && !mute)
        {
            AkSoundEngine.SetSwitch("Cleaner", "Grunger", gameObject);
            AkSoundEngine.PostEvent("ow", gameObject);
            timer = 1;
        }
    }
}
