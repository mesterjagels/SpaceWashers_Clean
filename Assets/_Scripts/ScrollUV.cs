using UnityEngine;
using System.Collections;

public class ScrollUV : MonoBehaviour {

    MeshRenderer mr;
    Material mat;
    Vector2 offset;
    SpaceshipController spaceship;
    int uvMoveSpeed = 20;
    [Header("Change parallax effect. Higher means slower background movement")]
    public float parralax;

	// Use this for initialization
	void Start () {
        mr = GetComponent<MeshRenderer>();
        mat = mr.materials[0];
        offset = mat.mainTextureOffset;
        spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<SpaceshipController>();
    }

    // Update is called once per frame
    void Update () {
        offset.x += spaceship.horizontalSpeed/ uvMoveSpeed / parralax;
        offset.y += spaceship.verticalSpeed / uvMoveSpeed / parralax;
            
        
        mat.mainTextureOffset = offset;
	}
}
