using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Linq;

public class Snake : MonoBehaviour
{

    private Vector3 position;
    private int rotationTrigger;
    private int makeBiggerTrigger;
    public GameObject bodyPrefab,tail;
    List<GameObject> body;
    private int currentHorizDir;
    private int currentVertDir;
    private int SCORE;
    private int frutasComidas;
    private int nivelAguila = 10;
    private int nivelFenec = 5;
    private int nivelCamaleon = 2;
    public static int makeSmallerTrigger;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(0.5f,0.5f,0);
        tail.transform.position = new Vector3(this.transform.position.x-1,0.5f,0);
        body = new List<GameObject>();
        body.Add(this.gameObject);
        body.Add(tail);
        currentHorizDir = 0;
        currentVertDir = 0;
        SCORE = 0;
        frutasComidas = 0;
        /*
        tail.transform.position = new Vector3(-1,0,0);
        body = new List<GameObject>();
        body.Add(this.gameObject);
        body.Add(tail);
        currentHorizDir = 0;
        currentVertDir = 0;
        SCORE = 0;
        */
    }

    // Update is called once per frame

    void Update() {
        int horizontalDir = Math.Sign(Input.GetAxis("Horizontal"));
        int verticalDir = Math.Sign(Input.GetAxis("Vertical"));

        //unidad de movimiento
        float magnitude = 1;

        if (horizontalDir != 0 && currentHorizDir == 0) {
            position = magnitude*horizontalDir*Vector3.right;
            rotationTrigger = 1;
            currentHorizDir = horizontalDir;
            currentVertDir = 0;
        } else if (verticalDir != 0 && currentVertDir == 0) {
            position = magnitude*verticalDir*Vector3.up;
            rotationTrigger = 1;
            currentVertDir = verticalDir;
            currentHorizDir = 0;
            //transform.Translate(0,movement*Time.deltaTime,0);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            makeBiggerTrigger = 1;
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            makeSmallerTrigger = 1;
        }

    }
    void FixedUpdate()
    {
        if (makeBiggerTrigger == 1) {
            SCORE++;
            makeBigger();
            UpdateScoreText();
        }

        if (makeSmallerTrigger == 1) {
            SCORE--;
            makeSmaller();
            UpdateScoreText();
        }

        Vector3 tailPosBefore = tail.transform.position;
        for(int i = body.Count - 1; i > 0; i--) {
            body[i].transform.position = body[i-1].transform.position;
        }
        Vector3 tailPosAfter = tail.transform.position;
        
        Vector3 newDirection = tailPosBefore - tailPosAfter;

        if (newDirection.x != 0) {
            tail.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
            tail.transform.localScale = new Vector3(1,-newDirection.x,1);
        } else if (newDirection.y != 0) {
            tail.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
            tail.transform.localScale = new Vector3(1,-newDirection.y,1);
        }

        this.transform.position += position;

        if(rotationTrigger != 0) {
            if (currentHorizDir != 0) {
                transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
                transform.localScale = new Vector3(1,-currentHorizDir,1);
            } else {
                transform.rotation = Quaternion.Euler(new Vector3(0,0,-180));
                transform.localScale = new Vector3(1,-currentVertDir,1);
            }
            rotationTrigger = 0;
        }
    }

    public void makeBigger() {
        Vector3 pos = body[body.Count-1].transform.position;
        GameObject newSegment = Instantiate(bodyPrefab,pos,Quaternion.identity);
        body.Insert(body.Count-1,newSegment);
        makeBiggerTrigger = 0;
    }

    private void makeSmaller() {
        GameObject segment = body.ElementAt<GameObject>(body.Count-2);
        body.RemoveAt(body.Count-2);
        Destroy(segment);
        makeSmallerTrigger = 0;
    }

    //falta la direccion de la cola


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
        if (collider.gameObject.CompareTag("Body") || /*collider.gameObject.CompareTag("Wall") ||*/
            collider.gameObject.CompareTag("Enemy")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else if (collider.gameObject.CompareTag("Fruit")) {
            SCORE++;
            UpdateScoreText();
            Destroy(collider.gameObject);
            makeBiggerTrigger = 1;
            frutasComidas++;
        } else if (collider.gameObject.CompareTag("Lagarto")) {
            if (SCORE < nivelCamaleon) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } else {
                Destroy(collider.gameObject);
            }
        } else if (collider.gameObject.CompareTag("Fenec")) {
            if (SCORE < nivelFenec) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } else {
                Destroy(collider.gameObject);
            }
        } else if (collider.gameObject.CompareTag("Aguila")) {
            if (SCORE < nivelAguila) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } else {
                Destroy(collider.gameObject);
            }
        }
    }

    private void UpdateScoreText() {
        GameObject go = GameObject.FindGameObjectWithTag("Score");
        go.GetComponent<Text>().text = "PUNTOS: " + SCORE;
    }

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

    public int size(){
        return this.body.Count;
    }

    public GameObject getTail(){
        return this.tail;
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
}

