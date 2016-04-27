using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CircleCollider2D))]
public class CleaningHand : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dirt") { 
            other.gameObject.SetActive(false);
        }
    }
}
