using UnityEngine;
using System.Collections;

public class JunkShredder : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
