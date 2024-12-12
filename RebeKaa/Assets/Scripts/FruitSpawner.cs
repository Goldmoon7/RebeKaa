using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{

    public GameObject fruitPrefab;
    public int timeBetweenSpawns = 8;
    private int xlimit = 34;
    private int ylimit = 14;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnFruit",0f,timeBetweenSpawns);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFruit() {
        int x = Random.Range(-xlimit,xlimit);
        int y = Random.Range(-ylimit,ylimit);
        Vector3 pos = new Vector3(x,y,0);
        Instantiate(fruitPrefab, pos, Quaternion.identity);
    }
}
