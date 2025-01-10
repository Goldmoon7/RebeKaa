using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine.Video;

public class Snake : MonoBehaviour
{

    private Vector3 position;
    private int rotationTrigger;
    private int makeBiggerTrigger;
    public GameObject bodyPrefab, tail, heartPrefab;
    public GameObject EndMenu;
    public Sprite conVida, sinVida;
    [SerializeField] public AudioClip comerfruta;
    [SerializeField] public AudioClip bebergreenkaa;
    [SerializeField] public AudioClip enemigoderrotado;
    [SerializeField] public AudioClip dañorecibido;
    List<GameObject> body;
    public static GameObject[] hearts;
    private int currentHorizDir;
    private int currentVertDir;
    private int SCORE;
    private int VIDAS;
    public static int nvidas;
    public static int frutasComidas;
    public static int greenkaaBebidas;
    public static int nlagartosmuertos;
    public static int nfenecmuertos;
    public static int naguilasmuertas;
    static public int longitud;
    static public float tiempo;
    static public int enemigosDerrotados;
    private int greenKaaUsadas;
    private int nivelAguila = 10;
    private int nivelFenec = 5;
    private int nivelCamaleon = 3;
    public Sprite kaaAlas; // El sprite que se mostrará al entrar en contacto
    public Sprite kaaNormal;
    private SpriteRenderer spriteRenderer;
    static public bool fly = false; //Indica si Kaa esta volando o no
    public static int makeSmallerTrigger;
    //esta hay que eliminarla cuando ya no hagamos pruebas
    private int cheat = 0;
    private int verticalBlock;
    private int horizontalBlock;
    private bool detectarColisiones;
    public static bool colisionesCuerpo;
    //private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.transform.position = new Vector3(0.5f,0.5f,0);
        tail.transform.position = new Vector3(this.transform.position.x-1,0.5f,0);
        body = new List<GameObject>();
        body.Add(this.gameObject);
        body.Add(tail);
        currentHorizDir = 0;
        currentVertDir = 0;
        SCORE = 0;
        frutasComidas = 0;
        UpdateFruitsText(); //para que se muestre desde el inicio
        longitud = 0;
        enemigosDerrotados = 0;
        UpdateEnemiesText(); //para que se muestre desde el inicio
        tiempo = Time.time;
        VIDAS = 3;
        nvidas = 3;
        hearts = new GameObject[3];
        horizontalBlock = 0;
        verticalBlock = 0;
        detectarColisiones = true;
        colisionesCuerpo = true;
        CreateLives();
    }

    // Update is called once per frame

    void Update() {
        int horizontalDir = Math.Sign(Input.GetAxis("Horizontal"));
        int verticalDir = Math.Sign(Input.GetAxis("Vertical"));

        //unidad de movimiento
        float magnitude = 1;

        if (horizontalDir != 0 && currentHorizDir == 0) {
            if (horizontalDir != horizontalBlock) {
                position = magnitude*horizontalDir*Vector3.right;
                rotationTrigger = 1;
                currentHorizDir = horizontalDir;
                currentVertDir = 0;
                if (horizontalBlock != 0) {
                    horizontalBlock = 0;
                }
            }
        } else if (verticalDir != 0 && currentVertDir == 0) {
            if (verticalDir != verticalBlock) {
                position = magnitude*verticalDir*Vector3.up;
                rotationTrigger = 1;
                currentVertDir = verticalDir;
                currentHorizDir = 0;
                if (verticalBlock != 0) {
                    verticalBlock = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && makeBiggerTrigger == 0) {
            makeBiggerTrigger = 1;
        }

        if (Input.GetKeyDown(KeyCode.L) && makeSmallerTrigger == 0) {
            makeSmallerTrigger = 1;
            cheat = 1;
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            GestionarColision();
        }

        if(VIDAS > 0)
            UpdateTiempoText();
    }
    void FixedUpdate()
    {
        if (makeBiggerTrigger == 1) {
            longitud++;
            MakeBigger();
            UpdateLongitudText();
        }

        if (makeSmallerTrigger == 1) {
            if(longitud > 0){
                longitud--;
            }
            if (cheat == 1) {
                MakeSmaller(1);
                cheat = 0;
            } else {
                MakeSmaller(0);
            }
            UpdateLongitudText();
        }

        Vector3 tailPosBefore = tail.transform.position;
        for(int i = body.Count - 1; i > 0; i--) {
            body[i].transform.position = body[i-1].transform.position;
        }
        Vector3 tailPosAfter = tail.transform.position;
        
        Vector3 newDirection = tailPosBefore - tailPosAfter;

        if (newDirection.x != 0) {
            tail.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
            tail.transform.localScale = new Vector3(1,-Math.Sign(newDirection.x),1);
        } else if (newDirection.y != 0) {
            tail.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
            tail.transform.localScale = new Vector3(1,-Math.Sign(newDirection.y),1);
        }

        //rb.MovePosition(transform.position += position);
        this.transform.position += position;

        if(rotationTrigger != 0) {
            if (currentHorizDir != 0) {
                //rb.MoveRotation(Quaternion.Euler(new Vector3(0,0,90)));
                transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
                transform.localScale = new Vector3(1,currentHorizDir,0);
            } else {
                //rb.MoveRotation(Quaternion.Euler(new Vector3(0,0,-180)));
                transform.rotation = Quaternion.Euler(new Vector3(0,0,-180));
                transform.localScale = new Vector3(1,currentVertDir,0);
            }
            rotationTrigger = 0;
        }
    }

    public void MakeBigger() {
        Vector3 pos = body[body.Count-1].transform.position;
        GameObject newSegment = Instantiate(bodyPrefab,pos,Quaternion.identity);
        newSegment.transform.localScale = new Vector3(1,1,0);
        body.Insert(body.Count-1,newSegment);
        makeBiggerTrigger = 0;
    }

    public void MakeSmaller(int i) {
        if (body.Count > 2) {
            GameObject segment = body.ElementAt<GameObject>(body.Count-2);
            body.RemoveAt(body.Count-2);
            Destroy(segment);
        }

        Debug.Log("VIDAS: " + VIDAS);
        if(i == 0 && VIDAS >= 0 && VIDAS < 3) {
            //Debug.Log("VIDAS: " + VIDAS);
            SpriteRenderer sr = hearts[VIDAS].GetComponent<SpriteRenderer>();
            sr.sprite = sinVida;
            //StartCoroutine(Blink(sr));
        }
        
        makeSmallerTrigger = 0;
    }


    //hacer el metodo auxiliar
    public void Rotate(GameObject go, Vector3 direction) {
        if (currentHorizDir != 0) {
                transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
                transform.localScale = new Vector3(1,-currentHorizDir,1);
            } else {
                transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
                transform.localScale = new Vector3(1,-currentVertDir,1);
            }
    }

    private void OnTriggerEnter2D (Collider2D collider) {
        if (detectarColisiones) {
            if (collider.gameObject.CompareTag("Body") && longitud>1) {
                if(ModoInfinito.noMorir == false){
                    VIDAS--;
                    nvidas--;
                    ControlAudio.Instance.EjecutarSonidoDaño(dañorecibido);
                    ShouldIDie();
                    GestionarColision();
                }
            } else if (collider.gameObject.CompareTag("Fruit")) {
                ControlAudio.Instance.EjecutarSonidoFruta(comerfruta);
                Destroy(collider.gameObject);
                makeBiggerTrigger = 1;
                frutasComidas++;
                UpdateFruitsText(); //actualiza frutasComidas
            } else if (collider.gameObject.CompareTag("GreenKaa")) {
                greenkaaBebidas++;
                ControlAudio.Instance.EjecutarSonidoGreenKaa(bebergreenkaa);
                Destroy(collider.gameObject);
                greenKaaUsadas++;
                ChangeToSpriteWithReset(1,kaaAlas,kaaNormal,20f);
            } else if (collider.gameObject.CompareTag("Lagarto")) {
                Enemy1 enemy = collider.GetComponent<Enemy1>();
                if (longitud < nivelCamaleon) {
                    if(ModoInfinito.noMorir == false){
                        VIDAS--;
                        nvidas--;
                        ControlAudio.Instance.EjecutarSonidoDaño(dañorecibido);
                        Debug.Log("vidas en lagarto = " + VIDAS);
                        ShouldIDie();
                    }
                } else {
                    collider.isTrigger = false;
                    ControlAudio.Instance.EjecutarSonidoEnemigo(enemigoderrotado);
                    enemy.SetMuerteLagarto(true);
                    //Destroy(collider.gameObject);
                    EnemySpawner.enemyCounter--;
                    SCORE = SCORE + 1;
                    //UpdateScoreText();
                    enemigosDerrotados++;
                    nlagartosmuertos++;
                    UpdateEnemiesText(); //actualiza enemigosDerrotados
                }
            } else if (collider.gameObject.CompareTag("Fenec")) {
                Enemy3 enemy = collider.GetComponent<Enemy3>();
                if (longitud < nivelFenec) {
                    if(ModoInfinito.noMorir == false){
                        VIDAS--;
                        nvidas--;
                        ControlAudio.Instance.EjecutarSonidoDaño(dañorecibido);
                        ShouldIDie();
                    }
                } else {
                    ControlAudio.Instance.EjecutarSonidoEnemigo(enemigoderrotado);
                    enemy.SetMuerteFenec(true);
                    //Destroy(collider.gameObject);
                    EnemySpawner.enemyCounter--;
                    SCORE = SCORE + 3;
                    //UpdateScoreText();
                    enemigosDerrotados++;
                    nfenecmuertos++;
                    UpdateEnemiesText(); //actualiza enemigosDerrotados
                }
            } else if (collider.gameObject.CompareTag("Aguila")) {
                Enemy2 enemy = collider.GetComponent<Enemy2>();
                if (fly == false || longitud < nivelAguila) {
                    if(ModoInfinito.noMorir == false){
                        VIDAS--;
                        nvidas--;
                        ControlAudio.Instance.EjecutarSonidoDaño(dañorecibido);
                        ShouldIDie();
                    }
                } else {
                    ControlAudio.Instance.EjecutarSonidoEnemigo(enemigoderrotado);
                    enemy.SetMuerteAguila(true);
                    //Destroy(collider.gameObject);
                    EnemySpawner.enemyCounter--;
                    SCORE = SCORE + 5;
                    //UpdateScoreText();
                    enemigosDerrotados++;
                    naguilasmuertas++;
                    UpdateEnemiesText(); //actualiza enemigosDerrotados
                }
            } else if (collider.gameObject.CompareTag("Boss")) {
                Boss enemy = collider.GetComponent<Boss>();
                if (enemy.enLlamas) {
                    if(ModoInfinito.noMorir == false){
                        VIDAS--;
                        nvidas--;
                        ControlAudio.Instance.EjecutarSonidoDaño(dañorecibido);
                        ShouldIDie();
                    }
                } else {
                    enemy.toques--;
                    if(enemy.toques==0){
                        ControlAudio.Instance.EjecutarSonidoEnemigo(enemigoderrotado);
                        enemy.SetMuerteBoss(true);
                        //Destroy(collider.gameObject);
                        EnemySpawner.enemyCounter--;
                        SCORE = SCORE + 20;
                        //UpdateScoreText();
                        enemigosDerrotados++;
                        UpdateEnemiesText(); //actualiza enemigosDerrotados
                    }
                }
            }
        }

        if (collider.gameObject.CompareTag("HorizWall")){
            if(ModoInfinito.noMorir == false){
                VIDAS --;
                ControlAudio.Instance.EjecutarSonidoDaño(dañorecibido);
                ShouldIDie();
            }
                int horizontalDir = 0;
                if (this.transform.position.x < 0) {
                    horizontalDir = 1;
                } else {
                    horizontalDir = -1;
                }
                verticalBlock = Math.Sign(this.transform.position.y);
                Debug.Log("vertBlock = " + verticalBlock);
                position = horizontalDir*Vector3.right;
                rotationTrigger = 1;
                currentHorizDir = horizontalDir;
                currentVertDir = 0;
            } else if (collider.gameObject.CompareTag("VertWall")) {
                if(ModoInfinito.noMorir == false){
                    VIDAS --;
                    ControlAudio.Instance.EjecutarSonidoDaño(dañorecibido);
                    ShouldIDie();
                }
                int verticalDir = 0;
                if (this.transform.position.y < 0) {
                    verticalDir = 1;
                } else {
                    verticalDir = -1;
                }
                horizontalBlock = Math.Sign(this.transform.position.x);
                Debug.Log("horizBlock = " + horizontalBlock);
                position = verticalDir*Vector3.up;
                rotationTrigger = 1;
                currentVertDir = verticalDir;
                currentHorizDir = 0;
            }
    }

    public void ShouldIDie()  {
        if (VIDAS > 0) {
            makeSmallerTrigger = 1;
        } else {
            ReiniciarTiempo();
            //EndGame();
            //StartCoroutine(FindDelJuego());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void EndGame()
    {
        EndMenu.SetActive(true);  // Mostrar el menú de pausa
        Time.timeScale = 0f;   // Detener el tiempo en el juego

    }

   private void UpdateScoreText() {
        GameObject go = GameObject.FindGameObjectWithTag("Score");
        go.GetComponent<Text>().text = "PUNTUACION: " + SCORE;
    }

    static public void UpdateLongitudText() {
        GameObject go = GameObject.FindGameObjectWithTag("Longitud");
        go.GetComponent<Text>().text = "Longitud: " + longitud;
    }
    private void UpdateEnemiesText() {
        GameObject go = GameObject.FindGameObjectWithTag("Enemies"); // Busca el objeto con la etiqueta "Enemies"
        go.GetComponent<Text>().text = "ENEMIGOS DERROTADOS: " + enemigosDerrotados; // Actualiza el texto
    }

    private void UpdateFruitsText() {
        GameObject go = GameObject.FindGameObjectWithTag("Fruits"); // Busca el objeto con la etiqueta "Fruits"
        go.GetComponent<Text>().text = "FRUTAS COMIDAS: " + frutasComidas; // Actualiza el texto
    }
    static public void UpdateTiempoText() {
        GameObject go = GameObject.FindGameObjectWithTag("Tiempo");
        float tempAct = Time.time -tiempo;
        int minutos= Mathf.FloorToInt(tempAct / 60f);
        int segundos = Mathf.FloorToInt(tempAct % 60);
        go.GetComponent<Text>().text = "Tiempo: " + minutos + ":" + segundos;
    }
    static public void ReiniciarTiempo(){
        GameObject go = GameObject.FindGameObjectWithTag("Tiempo");
        tiempo = Time.time;
        go.GetComponent<Text>().text = "Tiempo: " + 0 + ":" + 0;
    }
    public void CreateLives() {
        for (int i = 0; i < hearts.Length; i++) {
            Vector3 pos = new Vector3(-30+i*4,19.5f,0);
            hearts[i] = Instantiate(heartPrefab,pos,Quaternion.identity);
        }
    }

    private void GestionarColision() {
        StartCoroutine(PausaEntreColisiones(1f));
    }


    private IEnumerator PausaEntreColisiones(float pausa) {
        detectarColisiones = false;
        yield return new WaitForSeconds(pausa);
        detectarColisiones = true;
    }

    private IEnumerator FindDelJuego() {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeToSpriteWithReset(int index, Sprite newSprite, Sprite originalSprite, float delay){
    // Verifica que el índice sea válido
    if (index >= 0 && index < body.Count)
    {
        // Obtén el objeto objetivo de la lista
        GameObject targetObject = body[index];

        // Obtén la posición actual del segmento antes de actualizar
        Vector3 segmentPosBefore = targetObject.transform.position;

        // Actualiza el sprite
        SpriteRenderer targetSpriteRenderer = targetObject.GetComponent<SpriteRenderer>();
        if (targetSpriteRenderer != null)
        {
            fly = true;
            targetSpriteRenderer.sprite = newSprite;

            // Ajusta la rotación y escala basándote en el movimiento
            Vector3 segmentPosAfter = targetObject.transform.position; // Posición después del cambio
            Vector3 movementDirection = segmentPosBefore - segmentPosAfter;

            if (movementDirection.x != 0) // Movimiento horizontal
            {
                Debug.Log("Movimiento horizontal detectado");
                targetObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                targetObject.transform.localScale = new Vector3(1, -Math.Sign(movementDirection.x), 1);
            }
            else if (movementDirection.y != 0) // Movimiento vertical
            {
                Debug.Log("Movimiento vertical detectado");
                targetObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                targetObject.transform.localScale = new Vector3(1, -Math.Sign(movementDirection.y), 1);
            }
            else
            {
                Debug.Log("No se detectó movimiento");
            }


            // Inicia la corrutina para restaurar el sprite después del retraso
            StartCoroutine(ResetSprite(targetObject, originalSprite, delay));
        }
    }
}

private IEnumerator ResetSprite(GameObject targetObject, Sprite originalSprite, float delay)
{
    // Espera el tiempo especificado
    yield return new WaitForSeconds(delay);
    fly = false;

    // Cambia de vuelta al sprite original
    SpriteRenderer targetSpriteRenderer = targetObject.GetComponent<SpriteRenderer>();
    if (targetSpriteRenderer != null)
    {
        targetSpriteRenderer.sprite = originalSprite;
    }
}


    /*
    private IEnumerator Blink() {
        detectarColisiones = false;
        List<SpriteRenderer> sp = new List<SpriteRenderer>();
        for (int i = 0; i < body.Count; i++) {
            sp.Add(body[i].GetComponent<SpriteRenderer>());
        }
        Color original = Color.white;
        Color transp = original;
        transp.a = 0.25f;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < body.Count; i++) {
                sp[j].color = transp;
            }
            yield return new WaitForSeconds(0.2f);
            for (int j = 0; j < body.Count; i++) {
                sp[j].color = original;
            }
            yield return new WaitForSeconds(0.2f);
        }
        detectarColisiones = true;
    }
    */

    private IEnumerator Blink(SpriteRenderer sr) {
        Color original = Color.white;
        Color transp = original;
        transp.a = 0.25f;
        for (int i = 0; i < 3; i++) {
            sr.color = transp;
            yield return new WaitForSecondsRealtime(0.2f);
            sr.color = original;
            yield return new WaitForSecondsRealtime(0.2f);
        }
        sr.sprite = sinVida;
    }


    //UNIT TESTS --------------------------------------------------------------------------

    //Getter del atributo currentHorizDir
    public int getCurrentHorizDir(){
        return this.currentHorizDir;
    }

    //Getter del atributo currentVertDir
    public int getCurrentVertDir(){
        return this.currentVertDir;
    }

    //getter del vector posición
    public Vector3 getPosition(){
        return this.position;
    }

    public List<GameObject> getBody(){
        return this.body;
    }

    public GameObject instanciar(){
        tail.transform.position = new Vector3(-1,0,0);
        body = new List<GameObject>();
        body.Add(this.gameObject);
        body.Add(tail);
        return Instantiate(bodyPrefab,position,Quaternion.identity);
    }

    public GameObject instanciarCola(){
        // body.Add(tail);
        // body.Add(tail);
        return Instantiate(tail,position,Quaternion.identity);
    }

    public int getVidas(){
        return this.VIDAS;
    }
}

