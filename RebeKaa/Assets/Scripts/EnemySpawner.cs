using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1; //lagarto
    public GameObject enemyPrefab2; //fenec
    public GameObject enemyPrefab3; //aguila
    public GameObject TextoOleada;
    public int waveCounter; //contador de oleadas
    public int numeroOleada;
    public static int enemyCounter;

    void Start()
    {
        waveCounter = 0;
        numeroOleada = 1;
        enemyCounter = 0;
        Texto_y_Waves();
    }
    void Update(){
        if(enemyCounter == 0 && numeroOleada < 4){
           Texto_y_Waves();
        }
        //Falta poner que hacer cuando se acaban todas las rondas
    }
    public void SpawnRandomEnemy(GameObject enemy) { //Spawnea un enemigo pasado como parámetro en uno de los bordes del mapa
        int x;
        x = UnityEngine.Random.Range(1,5);
        switch (x){
            case 1:
                GameObject e1 = Instantiate(enemy, new Vector3(0,14,0), Quaternion.identity);
                e1.transform.localScale = new Vector3(3,3,3);
                break;
            case 2:
                GameObject e2 = Instantiate(enemy, new Vector3(0,-14,0), Quaternion.identity);
                e2.transform.localScale = new Vector3(3,3,3);
                break;
            case 3:
                GameObject e3 = Instantiate(enemy, new Vector3(33,0,0), Quaternion.identity);
                e3.transform.localScale = new Vector3(3,3,3);
                break;
            case 4:
                GameObject e4 = Instantiate(enemy, new Vector3(-33,0,0), Quaternion.identity);
                e4.transform.localScale = new Vector3(3,3,3);
                break;
        }
    }

    private void SpawnWave(){ //Spawneo de waves en orden en función del contador
            if(waveCounter == 0){
                enemyCounter = 4;
                waveCounter++;
                wave1();
            }
            else if(waveCounter == 1){
                enemyCounter = 4;
                waveCounter++;
                wave2();
            }
            else if(waveCounter == 2){
                enemyCounter = 6;
                waveCounter++;
                wave3();
                CancelInvoke();
            }
    }

    //WAVES
    private void wave1(){
        int x1,y1,x2,y2,x3,y3;
        x1 = -13; y1 = 0;
        x2 = 13; y2 = 0;
        x3 = 0; y3 = 10;
        Vector3 pos1 = new Vector3(x1,y1,0);
        Vector3 pos2 = new Vector3(x2,y2,0);
        Vector3 pos3 = new Vector3(x3,y3,0);
        GameObject e1 = Instantiate(enemyPrefab1, pos1, Quaternion.identity);
        e1.transform.localScale = new Vector3(3,3,3);
        GameObject e2 = Instantiate(enemyPrefab1, pos2, Quaternion.identity);
        e2.transform.localScale = new Vector3(3,3,3);
        GameObject e3 = Instantiate(enemyPrefab1, pos3, Quaternion.identity);
        e3.transform.localScale = new Vector3(3,3,3);
        SpawnRandomEnemy(enemyPrefab1);
    }
    private void wave2(){
        int x1,y1,x2,y2;
        x1 = -13; y1 = 0;
        x2 = 13; y2 = 0;
        Vector3 pos1 = new Vector3(x1,y1,0);
        Vector3 pos2 = new Vector3(x2,y2,0);
        GameObject e1 = Instantiate(enemyPrefab2, pos1, Quaternion.identity);
        e1.transform.localScale = new Vector3(3,3,3);
        GameObject e2 = Instantiate(enemyPrefab2, pos2, Quaternion.identity);
        e2.transform.localScale = new Vector3(3,3,3);
        SpawnRandomEnemy(enemyPrefab1);
        SpawnRandomEnemy(enemyPrefab1);
    }

    private void wave3(){
        int x1,y1,x2,y2,x3,y3;
        x1 = -13; y1 = 0;
        x2 = 13; y2 = 0;
        x3 = 0; y3 = 10;
        Vector3 pos1 = new Vector3(x1,y1,0);
        Vector3 pos2 = new Vector3(x2,y2,0);
        Vector3 pos3 = new Vector3(x3,y3,0);
        GameObject e1 = Instantiate(enemyPrefab3, pos1, Quaternion.identity);
        e1.transform.localScale = new Vector3(3,3,3);
        GameObject e2 = Instantiate(enemyPrefab3, pos2, Quaternion.identity);
        e2.transform.localScale = new Vector3(3,3,3);
        GameObject e3 = Instantiate(enemyPrefab2, pos3, Quaternion.identity);
        e3.transform.localScale = new Vector3(3,3,3);
        SpawnRandomEnemy(enemyPrefab1);
        SpawnRandomEnemy(enemyPrefab1);
        SpawnRandomEnemy(enemyPrefab1);
    }
    //FIN DE WAVES

    // private bool distanceLessThan(Vector3 pos1, int x2, int y2){ 
    //     //Por si se quiere spawnear enemigos alejados del jugador
    //     float x1 = pos1.x;
    //     float y1 = pos1.y;
    //     return (Math.Sqrt(Math.Pow(x2-x1,2) +
    //                 Math.Pow(y2-y1,2)) < 8) ? true : false;
    // }

    private void Texto_y_Waves(){
        //TextoOleada.GetComponent<Text>().text = "OLEADA " + numeroOleada;
        numeroOleada++;
        //Time.timeScale = 0f;
        //TextoOleada.SetActive(true);
        //Thread.Sleep(3000);
        //TextoOleada.SetActive(false);
       // Time.timeScale = 1f;
        SpawnWave();
    }
}
