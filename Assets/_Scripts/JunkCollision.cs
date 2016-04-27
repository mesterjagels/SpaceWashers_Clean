using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolygonCollider2D))]
public class JunkCollision : MonoBehaviour {

    [Header("Drag in the appropiate dirt gameobjects")]
    public GameObject[] pizzaDirt;
    public GameObject pizzaParticle;
    public GameObject[] slimeDirt;
    public GameObject slimeParticle;
    public GameObject[] dustDirt;
    public GameObject dustParticle;

    void Start()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Pizza":
                Debug.Log("Pizza hit");
                foreach (ContactPoint2D pizzaHit in col.contacts)
                {
                    Vector2 hitpoint = pizzaHit.point;
                    Instantiate(pizzaDirt[Random.Range(0, pizzaDirt.Length)], hitpoint, Quaternion.identity);
                    Instantiate(pizzaParticle, hitpoint, Quaternion.identity);
                }
                break;

            case "Slime":
                Debug.Log("Slime hit");
                foreach (ContactPoint2D slimeHit in col.contacts)
                {
                    Vector2 hitpoint = slimeHit.point;
                    Instantiate(slimeDirt[Random.Range(0, pizzaDirt.Length)], hitpoint, Quaternion.identity);
                    Instantiate(slimeParticle, hitpoint, Quaternion.identity);

                }
                break;
        }
    }
}
