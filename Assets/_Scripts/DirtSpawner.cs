using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DirtSpawner : MonoBehaviour
{

    public GameObject[] dirt;
    public float interval;
    public int distanceFromCam;
    private GameObject cam;
    public int dirtSpawnerWidth = 50;

    public int dirtPoolSize = 500;
    List<GameObject> dirtPool;

    private int i = 0;
    private int j = 0;

    void Awake()
    {
        dirtPool = new List<GameObject>();
        for(int i = 0; i < dirtPoolSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(dirt[Random.Range(0, dirt.Length)]);
            obj.SetActive(false);
            dirtPool.Add(obj);
        }

        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
        {
        while (true) {
            i = j % (dirtPoolSize);

            if (!dirtPool[i].activeInHierarchy)
            {
                dirtPool[i].transform.position = new Vector3(Random.Range(cam.transform.position.x - dirtSpawnerWidth, cam.transform.position.x + dirtSpawnerWidth),
            cam.transform.position.y - distanceFromCam, 10);
                dirtPool[i].SetActive(true);
                j++;
                
            }
            yield return new WaitForSeconds(interval);
        }
        
    }
}
