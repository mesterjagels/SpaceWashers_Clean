using UnityEngine;
using System.Collections;

public class BarrelScript : MonoBehaviour {

    public Sprite[] barrelSprites;
    private SpriteRenderer sr;
    bool spriteChanged = false;
    public GameObject oozeObj;
    private ParticleSystem ooze;

    void Awake()
    {
        ooze = oozeObj.GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        ooze.enableEmission = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!spriteChanged)  {
            sr.sprite = barrelSprites[Random.Range(0, 2)];
            spriteChanged = true;
        }
        if (!ooze.enableEmission){ ooze.enableEmission = true; }
    }
}
