using UnityEngine;
using System.Collections;

public class JunkShredder : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("I should destroy shit!");
        Destroy(other.gameObject);
    }
}
