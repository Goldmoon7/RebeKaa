using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{

    public GameObject fruitPrefab;
    public List<GameObject> fruitPrefabs;
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
        int tipoFruta; /* = Random.Range(0,fruitPrefabs.Count);*/
        Vector3 pos = new Vector3(x+0.5f*signox,y+0.5f*signoy,0);
        double p = Random.Range(0,1f);
        if (p <= 0.1) {
            tipoFruta = 2;
        } else if (p <= 0.7) {
            tipoFruta = 0;
        } else {
            tipoFruta = 1;
        }
        Instantiate(fruitPrefabs[tipoFruta], pos, Quaternion.identity);
    }
}
