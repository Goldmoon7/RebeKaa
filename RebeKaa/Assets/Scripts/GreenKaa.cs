using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenKaa : MonoBehaviour
{

    public GameObject greenKaaPrefab;
    private int xlimit = 32;
    private int ylimit = 14;

    static public bool fly = false; //Indica si Kaa esta volando o no

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnGreenKaa",10f,10);
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
        GameObject newSegment = Instantiate(greenKaaPrefab, pos, Quaternion.identity);
    }
}

