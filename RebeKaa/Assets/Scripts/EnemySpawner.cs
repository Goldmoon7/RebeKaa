using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Numerics;

public class EnemySpawner : MonoBehaviour
{
    /*
    public GameObject enemyPrefab1; //lagarto
    public GameObject enemyPrefab2; //fenec
    public GameObject enemyPrefab3; //aguila
    */
    public List<GameObject> enemyPrefabs; //0 = largato, 1 = fenec, 2 = aguila
    public GameObject boss;
    public GameObject TextoOleada;
    public GameObject TextoTutorial;
    public GameObject EndMenu;
    public static int waveCounter; //contador de oleadas
    public int numeroOleada;
    public static int enemyCounter;
    private int xlimit = 32;
    private int ylimit = 14;

    void Start()
    {
        /*
        waveCounter = 0;
        numeroOleada = 1;
        enemyCounter = 0;
        Texto_y_Waves();
        */
        waveCounter = 0;
        StartCoroutine(GestorOleadas());
    }
    void Update(){
        /*
        if(enemyCounter == 0 && numeroOleada < 4){
           Texto_y_Waves();
        }
        */
        //Falta poner que hacer cuando se acaban todas las rondas
    }

    
    public void SpawnRandomEnemy(GameObject enemy) { //Spawnea un enemigo pasado como parámetro en uno de los bordes del mapa
        int x;
        x = UnityEngine.Random.Range(1,5);
        switch (x){
            case 1:
                GameObject e1 = Instantiate(enemy, new UnityEngine.Vector3(0,14,0), UnityEngine.Quaternion.identity);
                e1.transform.localScale = new UnityEngine.Vector3(3,3,3);
                break;
            case 2:
                GameObject e2 = Instantiate(enemy, new UnityEngine.Vector3(0,-14,0), UnityEngine.Quaternion.identity);
                e2.transform.localScale = new UnityEngine.Vector3(3,3,3);
                break;
            case 3:
                GameObject e3 = Instantiate(enemy, new UnityEngine.Vector3(33,0,0), UnityEngine.Quaternion.identity);
                e3.transform.localScale = new UnityEngine.Vector3(3,3,3);
                break;
            case 4:
                GameObject e4 = Instantiate(enemy, new UnityEngine.Vector3(-33,0,0), UnityEngine.Quaternion.identity);
                e4.transform.localScale = new UnityEngine.Vector3(3,3,3);
                break;
        }
    }
    /*

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
    */

    private void SpawnWave() {
        int i = waveCounter-1;
        enemyCounter = i + 4;
        for (int j = 0; j < enemyCounter; j++) {
            if(waveCounter != 6){
            int x = Random.Range(0,xlimit+1);
            int y = Random.Range(0,ylimit+1);
            if (j == 1 || j == 2) {
                x *= -1;
            }
            if (j == 2 || j == 3) {
                y *= -1;
            }
            if (waveCounter > 2) {
                i = 2;
            }
            int tipoEnemigo = Random.Range(0,i+1);
            UnityEngine.Vector3 pos = new UnityEngine.Vector3(x,y,0);
            GameObject enemy = Instantiate(enemyPrefabs[tipoEnemigo], pos, UnityEngine.Quaternion.identity);
            enemy.transform.localScale = new UnityEngine.Vector3(3,3,3);
            }
            else{
                enemyCounter = 1;
                UnityEngine.Vector3 pos = new UnityEngine.Vector3(25, 0, 0);
                GameObject enemy = Instantiate(boss, pos, UnityEngine.Quaternion.identity);
            }
        }
    }

