using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{

    public GameObject fruitPrefab;
    public int timeBetweenSpawns = 8;
    private int xlimit = 32;
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
        int signox = Random.Range(0,1);
        int signoy = Random.Range(0,1);
        if (signox == 0) {
            signox = -1;
        }
        if (signoy == 0) {
            signoy = -1;
        }
        Vector3 pos = new Vector3(x+0.5f*signox,y+0.5f*signoy,0);
        Instantiate(fruitPrefab, pos, Quaternion.identity);
    }
}
