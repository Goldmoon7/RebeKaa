using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenKaa : MonoBehaviour
{

    public GameObject greenKaaPrefab;
    private int xlimit = 32;
    private int ylimit = 14;
    private int timeBetweenSpawns;

    static public bool fly = false; //Indica si Kaa esta volando o no

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenSpawns = 10;
        StartCoroutine(RetrasarComienzo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnGreenKaa() {
        int x = Random.Range(-xlimit,xlimit);
        int y = Random.Range(-ylimit,ylimit);
        int signox = Random.Range(0,1);
        if (signox == 0) {
            signox = -1;
        }
        Vector3 pos = new Vector3(x+0.5f*signox,y,0);
        GameObject greenKaa = Instantiate(greenKaaPrefab, pos, Quaternion.identity);
    }

    private IEnumerator RetrasarComienzo() {
        yield return new WaitUntil(() => EnemySpawner.waveCounter > 2);
        InvokeRepeating("spawnGreenKaa",5f,timeBetweenSpawns);
    }
}