    private IEnumerator GestorOleadas() {
        CartelTutorial(0);
        yield return new WaitUntil(() => Snake.frutasComidas == 1);
        CartelTutorial(1);
        //Time.fixedDeltaTime += 0.011f;
        waveCounter = 1;
        SiguienteOleada();
        //SpawnWave();
        yield return new WaitUntil(() => enemyCounter == 0);
        /*
        if (PlayerPrefs.GetInt("nivelActual") == 1) {
            PlayerPrefs.SetInt("nivelActual",2);
            EndGame();
        }
        */
        //Time.fixedDeltaTime += 0.011f;
        waveCounter++;
        SiguienteOleada();
        yield return new WaitUntil(() => enemyCounter == 0);
        /*
        if (PlayerPrefs.GetInt("nivelActual") == 2) {
            PlayerPrefs.SetInt("nivelActual",3);
           EndGame();
        }
        */
        //Time.fixedDeltaTime += 0.011f;
        waveCounter++;
        SiguienteOleada();
        yield return new WaitUntil(() => enemyCounter == 0);
        /*
        if (PlayerPrefs.GetInt("nivelActual") == 3) {
            PlayerPrefs.SetInt("nivelActual",4);
            EndGame();
        }
        */
        //Time.fixedDeltaTime += 0.011f;
        waveCounter++;
        CartelTutorial(2);
        SiguienteOleada();
        yield return new WaitUntil(() => enemyCounter == 0);
        /*
        if (PlayerPrefs.GetInt("nivelActual") == 4) {
            PlayerPrefs.SetInt("nivelActual",5);
            EndGame();
        }
        */
        //Time.fixedDeltaTime += 0.011f;
        waveCounter++;
        SiguienteOleada();
        yield return new WaitUntil(() => enemyCounter == 0);

        waveCounter++;
        CartelTutorial(3);
        SiguienteOleada();
        yield return new WaitUntil(() => enemyCounter == 0);
        EndGame();
        
    }

    public void EndGame()
    {
        EndMenu.SetActive(true);  // Mostrar el menú de pausa
        Time.timeScale = 0f;   // Detener el tiempo en el juego

    }

    public void SiguienteOleada() {
        Time.timeScale = 0f;
        StartCoroutine(CartelOleada());
        SpawnWave();
    }

    public IEnumerator CartelOleada() {
        Text txt = TextoOleada.GetComponent<Text>();
        txt.text = "OLEADA: " + waveCounter;
        TextoOleada.SetActive(true);
        Color original = Color.white;
        Color transp = original;
        transp.a = 0.25f;
        for (int i = 0; i < 5; i++) {
            txt.color = transp;
            yield return new WaitForSecondsRealtime(0.2f);
            txt.color = original;
            yield return new WaitForSecondsRealtime(0.2f);
        }
        TextoOleada.SetActive(false);
        Time.timeScale = 1f;
    }

    public IEnumerator CartelTutorial(int fase){
        Text txt = TextoTutorial.GetComponent<Text>();
        txt.color = Color.white;
        switch(fase){
            case 0:{
                txt.text = "Muévete utilizando las teclas A W S D o las teclas de dirección ←  ↑  ↓  →\nCome frutas para aumentar la longitud de Kaa y evita chocarte con las paredes.";
                break;
            }
            case 1:{
                txt.text = "Parece que vas a tener que derrotar a esos enemigos, para ello, necesitarás una longitud mínima de 3 para derrotar a los lagartos y una longitud mínima de 5 para derrotar a los fennecs.";
                break;
            }
            case 2:{
                txt.text = "¡Uh Oh! Enemigos más peligrosos se acercan. Deberás beber la famosa GreenKaa que va apareciendo y además necesitas tener una longitud mínima de 10 para derrotarlas. Ten cuidado porque si tienes longitud 10 pero no has bebido el GreenKaa, las águilas te matarán.";
                break;
            }
            case 3:{
                txt.text = "¡Por fin has alcanzado al mequetrefe que se llevó a Rebe! Esquiva los proyectiles de fuego para no recibir daño y atácalo golpeándole el cuerpo para derrotarlo";
                break;
            }
        }
        TextoTutorial.SetActive(true);
        yield return new WaitUntil(() => Input.anyKeyDown);
        TextoTutorial.SetActive(false);
    }

    private IEnumerator GestionarTiempo() {
        //0.125
        //0.07
        //Time.fixedDeltaTime += 0.011f;
        yield break;
    }
}
