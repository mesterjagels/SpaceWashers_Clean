using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CircleCollider2D))]
public class CleaningHand : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Dirt cleaned");
        other.gameObject.SetActive(false);

        if (other.tag == "Dirt") { 
        }
    }
}
