using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DirtSpawner : MonoBehaviour
{
    public Vector3 spawnPos1;
    public Vector3 spawnPos2;
    public Vector3 spawnPos3;
    public Vector3 spawnPos4;
    public Vector3 spawnPos5;


    public GameObject[] dirt;
    public int intervalMin;
    public int intervalMax;
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
        for (int i = 0; i < dirtPoolSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(dirt[Random.Range(0, dirt.Length)]);
            obj.SetActive(false);
            dirtPool.Add(obj);
        }

        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Start()
    {
        StartCoroutine(SpawnPos1());
        StartCoroutine(SpawnPos2());
        StartCoroutine(SpawnPos3());
        StartCoroutine(SpawnPos4());
        StartCoroutine(SpawnPos5());

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(spawnPos1, 0.2f);
        Gizmos.DrawSphere(spawnPos2, 0.2f);
        Gizmos.DrawSphere(spawnPos3, 0.2f);
        Gizmos.DrawSphere(spawnPos4, 0.2f);
        Gizmos.DrawSphere(spawnPos5, 0.2f);
    }

    void Update()
    {
        i = j % (dirtPoolSize);
    }

    IEnumerator SpawnPos1()
    {
        while (true)
        {
            if (!dirtPool[i].activeInHierarchy)
            {
                dirtPool[i].transform.position = spawnPos1;
                dirtPool[i].SetActive(true);
                j++;
            }
            yield return new WaitForSeconds(Random.Range(intervalMin, intervalMax));
        }
    }
    IEnumerator SpawnPos2()
    {
        while (true)
        {
            if (!dirtPool[i].activeInHierarchy)
            {
                dirtPool[i].transform.position = spawnPos2;
                dirtPool[i].SetActive(true);
                j++;
            }
            yield return new WaitForSeconds(Random.Range(intervalMin, intervalMax));
        }
    }
    IEnumerator SpawnPos3()
    {
        while (true)
        {
            if (!dirtPool[i].activeInHierarchy)
            {
                dirtPool[i].transform.position = spawnPos3;
                dirtPool[i].SetActive(true);
                j++;
            }
            yield return new WaitForSeconds(Random.Range(intervalMin, intervalMax));
        }
    }
    IEnumerator SpawnPos4()
    {
        while (true)
        {
            if (!dirtPool[i].activeInHierarchy)
            {
                dirtPool[i].transform.position = spawnPos4;
                dirtPool[i].SetActive(true);
                j++;
            }
            yield return new WaitForSeconds(Random.Range(intervalMin, intervalMax));
        }
    }
    IEnumerator SpawnPos5()
    {
        while (true)
        {
            if (!dirtPool[i].activeInHierarchy)
            {
                dirtPool[i].transform.position = spawnPos5;
                dirtPool[i].SetActive(true);
                j++;
            }
            yield return new WaitForSeconds(Random.Range(intervalMin, intervalMax));
        }
    }
}
