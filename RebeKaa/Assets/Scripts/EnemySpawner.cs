using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{
    /*
    public GameObject enemyPrefab1; //lagarto
    public GameObject enemyPrefab2; //fenec
    public GameObject enemyPrefab3; //aguila
    */
    public List<GameObject> enemyPrefabs; //0 = largato, 1 = fenec, 2 = aguila
    public List<Sprite> cartelesOleada;
    public GameObject cartelOleada, textovidasboss;
    public GameObject boss;
    public GameObject TextoOleada;
    public GameObject EndMenu;
    public GameObject texto_tutorial;
    public GameObject panelTutorial;
    public GameObject panelVineta;
    public GameObject textoVineta;
    public List<Sprite> imagenVinetas;
    public GameObject ImagenVineta;
    public static int waveCounter; //contador de oleadas
    public int numeroOleada;
    public static int enemyCounter;
    private int xlimit = 32;
    private int ylimit = 14;
    public static int bossMuerto = 0;
    private bool modificarVelocidad;
    private bool fin;
    private int minutos;
    private int segundos;

    void Start()
    {
        /*
        waveCounter = 0;
        numeroOleada = 1;
        enemyCounter = 0;
        Texto_y_Waves();
        */
        /*
        texto_tutorial = GameObject.Find("Tutorial/Panel/Texto_tutorial");
        panel = GameObject.Find("Tutorial/Panel");
        */
        waveCounter = 0;
        cartelOleada.SetActive(false);
        panelTutorial.SetActive(false);
        panelVineta.SetActive(false);
        //Time.fixedDeltaTime = 0.125f;
        modificarVelocidad = false;
        fin = false;
        Time.fixedDeltaTime = 0.125f;
        StartCoroutine(GestorOleadas());
        /*
        if (PlayerPrefs.GetInt("nivelActual") < 6) {
            Time.fixedDeltaTime = 0.125f;
            StartCoroutine(GestorOleadas());
        } else if (PlayerPrefs.GetInt("nivelActual") == 6) {
            Time.fixedDeltaTime = 0.07f;
            StartCoroutine(Boss());
        } else {
            Time.fixedDeltaTime = 0.07f;
            StartCoroutine(End());
        }
        */
        
    }
    void Update(){
    }

    void FixedUpdate() {
        if (modificarVelocidad) {
            Time.fixedDeltaTime -= 0.011f;
            modificarVelocidad = false;
        }
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
        if (waveCounter != 6) {
            int i = waveCounter-1;
            enemyCounter = i + 4;
            for (int j = 0; j < enemyCounter; j++) {
                if(waveCounter != 6){
                    int x = UnityEngine.Random.Range(0,xlimit+1);
                    int y = UnityEngine.Random.Range(0,ylimit+1);
                    if (j == 1 || j == 2) {
                        x *= -1;
                    }
                    if (j == 2 || j == 3) {
                        y *= -1;
                    }
                    if (waveCounter > 2) {
                        i = 2;
                    }
                    int tipoEnemigo = UnityEngine.Random.Range(0,i+1);
                    UnityEngine.Vector3 pos = new UnityEngine.Vector3(x,y,0);
                    GameObject enemy = Instantiate(enemyPrefabs[tipoEnemigo], pos, UnityEngine.Quaternion.identity);
                    enemy.transform.localScale = new UnityEngine.Vector3(3,3,3);
                    }
                else {
                    enemyCounter = 1;
                    UnityEngine.Vector3 pos = new UnityEngine.Vector3(25, 0, 0);
                    GameObject enemy = Instantiate(boss, pos, UnityEngine.Quaternion.identity);
                }
            }
        } else {
            enemyCounter = 1;
            UnityEngine.Vector3 pos = new UnityEngine.Vector3(25, 0, 0);
            GameObject enemy = Instantiate(boss, pos, UnityEngine.Quaternion.identity);
        }
    }

    private IEnumerator GestorOleadas() {
        if (PlayerPrefs.GetInt("nivelActual") == 1) {
            StartCoroutine(CartelTutorial(0));
        }
        yield return new WaitUntil(() => Snake.frutasComidas == 1);
        if (PlayerPrefs.GetInt("nivelActual") == 1) {
            fin = true;
            StartCoroutine(CartelTutorial(1));
        }
        yield return new WaitUntil(() => fin == false);
        modificarVelocidad = true;
        waveCounter = 1;
        SiguienteOleada(); //oleada 1
        yield return new WaitUntil(() => enemyCounter == 0);
        if (PlayerPrefs.GetInt("nivelActual") == 1) {
            fin = true;
            PlayerPrefs.SetInt("nivelActual",2);
            EndGame();
        }
        yield return new WaitUntil(() => fin == false);
        modificarVelocidad = true;
        waveCounter++;
        SiguienteOleada(); // oleada 2
        yield return new WaitUntil(() => enemyCounter == 0);
        if (PlayerPrefs.GetInt("nivelActual") == 2) {
            fin = true;
            PlayerPrefs.SetInt("nivelActual",3);
            EndGame();
        }
        yield return new WaitUntil(() => fin == false);
        if (PlayerPrefs.GetInt("nivelActual") == 3) {
            fin = true;
            StartCoroutine(CartelTutorial(2));
        }
        yield return new WaitUntil(() => fin == false);
        modificarVelocidad = true;
        waveCounter++;
        SiguienteOleada(); //oleada 3
        yield return new WaitUntil(() => enemyCounter == 0);
        if (PlayerPrefs.GetInt("nivelActual") == 3) {
            fin = true;
            PlayerPrefs.SetInt("nivelActual",4);
            EndGame();
        }
        yield return new WaitUntil(() => fin == false);
        modificarVelocidad = true;
        waveCounter++;
        SiguienteOleada(); // oleada 4
        yield return new WaitUntil(() => enemyCounter == 0);
        if (PlayerPrefs.GetInt("nivelActual") == 4) {
            fin = true;
            PlayerPrefs.SetInt("nivelActual",5);
            EndGame();
        }
        yield return new WaitUntil(() => fin == false);
        modificarVelocidad = true;
        waveCounter++;
        SiguienteOleada(); // oleada 5
        yield return new WaitUntil(() => enemyCounter == 0);
        fin = true;
        StartCoroutine(CartelVineta(0));
        yield return new WaitUntil(() => fin == false);
        fin = true;
        StartCoroutine(CartelTutorial(3));
        yield return new WaitUntil(() => fin == false);
        waveCounter = 6;
        SiguienteOleada(); // oleada 6 (boss)
        textovidasboss.SetActive(true);
        yield return new WaitUntil(() => enemyCounter == 0);
        fin = true;
        StartCoroutine(CartelVineta(1));
        yield return new WaitUntil(() => fin == false);
        fin = true;
        EndGame();
        yield return new WaitUntil(() => fin == false);
    }

    public IEnumerator Boss() {
        if (PlayerPrefs.GetInt("nivelActual") == 6) {
            fin = true;
            StartCoroutine(CartelTutorial(3));
        }
        yield return new WaitUntil(() => fin == false);
        waveCounter = 6;
        SiguienteOleada(); // oleada 6 (boss)
        textovidasboss.SetActive(true);
        yield return new WaitUntil(() => enemyCounter == 0);
        PlayerPrefs.SetInt("nivelActual",7);
        SceneManager.LoadScene("ViñetasP3");
        /*
        fin = true;
        EndGame();
        yield return new WaitUntil(() => fin == false);
        */
    }

    public IEnumerator End() {
        fin = true;
        EndGame();
        yield return new WaitUntil(() => fin == false);
    }

    public void EndGame()
    {
        StopAllCoroutines();
        Time.timeScale = 0f;   // Detener el tiempo en el juego
        EndMenu.SetActive(true);  // Mostrar el menú de pausa
        UpdateStatsText();
        UpdateTotalPoints();
    }

    public void SiguienteOleada() {
        Time.timeScale = 0f;
        if(waveCounter < 6) {
            StartCoroutine(CartelOleada());
        }
        SpawnWave();
    }

    public IEnumerator CartelOleada() {
        SpriteRenderer sr = cartelOleada.GetComponent<SpriteRenderer>();
        sr.sprite = cartelesOleada[waveCounter-1];
        cartelOleada.SetActive(true);
        Color original = Color.white;
        Color transp = original;
        transp.a = 0.25f;
        for (int i = 0; i < 5; i++) {
            sr.color = transp;
            yield return new WaitForSecondsRealtime(0.2f);
            sr.color = original;
            yield return new WaitForSecondsRealtime(0.2f);
        }
        cartelOleada.SetActive(false);
        Time.timeScale = 1f;
    }

    public IEnumerator CartelTutorial(int fase){
        Time.timeScale = 0f;
        switch(fase){
            case 0:{
                texto_tutorial.GetComponent<Text>().text = "Muévete utilizando las teclas A W S D o las teclas de dirección ←  ↑  ↓  →\nCome frutas para aumentar la longitud de Kaa y evita chocarte con las paredes.";
                break;
            }
            case 1:{
                texto_tutorial.GetComponent<Text>().text = "Parece que vas a tener que derrotar a esos enemigos, para ello, necesitarás una longitud mínima de 3 para derrotar a los lagartos y una longitud mínima de 5 para derrotar a los fennecs.";
                break;
            }
            case 2:{
                texto_tutorial.GetComponent<Text>().text = "¡Uh Oh! Enemigos más peligrosos se acercan. Deberás beber la famosa GreenKaa que va apareciendo y además necesitas tener una longitud mínima de 10 para derrotarlas. Ten cuidado porque si tienes longitud 10 pero no has bebido el GreenKaa, las águilas te matarán.";
                break;
            }
            case 3:{
                texto_tutorial.GetComponent<Text>().text = "¡Por fin has alcanzado al mequetrefe que se llevó a Rebe! Esquiva los proyectiles de fuego para no recibir daño y atácalo golpeándole el cuerpo para derrotarlo, recuerda tener minimo 20 de longitud o el ataque habra sido en vano";
                break;
            }
        }
        panelTutorial.SetActive(true);
        yield return new WaitUntil(() => Input.anyKeyDown);
        Time.timeScale = 1f;
        panelTutorial.SetActive(false);
        fin = false;
    }

    public IEnumerator CartelVineta(int fase) {
        Time.timeScale = 0f;
        switch(fase){
            case 0:{
                textoVineta.GetComponent<Text>().text = "Kaa consigue derrotar a los animales que se interpusieron en su camino hacia Rebe y sigue avanzando por el desierto, hasta que se topa con una cueva de aspecto sospechoso en la que se vislumbran dos ojos que la miran fijamente y los gritos de Rebe pidiendo ayuda!";
                break;
            }
            case 1:{
                textoVineta.GetComponent<Text>().text = "Tras derrotar al malvado Monstruo del desierto, Kaa y Rebe se dirigen a casa, felices de volver a estar juntas.";
                break;
            }
        }
        panelVineta.SetActive(true);
        ImagenVineta.SetActive(false);
        yield return new WaitUntil(() => Input.anyKeyDown);
        textoVineta.SetActive(false);
        SpriteRenderer sr = ImagenVineta.GetComponent<SpriteRenderer>();
        sr.sprite = imagenVinetas[fase];
        ImagenVineta.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        panelVineta.SetActive(false);
        Time.timeScale = 1f;
        fin = false;
    }
    private void UpdateStatsText() {
        GameObject go = GameObject.FindGameObjectWithTag("Stats"); // Busca el objeto con la etiqueta "Stats"
        float tempAct = Time.time -Snake.tiempo;
        minutos= Mathf.FloorToInt(tempAct / 60f);
        segundos = Mathf.FloorToInt(tempAct % 60);
        go.GetComponent<Text>().text = "Frutas Comidas.................... " + Snake.frutasComidas + 
        "\n\nEnemigos Derrotados............... " + Snake.enemigosDerrotados + 
        "\n\n\tLagartos Derrotados.................... "+ Snake.nlagartosmuertos +
        "\n\n\tFenecs Derrotados.................... "+ Snake.nfenecmuertos +
        "\n\n\tAguilas Derrotadas.................... " + Snake.naguilasmuertas +
        "\n\n\tBoss Derrotado......................... " + bossMuerto +
        "\n\nGreenKaa Bebidos.................. " + Snake.greenkaaBebidas + 
        "\n\nLongitud.......................... " + Snake.longitud +
        "\n\nTiempo............................ " + minutos + ":" + segundos +
        "\n\nVidas Restantes............................ " + Snake.nvidas; // Actualiza el texto
    }

    private void UpdateTotalPoints(){
        GameObject go = GameObject.FindGameObjectWithTag("TOTAL");
        double total;
        total = Snake.frutasComidas * 5 + Snake.nlagartosmuertos * 10 + Snake.nfenecmuertos*20 + Snake.naguilasmuertas*40 + 
                bossMuerto*100 + Snake.longitud + (segundos + minutos*60f) * (-0.2) + Snake.nvidas*10;
        go.GetComponent<Text>().text = "TOTAL: " + total;

    }
}
