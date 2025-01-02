using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float speed = 5f;  // Velocidad de movimiento
    public float rotationSpeed = 90f;  // Velocidad de rotación en grados por segundo
    private Vector2 moveDirection;  // Dirección de movimiento en 2D
    private float currentRotation = 0f;  // Ángulo actual de rotación en grados
    private float xBorderLimit, yBorderLimit;
    public float rotationInterval = 2f;  // Intervalo de rotación en segundos
    private float timeSinceLastRotation;
    private SpriteRenderer sprite;
    public Sprite spriteR;
    public Sprite spriteL;
    public Animator animator;
    public bool muerte_fenec = false;
    private Deteccion det;
    private int nivelEnemigo = 5;


    void Start()
    {
        // Inicializar la dirección de movimiento 
        moveDirection = Vector2.up;
        // Bordes del mapa
        xBorderLimit = 36;
        yBorderLimit = 17;
        // Inicializar sprite
        sprite = GetComponent<SpriteRenderer>();
        animator= GetComponent<Animator>();

        det = GetComponentInChildren<Deteccion>();
        det.detEntrada += SpriteAColor;
        det.detSalida += SpriteANormal;
    }

    void Update()
    {
        // Mover el enemigo en la dirección de movimiento
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
        timeSinceLastRotation += Time.deltaTime;
        if (timeSinceLastRotation >= rotationInterval)
        {
            RotateEnemy();
            timeSinceLastRotation = 0f;
        }
        var newPos = transform.position;
        if(newPos.x > xBorderLimit)
        newPos.x = -xBorderLimit+1;
        else if(newPos.x < -xBorderLimit)
        newPos.x = xBorderLimit-1;
        else if(newPos.y > yBorderLimit)
        newPos.y = -yBorderLimit+1;
        else if(newPos.y < -yBorderLimit)
        newPos.y = yBorderLimit-1;
        transform.position = newPos;

        animator.SetBool("muerte_fenec", muerte_fenec);
    }
    void RotateEnemy()
    {
        // Rotar al enemigo un ángulo aleatorio
        int randomAngle = UnityEngine.Random.Range(0, 4);
        currentRotation = (randomAngle*90f)%360f;

        // Aplicar la rotación al transform
        if (randomAngle == 1){
            this.transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
        }
        if (randomAngle == 3){
            this.transform.localScale = new Vector3(Math.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
        }

        // Actualizar la dirección de movimiento según el ángulo de rotación actual
        float radians = currentRotation * Mathf.Deg2Rad;
        if(currentRotation == 0){
            moveDirection = new Vector2(0,1);
        }
        else if(currentRotation == 90){
            moveDirection = new Vector2(-1,0);
        }
        else if(currentRotation == 180){
            moveDirection = new Vector2(0,-1);
        }
        else if(currentRotation == 270){
            moveDirection = new Vector2(1,0);
        }
    }

    public void SpriteAColor() {
        //cambiar la animacion a color verde o rojo
        if (Snake.longitud < nivelEnemigo) {
            //cambiar a rojo
            animator.SetInteger("color_fenec", 1);
        } else {
            //cambiar a verde
            animator.SetInteger("color_fenec", 2);
        }
    }

    public void SpriteANormal() {
        //devolver la animacion a color normal
        animator.SetInteger("color_fenec", 0);
    }

    public void SetMuerteFenec(bool estado){
        if(estado){
            this.tag = "Muerto";
            muerte_fenec = estado;
            speed = 0f;
            Destroy(gameObject,3f);
            animator.SetBool("muerte_fenec", false);
        }
        /*muerte_lagarto = estado;
        if(muerte_lagarto == true){
            animator.SetBool("muerte_lagarto", true);
            Destroy(gameObject,10f);
        }*/
    }
}
