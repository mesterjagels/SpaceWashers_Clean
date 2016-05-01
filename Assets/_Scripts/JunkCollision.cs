using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolygonCollider2D))]
public class JunkCollision : MonoBehaviour {

    [Header("Drag in the appropiate dirt gameobjects")]
    [Header("Pizza")]
    public GameObject[] pizzaDirt;
    public GameObject pizzaParticle;
    
    [Header("Slime")]
    public GameObject[] slimeDirt;
    public GameObject slimeParticle;

    [Header("Dust?")]
    public GameObject[] dustDirt;
    public GameObject dustParticle;

    void Start()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Vector3 velocityBeforeHit = col.gameObject.GetComponent<Rigidbody2D>().velocity;
        col.gameObject.GetComponent<WorldMovement>().enabled = false;
        col.gameObject.GetComponent<Rigidbody2D>().velocity = velocityBeforeHit;
        Destroy(col.gameObject, 5f);

        switch (col.gameObject.tag)
        {
            case "Pizza":
                AkSoundEngine.SetSwitch("Junk_type", "Pizza", gameObject);
                AkSoundEngine.PostEvent("hull_impact", gameObject);
                foreach (ContactPoint2D pizzaHit in col.contacts)
                {
                    Vector3 hitpoint = new Vector3(pizzaHit.point.x, pizzaHit.point.y, -10);
                    Instantiate(pizzaDirt[Random.Range(0, pizzaDirt.Length - 1)], hitpoint, Quaternion.identity);
                    Instantiate(pizzaParticle, hitpoint, Quaternion.Euler(new Vector3(0, 0, 180)));
                }
                break;

            case "Slime":
                AkSoundEngine.SetSwitch("Junk_type", "Toxic_waste", gameObject);
                AkSoundEngine.PostEvent("hull_impact", gameObject);
                foreach (ContactPoint2D slimeHit in col.contacts)
                {
                    Vector3 hitpoint = new Vector3(slimeHit.point.x, slimeHit.point.y, -10);
                    Instantiate(slimeDirt[Random.Range(0, slimeDirt.Length - 1)], hitpoint, Quaternion.identity);
                    Instantiate(slimeParticle, hitpoint, Quaternion.Euler(new Vector3(90, 0, 180)));

                }
                break;
        }
    }
}
